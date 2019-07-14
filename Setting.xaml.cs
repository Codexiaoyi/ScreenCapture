using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScreenCaptureDemo
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (MyCheckBox.IsChecked == true)//如果选中则设置开机自启动
            {
                #region 设置开机自启
                string strName = AppDomain.CurrentDomain.BaseDirectory + "ScreenCaptureDemo.exe";//获取要自动运行的应用程序名
                if (!System.IO.File.Exists(strName))//判断要自动运行的应用程序文件是否存在
                    return;
                string strnewName = strName.Substring(strName.LastIndexOf("\\") + 1);//获取应用程序文件名，不包括路径
                RegistryKey registry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);//检索指定的子项
                if (registry == null)//若指定的子项不存在
                    registry = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");//则创建指定的子项
                registry.SetValue(strnewName, strName);//设置该子项的新的“键值对”
                #endregion
            }
            else
            {
                #region 取消开机自启
                string strName = AppDomain.CurrentDomain.BaseDirectory + "ScreenCaptureDemo.exe";//获取要自动运行的应用程序名
                if (!System.IO.File.Exists(strName))//判断要取消的应用程序文件是否存在
                    return;
                string strnewName = strName.Substring(strName.LastIndexOf("\\") + 1);///获取应用程序文件名，不包括路径
                RegistryKey registry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);//读取指定的子项
                if (registry == null)//若指定的子项不存在
                    registry = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");//则创建指定的子项
                registry.DeleteValue(strnewName, false);//删除指定“键名称”的键/值对
                #endregion
            }
        }
    }
}
