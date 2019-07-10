using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ScreenCaptureDemo
{
    /// <summary>
    /// 截屏界面交互逻辑
    /// </summary>
    public partial class PrintScreen : Window
    {
        #region 参数定义
        private BitmapImage GlobalBitmapImage;
        private System.Drawing.Bitmap GlobalBitmap;
        private bool ScopeFlag = false;//标志是否选定范围
        private Point MovePreviousPoint;//移动前的点
        private Point MoveCurrentPoint;//移动时的点
        private PointEnum CurrentPoint;//当前点的枚举值
        private Border GlobalBorder;//范围选定框
        private Border LeftAnchor;
        private Border TopAnchor;
        private Border RightAnchor;
        private Border BottomAnchor;
        private Border LeftTopAnchor;
        private Border LeftBottomAnchor;
        private Border RightTopAnchor;
        private Border RightBottomAnchor;
        private Border TopShade;//上方遮罩
        private Border DownShade;//下方遮罩
        private Border LeftShade;//左边遮罩
        private Border RightShade;//右边遮罩
        private Rect Rect;//矩形框
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <param name="bitMap"></param>
        public PrintScreen(BitmapImage bitmapImage, System.Drawing.Bitmap bitMap)
        {
            InitializeComponent();
            Loaded += PrintScreen_Loaded;
            Closed += PrintScreen_Closed;
            GlobalBitmap = bitMap;
            GlobalBitmapImage = bitmapImage;
        }

        private void PrintScreen_Closed(object sender, EventArgs e)
        {
            GlobalBitmap.Dispose();
        }

        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintScreen_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            Width = System.Windows.Forms.SystemInformation.VirtualScreen.Width;
            Height = System.Windows.Forms.SystemInformation.VirtualScreen.Height;
            Left = 0;
            Top = 0;
            ImageContainer.Source = GlobalBitmapImage;
            //将范围选择框与八个范围控制点初始化并加入到Canvas容器中
            GlobalBorder = new Border() { BorderBrush = Brushes.BlueViolet, BorderThickness = new Thickness(2), Visibility = Visibility.Collapsed, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            MainGrid.Children.Add(GlobalBorder);
            LeftAnchor = new Border() { Width = 6, Height = 6, Background = Brushes.Blue, Visibility = Visibility.Collapsed, Cursor = Cursors.SizeWE, BorderThickness = new Thickness(0), Tag = PointEnum.Left };
            MainGrid.Children.Add(LeftAnchor);
            TopAnchor = new Border() { Width = 6, Height = 6, Background = Brushes.Blue, Visibility = Visibility.Collapsed, Cursor = Cursors.SizeNS, BorderThickness = new Thickness(0), Tag = PointEnum.Top };
            MainGrid.Children.Add(TopAnchor);
            RightAnchor = new Border() { Width = 6, Height = 6, Background = Brushes.Blue, Visibility = Visibility.Collapsed, Cursor = Cursors.SizeWE, BorderThickness = new Thickness(0), Tag = PointEnum.Right };
            MainGrid.Children.Add(RightAnchor);
            BottomAnchor = new Border() { Width = 6, Height = 6, Background = Brushes.Blue, Visibility = Visibility.Collapsed, Cursor = Cursors.SizeNS, BorderThickness = new Thickness(0), Tag = PointEnum.Bottom };
            MainGrid.Children.Add(BottomAnchor);
            LeftTopAnchor = new Border() { Width = 6, Height = 6, Background = Brushes.Blue, Visibility = Visibility.Collapsed, Cursor = Cursors.SizeNWSE, BorderThickness = new Thickness(0), Tag = PointEnum.TopLeft };
            MainGrid.Children.Add(LeftTopAnchor);
            LeftBottomAnchor = new Border() { Width = 6, Height = 6, Background = Brushes.Blue, Visibility = Visibility.Collapsed, Cursor = Cursors.SizeNESW, BorderThickness = new Thickness(0), Tag = PointEnum.BottomLeft };
            MainGrid.Children.Add(LeftBottomAnchor);
            RightTopAnchor = new Border() { Width = 6, Height = 6, Background = Brushes.Blue, Visibility = Visibility.Collapsed, Cursor = Cursors.SizeNESW, BorderThickness = new Thickness(0), Tag = PointEnum.TopRight };
            MainGrid.Children.Add(RightTopAnchor);
            RightBottomAnchor = new Border() { Width = 6, Height = 6, Background = Brushes.Blue, Visibility = Visibility.Collapsed, Cursor = Cursors.SizeNWSE, BorderThickness = new Thickness(0), Tag = PointEnum.BottomRight };
            MainGrid.Children.Add(RightBottomAnchor);
            //将遮罩初始化并加入容器
            TopShade = new Border() { Background = Brushes.Black, Opacity = 0.6, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            MainGrid.Children.Add(TopShade);
            DownShade = new Border() { Background = Brushes.Black, Opacity = 0.6, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            MainGrid.Children.Add(DownShade);
            LeftShade = new Border() { Background = Brushes.Black, Opacity = 0.6, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            MainGrid.Children.Add(LeftShade);
            RightShade = new Border() { Background = Brushes.Black, Opacity = 0.6, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            MainGrid.Children.Add(RightShade);
            //选择框及各点位事件绑定
            GlobalBorder.MouseLeftButtonDown += GlobalBorder_MouseLeftButtonDown;
            LeftAnchor.MouseLeftButtonDown += Anchor_MouseLeftButtonDown;
            LeftAnchor.MouseLeftButtonUp += Anchor_MouseLeftButtonUp;
            TopAnchor.MouseLeftButtonDown += Anchor_MouseLeftButtonDown;
            TopAnchor.MouseLeftButtonUp += Anchor_MouseLeftButtonUp;
            RightAnchor.MouseLeftButtonDown += Anchor_MouseLeftButtonDown;
            RightAnchor.MouseLeftButtonUp += Anchor_MouseLeftButtonUp;
            BottomAnchor.MouseLeftButtonDown += Anchor_MouseLeftButtonDown;
            BottomAnchor.MouseLeftButtonUp += Anchor_MouseLeftButtonUp;
            LeftTopAnchor.MouseLeftButtonDown += Anchor_MouseLeftButtonDown;
            LeftTopAnchor.MouseLeftButtonUp += Anchor_MouseLeftButtonUp;
            LeftBottomAnchor.MouseLeftButtonDown += Anchor_MouseLeftButtonDown;
            LeftBottomAnchor.MouseLeftButtonUp += Anchor_MouseLeftButtonUp;
            RightTopAnchor.MouseLeftButtonDown += Anchor_MouseLeftButtonDown;
            RightTopAnchor.MouseLeftButtonUp += Anchor_MouseLeftButtonUp;
            RightBottomAnchor.MouseLeftButtonDown += Anchor_MouseLeftButtonDown;
            RightBottomAnchor.MouseLeftButtonUp += Anchor_MouseLeftButtonUp;
        }

        #region 定位、遮罩、保存
        /// <summary>
        /// 定位
        /// </summary>
        /// <param name="previousPoint">固定点</param>
        /// <param name="currentPoint">当前点</param>
        public void Location(Point previousPoint, Point currentPoint)
        {
            Point newPreviousPoint = previousPoint;//点位变化
            Point newCurrentPoint = currentPoint;
            //计算当前框宽高
            double borderWidth = Math.Abs(newCurrentPoint.X - newPreviousPoint.X);
            double borderHeight = Math.Abs(newCurrentPoint.Y - newPreviousPoint.Y);
            //界限判定，超界则两点交换
            if (newPreviousPoint.X > newCurrentPoint.X)
            {
                newCurrentPoint.X = previousPoint.X;
                newPreviousPoint.X = currentPoint.X;
            }
            if (newPreviousPoint.Y > newCurrentPoint.Y)
            {
                newCurrentPoint.Y = previousPoint.Y;
                newPreviousPoint.Y = currentPoint.Y;
            }
            //界限判定边界不得超出窗口
            if (newPreviousPoint.X < 0)
            {
                newPreviousPoint.X = 0;
                newCurrentPoint = new Point(newPreviousPoint.X + borderWidth, newPreviousPoint.Y + borderHeight);
            }
            if (newPreviousPoint.Y < 0)
            {
                newPreviousPoint.Y = 0;
                newCurrentPoint = new Point(newPreviousPoint.X + borderWidth, newPreviousPoint.Y + borderHeight);
            }
            if (newPreviousPoint.X + GlobalBorder.ActualWidth > windows.ActualWidth)
            {
                newPreviousPoint.X = windows.ActualWidth - GlobalBorder.ActualWidth;
                newCurrentPoint = new Point(newPreviousPoint.X + borderWidth, newPreviousPoint.Y + borderHeight);
            }
            if (newPreviousPoint.Y + GlobalBorder.ActualHeight > windows.ActualHeight)
            {
                newPreviousPoint.Y = windows.ActualHeight - GlobalBorder.ActualHeight;
                newCurrentPoint = new Point(newPreviousPoint.X + borderWidth, newPreviousPoint.Y + borderHeight);
            }
            //各点位定位
            LeftTopAnchor.Margin = new Thickness(newPreviousPoint.X, newPreviousPoint.Y, 0, 0);
            LeftTopAnchor.Visibility = Visibility.Visible;
            LeftAnchor.Margin = new Thickness(newPreviousPoint.X, borderHeight / 2 + newPreviousPoint.Y, 0, 0);
            LeftAnchor.Visibility = Visibility.Visible;
            LeftBottomAnchor.Margin = new Thickness(newPreviousPoint.X, borderHeight + newPreviousPoint.Y - 5, 0, 0);
            LeftBottomAnchor.Visibility = Visibility.Visible;
            TopAnchor.Margin = new Thickness(newPreviousPoint.X + borderWidth / 2, newPreviousPoint.Y, 0, 0);
            TopAnchor.Visibility = Visibility.Visible;
            BottomAnchor.Margin = new Thickness(newPreviousPoint.X + borderWidth / 2, borderHeight + newPreviousPoint.Y - 5, 0, 0);
            BottomAnchor.Visibility = Visibility.Visible;
            RightTopAnchor.Margin = new Thickness(newCurrentPoint.X - 5, newPreviousPoint.Y, 0, 0);
            RightTopAnchor.Visibility = Visibility.Visible;
            RightAnchor.Margin = new Thickness(newCurrentPoint.X - 5, borderHeight / 2 + newPreviousPoint.Y, 0, 0);
            RightAnchor.Visibility = Visibility.Visible;
            RightBottomAnchor.Margin = new Thickness(newCurrentPoint.X - 5, newCurrentPoint.Y - 5, 0, 0);
            RightBottomAnchor.Visibility = Visibility.Visible;
            //选择框定位
            GlobalBorder.Margin = new Thickness(newPreviousPoint.X, newPreviousPoint.Y, 0, 0);
            GlobalBorder.Visibility = Visibility.Visible;
            GlobalBorder.Width = borderWidth;
            GlobalBorder.Height = borderHeight;
        }

        /// <summary>
        /// 遮罩函数
        /// </summary>
        public void Mask()
        {
            double PreviousShadeX = GlobalBorder.Margin.Left;//遮罩大小计算
            double PreviousShadeY = GlobalBorder.Margin.Top;
            double CurrentShadeX = GlobalBorder.Margin.Left + GlobalBorder.Width;
            double CurrentShadeY = GlobalBorder.Margin.Top + GlobalBorder.Height;
            //超界情况处理
            if (CurrentShadeX < PreviousShadeX)
            {
                PreviousShadeX = GlobalBorder.Margin.Left + GlobalBorder.Width;
                CurrentShadeX = GlobalBorder.Margin.Left;
            }
            if (CurrentShadeY < PreviousShadeY)
            {
                PreviousShadeY = GlobalBorder.Margin.Top + GlobalBorder.Height;
                CurrentShadeY = GlobalBorder.Margin.Top;
            }
            if (PreviousShadeX < 0) PreviousShadeX = 0;
            if (PreviousShadeY < 0) PreviousShadeY = 0;
            if (CurrentShadeX > windows.ActualWidth) CurrentShadeX = windows.ActualWidth;
            if (CurrentShadeY > windows.ActualHeight) CurrentShadeY = windows.ActualHeight;
            //四个遮罩定位
            TopShade.Margin = new Thickness(0, 0, 0, 0);
            TopShade.Width = windows.ActualWidth;
            TopShade.Height = PreviousShadeY;

            DownShade.Margin = new Thickness(0, CurrentShadeY, 0, 0);
            DownShade.Width = windows.ActualWidth;
            DownShade.Height = windows.Height - CurrentShadeY;

            LeftShade.Margin = new Thickness(0, PreviousShadeY, 0, 0);
            LeftShade.Width = PreviousShadeX;
            LeftShade.Height = windows.ActualHeight - TopShade.Height - DownShade.Height;

            RightShade.Margin = new Thickness(CurrentShadeX, PreviousShadeY, 0, 0);
            RightShade.Width = windows.ActualWidth - CurrentShadeX;
            RightShade.Height = windows.ActualHeight - TopShade.Height - DownShade.Height;
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <returns></returns>
        public BitmapSource Save()
        {
            if (GlobalBorder != null)
            {
                double left = GlobalBorder.Margin.Left;
                double top = GlobalBorder.Margin.Top;
                System.Drawing.Image CatchedBmp = new System.Drawing.Bitmap((int)GlobalBorder.Width, (int)GlobalBorder.Height);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(CatchedBmp); //创建图片画布 
                ////目标范围
                System.Drawing.Rectangle desiRectangle = new System.Drawing.Rectangle(0, 0, (int)GlobalBorder.Width, (int)GlobalBorder.Height);
                ////源范围
                System.Drawing.Rectangle sourceRectangle = new System.Drawing.Rectangle((int)left, (int)top, (int)GlobalBorder.Width, (int)GlobalBorder.Height);
                g.DrawImage(GlobalBitmap, desiRectangle, sourceRectangle, System.Drawing.GraphicsUnit.Pixel);
                //保存到剪贴板
                System.Drawing.Bitmap map = (System.Drawing.Bitmap)CatchedBmp;
                BitmapSource source = ScreenCaptureHelper.ToBitmapSource(map);
                g.Dispose();
                CatchedBmp.Dispose();
                this.Close();
                return source;
            }
            return null;
        }
        #endregion

        #region MainGrid事件

        /// <summary>
        /// MainGrid按下鼠标事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ScopeFlag == false)//范围未选择时进行起始点记录及遮罩初始化
            {
                MovePreviousPoint = e.GetPosition(MainGrid);
            }
        }

        /// <summary>
        /// MainGrid移动鼠标事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                return;
            }
            MoveCurrentPoint = e.GetPosition(MainGrid);
            Point previousPoint;//固定点
            Point currentPoint;//当前扩展点
            if (CurrentPoint != PointEnum.None)//未选择点位时可对各点扩展
            {
                switch (CurrentPoint)
                {
                    case PointEnum.TopLeft:
                        previousPoint = Rect.BottomRight;
                        currentPoint = new Point(MoveCurrentPoint.X, MoveCurrentPoint.Y);
                        break;
                    case PointEnum.Top:
                        previousPoint = new Point(Rect.TopLeft.X, MoveCurrentPoint.Y);
                        currentPoint = Rect.BottomRight;
                        break;
                    case PointEnum.TopRight:
                        previousPoint = Rect.BottomLeft;
                        currentPoint = new Point(MoveCurrentPoint.X, MoveCurrentPoint.Y);
                        break;
                    case PointEnum.Right:
                        previousPoint = Rect.TopLeft;
                        currentPoint = new Point(MoveCurrentPoint.X, Rect.BottomRight.Y);
                        break;
                    case PointEnum.BottomRight:
                        previousPoint = Rect.TopLeft;
                        currentPoint = new Point(MoveCurrentPoint.X, MoveCurrentPoint.Y);
                        break;
                    case PointEnum.Bottom:
                        previousPoint = Rect.TopLeft;
                        currentPoint = new Point(Rect.BottomRight.X, MoveCurrentPoint.Y);
                        break;
                    case PointEnum.BottomLeft:
                        previousPoint = Rect.TopRight;
                        currentPoint = new Point(MoveCurrentPoint.X, MoveCurrentPoint.Y);
                        break;
                    case PointEnum.Left:
                        previousPoint = new Point(MoveCurrentPoint.X, Rect.Y);
                        currentPoint = Rect.BottomRight;
                        break;
                    case PointEnum.Move:
                        double moveX = MoveCurrentPoint.X - MovePreviousPoint.X;//移动范围记录
                        double moveY = MoveCurrentPoint.Y - MovePreviousPoint.Y;
                        previousPoint = new Point(Rect.TopLeft.X + moveX, Rect.TopLeft.Y + moveY);
                        currentPoint = new Point(Rect.BottomRight.X + moveX, Rect.BottomRight.Y + moveY);
                        break;
                    default:
                        previousPoint = Rect.TopLeft;
                        currentPoint = Rect.BottomLeft;
                        break;
                }
                ToolPanel.Visibility = Visibility.Collapsed;
                Location(previousPoint, currentPoint);
                Mask();
            }
            if (ScopeFlag == false)//初始范围框
            {
                GlobalBorder.Cursor = Cursors.SizeAll;
                GlobalBorder.Background = Brushes.Transparent;
                Location(MovePreviousPoint, MoveCurrentPoint);
                Mask();
            }
        }

        /// <summary>
        /// MainGrid鼠标离开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            ScopeFlag = true;
            CurrentPoint = PointEnum.None;
        }

        /// <summary>
        /// MainGrid鼠标抬起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CurrentPoint = PointEnum.None;
            ToolPanel.Visibility = Visibility.Visible;
            if (GlobalBorder.Margin.Top + GlobalBorder.Height > windows.ActualHeight - 44 && GlobalBorder.Margin.Top >= 44)
            {
                ToolPanel.Margin = new Thickness(GlobalBorder.Margin.Left + GlobalBorder.Width - 160, GlobalBorder.Margin.Top - 42, windows.Width - (GlobalBorder.Margin.Left + GlobalBorder.Width), windows.Height - GlobalBorder.Margin.Top);
            }
            else if (GlobalBorder.Margin.Top + GlobalBorder.Height > windows.ActualHeight - 44 && GlobalBorder.Margin.Top < 44)
            {
                ToolPanel.Margin = new Thickness(GlobalBorder.Margin.Left + GlobalBorder.Width - 160, GlobalBorder.Margin.Top, windows.Width - (GlobalBorder.Margin.Left + GlobalBorder.Width), windows.Height - GlobalBorder.Margin.Top - 40);
            }
            else
            {
                ToolPanel.Margin = new Thickness(GlobalBorder.Margin.Left + GlobalBorder.Width - 160, GlobalBorder.Margin.Top + GlobalBorder.Height, windows.Width - (GlobalBorder.Margin.Left + GlobalBorder.Width), windows.Height - (GlobalBorder.Margin.Top + GlobalBorder.Height) - 45);
            }
            ScopeFlag = true;
        }
        #endregion

        #region 定位点与范围选择框的鼠标按下和抬起事件
        private void Anchor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CurrentPoint = PointEnum.None;
        }

        private void Anchor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rect = new Rect(new Point(GlobalBorder.Margin.Left, GlobalBorder.Margin.Top), new Point(GlobalBorder.Margin.Left + GlobalBorder.ActualWidth, GlobalBorder.Margin.Top + GlobalBorder.ActualHeight));
            CurrentPoint = (PointEnum)(sender as FrameworkElement).Tag;
        }

        private void GlobalBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MovePreviousPoint = e.GetPosition(MainGrid);
            Rect = new Rect(new Point(GlobalBorder.Margin.Left, GlobalBorder.Margin.Top), new Point(GlobalBorder.Margin.Left + GlobalBorder.ActualWidth, GlobalBorder.Margin.Top + GlobalBorder.ActualHeight));
            CurrentPoint = PointEnum.Move;
        }
        #endregion

        /// <summary>
        /// 按钮保存剪切板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            BitmapSource source = Save();
            Clipboard.SetImage(source);
        }

        /// <summary>
        /// 按钮退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Esc快捷键退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit(object sender, CanExecuteRoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 按钮保存文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            BitmapSource source = Save();
            try
            {
                System.Windows.Forms.SaveFileDialog save = new System.Windows.Forms.SaveFileDialog()//开启文件选择框
                {
                    RestoreDirectory = true,
                };
                save.Filter = "png|*.png";
                save.FileName = "image" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                save.ShowDialog();
                PngBitmapEncoder pbe = new PngBitmapEncoder();
                pbe.Frames.Add(BitmapFrame.Create(source));
                using (Stream stream = File.Create(save.FileName))
                {
                    pbe.Save(stream);//文件流创建文件，图片保存为png格式
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 双击保存剪切板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Windows_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BitmapSource source = Save();
            Clipboard.SetImage(source);
        }

        /// <summary>
        /// 右键退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Windows_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
