using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.IO;

namespace ScreenCaptureDemo
{
    public class ScreenCaptureHelper //截屏帮助类
    {
        /// <summary>
        /// 获取整个屏幕
        /// </summary>
        /// <returns></returns>
        public static Bitmap GetScreenSnapshot()
        {
            Rectangle rc = SystemInformation.VirtualScreen;
            var bitmap = new Bitmap(rc.Width, rc.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
            }

            return bitmap;
        }

        /// <summary>
        /// 将Bitmap转为BitmapSource
        /// </summary>
        /// <param name="bmp">bmp图片</param>
        /// <returns></returns>
        public static BitmapSource ToBitmapSource(Bitmap bmp)
        {
            BitmapSource returnSource;

            try
            {
                returnSource = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
                returnSource = null;
            }

            return returnSource;

        }

        /// <summary>
        /// 选取截取的部分
        /// </summary>
        /// <param name="screenSnapshot">屏幕图片</param>
        /// <param name="region">截取点</param>
        /// <returns></returns>
        public static BitmapSource CopyFromScreenSnapshot(BitmapSource screenSnapshot, Rect region)
        {
            Int32Rect window = new Int32Rect((int)region.X, (int)region.Y, (int)region.Width, (int)region.Height);

            BitmapSource bitmapScr = ToBitmapSource(GetScreenSnapshot());

            //计算Stride

            int stride = bitmapScr.Format.BitsPerPixel * window.Width / 8;

            //声明字节数组

            byte[] data = new byte[window.Height * stride];

            //调用CopyPixels

            bitmapScr.CopyPixels(window, data, stride, 0);

            PngBitmapEncoder encoder = new PngBitmapEncoder();

            return BitmapSource.Create(window.Width, window.Height, 0, 0, PixelFormats.Bgr32, null, data, stride);
        }

        /// <summary>
        /// 截取图片保存
        /// </summary>
        /// <param name="wnd">截取点大小</param>
        /// <param name="saveLocation">保存地址</param>
        /// <param name="pictureName">保存名称</param>
        public static void TakePicture(Rect wnd, String saveLocation, String pictureName)
        {
            BitmapSource bitmapScr = CopyFromScreenSnapshot(ToBitmapSource(GetScreenSnapshot()), wnd);

            PngBitmapEncoder encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bitmapScr));

            FileStream fileStream = new FileStream(saveLocation + "\\" + pictureName + ".png", FileMode.Create, FileAccess.Write);

            encoder.Save(fileStream);

            fileStream.Close();

        }

        /// <summary>
        /// Bitmap转BitmapImage
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }

            return bitmapImage;
        }
    }
}
