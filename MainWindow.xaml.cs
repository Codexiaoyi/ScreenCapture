using System;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Win32;

namespace ScreenCaptureDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 当前窗口句柄
        /// </summary>
        private IntPtr m_Hwnd = new IntPtr();

        /// <summary>
        /// 记录快捷键注册项的唯一标识符
        /// </summary>
        private Dictionary<EHotKeySetting, int> m_HotKeySettings = new Dictionary<EHotKeySetting, int>();
        System.Windows.Forms.NotifyIcon notifyIcon = null;
        private Bitmap SkinBitmap;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            NotifyIcon();
        }

        private void NotifyIcon()
        {
            this.notifyIcon = new System.Windows.Forms.NotifyIcon();
            //this.notifyIcon.BalloonTipText = "ECMS 服务正在运行..."; //设置程序启动时显示的文本
            this.notifyIcon.Text = "Alt + Z是截图快捷键";//最小化到托盘时，鼠标点击时显示的文本
            this.notifyIcon.Icon = new Icon(@".\ico.ico");//程序图标
            this.notifyIcon.Visible = true;

            //System.Windows.Forms.MenuItem skin = new System.Windows.Forms.MenuItem("设置背景");
            //skin.Click += Skin_Click;
            //右键菜单--退出菜单项
            System.Windows.Forms.MenuItem setting = new System.Windows.Forms.MenuItem("设置");
            setting.Click += new EventHandler(SettingWindow);
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("退出");
            exit.Click += new EventHandler(CloseWindow);
            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { setting,exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);
        }

        private void SettingWindow(object sender, EventArgs e)
        {
            if (App.Current.Windows.Cast<Window>().Any(x => x is Setting)) return;
            Setting setting = new Setting();
            setting.ShowDialog();
        }

        private void Skin_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog videoFile = new OpenFileDialog();
                videoFile.Filter = "png|*.png|jpg|*.jpg";
                if (videoFile.ShowDialog() == true)
                {
                    var fileName = videoFile.FileName;
                    SkinBitmap = new Bitmap(fileName);
                }
            }
            catch
            { }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            this.notifyIcon.Visible = false;
            Application.Current.Shutdown();
        }

        /// <summary>
        /// 窗体加载完成后事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HotKeySettingsManager.Instance.RegisterGlobalHotKeyEvent += Instance_RegisterGlobalHotKeyEvent;
        }

        /// <summary>
        /// WPF窗体的资源初始化完成，并且可以通过WindowInteropHelper获得该窗体的句柄用来与Win32交互后调用
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            // 获取窗体句柄
            m_Hwnd = new WindowInteropHelper(this).Handle;
            HwndSource hWndSource = HwndSource.FromHwnd(m_Hwnd);
            // 添加处理程序
            if (hWndSource != null) hWndSource.AddHook(WndProc);
        }

        /// <summary>
        /// 所有控件初始化完成后调用
        /// </summary>
        /// <param name="e"></param>
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            // 注册热键
            InitHotKey();
        }

        /// <summary>
        /// 通知注册系统快捷键事件处理函数
        /// </summary>
        /// <param name="hotKeyModelList"></param>
        /// <returns></returns>
        private bool Instance_RegisterGlobalHotKeyEvent(ObservableCollection<HotKeyModel> hotKeyModelList)
        {
            return InitHotKey(hotKeyModelList);
        }

        ///// <summary>
        ///// 打开快捷键设置窗体
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnOpenHotkeySetting_Click(object sender, RoutedEventArgs e)
        //{
        //    var win = HotKeySettingsWindow.CreateInstance();
        //    if (!win.IsVisible)
        //    {
        //        win.ShowDialog();
        //    }
        //    else
        //    {
        //        win.Activate();
        //    }
        //}

        /// <summary>
        /// 初始化注册快捷键
        /// </summary>
        /// <param name="hotKeyModelList">待注册热键的项</param>
        /// <returns>true:保存快捷键的值；false:弹出设置窗体</returns>
        private bool InitHotKey(ObservableCollection<HotKeyModel> hotKeyModelList = null)
        {
            var list = hotKeyModelList ?? HotKeySettingsManager.Instance.LoadDefaultHotKey();
            // 注册全局快捷键
            string failList = HotKeyHelper.RegisterGlobalHotKey(list, m_Hwnd, out m_HotKeySettings);
            if (string.IsNullOrEmpty(failList))
                return true;
            //MessageBoxResult mbResult = MessageBox.Show(string.Format("无法注册下列快捷键\n\r{0}是否要改变这些快捷键？", failList), "提示", MessageBoxButton.YesNo);
            // 弹出热键设置窗体
            //var win = HotKeySettingsWindow.CreateInstance();
            //if (mbResult == MessageBoxResult.Yes)
            //{
            //    if (!win.IsVisible)
            //    {
            //        win.ShowDialog();
            //    }
            //    else
            //    {
            //        win.Activate();
            //    }
            //    return false;
            //}
            return true;
        }
        /// <summary>
        /// 窗体回调函数，接收所有窗体消息的事件处理函数,在这里设置快捷键操作
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="msg">消息</param>
        /// <param name="wideParam">附加参数1</param>
        /// <param name="longParam">附加参数2</param>
        /// <param name="handled">是否处理</param>
        /// <returns>返回句柄</returns>
        private IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wideParam, IntPtr longParam, ref bool handled)
        {

            //var hotkeySetting = new EHotKeySetting();
            switch (msg)
            {
                case HotKeyManager.WM_HOTKEY:
                    int sid = wideParam.ToInt32();
                    //if (sid == m_HotKeySettings[EHotKeySetting.全屏])
                    //{
                    //    hotkeySetting = EHotKeySetting.全屏;
                    //    //TODO 执行全屏操作
                    //}
                    //else 
                    if (App.Current.Windows.Cast<Window>().Any(x => x is PrintScreen)) break;  //查看是否存在Window7窗口正在运行
                    if (sid == m_HotKeySettings[EHotKeySetting.截图])
                    {
                        //hotkeySetting = EHotKeySetting.截图;
                        DateTime dt = DateTime.Now;
                        Bitmap bitMap = ScreenCaptureHelper.GetScreenSnapshot();
                        BitmapImage bitmapImage = ScreenCaptureHelper.BitmapToBitmapImage(bitMap);
                        //BitmapImage skinBitmap = ScreenCaptureHelper.BitmapToBitmapImage(SkinBitmap);
                        PrintScreen win7 = new PrintScreen(bitmapImage, bitMap);
                        win7.ShowDialog();

                        //ImageSource img = System.Windows.Clipboard.GetImage();
                    }
                    //else if (sid == m_HotKeySettings[EHotKeySetting.播放])
                    //{
                    //    hotkeySetting = EHotKeySetting.播放;
                    //    //TODO ......
                    //}
                    //else if (sid == m_HotKeySettings[EHotKeySetting.前进])
                    //{
                    //    hotkeySetting = EHotKeySetting.前进;
                    //}
                    //else if (sid == m_HotKeySettings[EHotKeySetting.后退])
                    //{
                    //    hotkeySetting = EHotKeySetting.后退;
                    //}
                    //else if (sid == m_HotKeySettings[EHotKeySetting.保存])
                    //{
                    //    hotkeySetting = EHotKeySetting.保存;
                    //}
                    //else if (sid == m_HotKeySettings[EHotKeySetting.打开])
                    //{
                    //    hotkeySetting = EHotKeySetting.打开;
                    //}
                    //else if (sid == m_HotKeySettings[EHotKeySetting.新建])
                    //{
                    //    hotkeySetting = EHotKeySetting.新建;
                    //}
                    //else if (sid == m_HotKeySettings[EHotKeySetting.删除])
                    //{
                    //    hotkeySetting = EHotKeySetting.删除;
                    //}
                    //MessageBox.Show(string.Format("触发【{0}】快捷键", hotkeySetting));
                    handled = true;
                    break;
            }
            return IntPtr.Zero;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //DateTime dt = DateTime.Now;
            //Bitmap bitMap = ScreenCaptureHelper.GetScreenSnapshot();
            //BitmapImage bitmapImage = ScreenCaptureHelper.BitmapToBitmapImage(bitMap);

            //PrintScreen win7 = new PrintScreen(bitmapImage, bitMap);
            //win7.ShowDialog();//开启截屏主界面

            //ImageSource img = System.Windows.Clipboard.GetImage();
        }

    }
}

