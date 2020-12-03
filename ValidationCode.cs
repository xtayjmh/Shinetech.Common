using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Shinetech.Common
{
    public class ValidationCode
    {
        public static string GetRandomString(int length)
        {
            return Guid.NewGuid().ToString("N").Substring(32 - length, length);
        }

        public static byte[] GetVcodeImg(int length, int width = 200, int height = 45, int fontSize = 20)
        {
            var strCode = GetRandomString(length);
            return GetVcodeImg(strCode, width, height, fontSize);
        }

        /// <summary>
        ///     由随机字符串，随即颜色背景，和随机线条产生的Image
        /// </summary>
        /// <returns>Image</returns>
        public static byte[] GetVcodeImg(string text, int width = 140, int height = 40, int fontSize = 16)
            //返回 Image
        {
            var charNum = text.Length;
            //声明一个位图对象
            Bitmap bitMap = null;
            //声明一个绘图画面
            Graphics gph = null;
            //创建内存流
            var memStream = new MemoryStream();
            var random = new Random();
            //由给定的需要生成字符串中字符个数 CharNum， 图片宽度 Width 和高度 Height 确定字号 FontSize，
            //确保不因字号过大而不能全部显示在图片上
            var fontWidth = (int) Math.Round(width / (charNum + 2) / 1.3);
            var fontHeight = (int) Math.Round(height / 1.5);
            //字号取二者中小者，以确保所有字符能够显示，并且字符的下半部分也能显示
            fontSize = fontWidth <= fontHeight ? fontWidth : fontHeight;
            //创建位图对象
            bitMap = new Bitmap(width + fontSize, height);
            //根据上面创建的位图对象创建绘图图面
            gph = Graphics.FromImage(bitMap);
            //设定验证码图片背景色
            gph.Clear(GetControllableColor(200));
            //产生随机干扰线条
            for (var i = 0; i < 10; i++)
            {
                var backPen = new Pen(GetControllableColor(100), 2);
                //线条起点
                var x = random.Next(width);
                var y = random.Next(height);
                //线条终点
                var x2 = random.Next(width);
                var y2 = random.Next(height);
                //划线
                gph.DrawLine(backPen, x, y, x2, y2);
            }

            //定义一个含10种字体的数组
            string[] fontFamily =
            {
                "Arial", "Verdana", "Comic Sans MS", "Impact", "Haettenschweiler",
                "Lucida Sans Unicode", "Garamond", "Courier New", "Book Antiqua", "Arial Narrow"
            };

            var sb = new SolidBrush(GetControllableColor(0));
            //通过循环,绘制每个字符,
            for (var i = 0; i < text.Length; i++)
            {
                var textFont = new Font(fontFamily[random.Next(10)], fontSize, FontStyle.Bold); //字体随机,字号大小30,加粗
                //每次循环绘制一个字符,设置字体格式,画笔颜色,字符相对画布的X坐标,字符相对画布的Y坐标
                var space = (int) Math.Round((double) ((width - fontSize * (charNum + 2)) / charNum));
                //纵坐标
                var y = (int) Math.Round((double) ((height - fontSize) / 3));
                gph.DrawString(text.Substring(i, 1), textFont, sb, fontSize + i * (fontSize + space), y);
            }

            //扭曲图片
            bitMap = TwistImage(bitMap, true, random.Next(3, 5), random.Next(3));

            bitMap.Save(memStream, ImageFormat.Gif);

            bitMap.Dispose();

            return memStream.ToArray();
        }

        /// <summary>
        ///     产生一种 R,G,B 均大于 colorBase 随机颜色，以确保颜色不会过深
        /// </summary>
        /// <returns>背景色</returns>
        private static Color GetControllableColor(int colorBase)
        {
            var color = Color.Black;

            var random = new Random();
            //确保 R,G,B 均大于 colorBase，这样才能保证背景色较浅
            color = Color.FromArgb(random.Next(56) + colorBase, random.Next(56) + colorBase, random.Next(56) + colorBase);
            return color;
        }

        /// <summary>
        ///     扭曲图片
        /// </summary>
        /// <param name="srcBmp"></param>
        /// <param name="bXDir"></param>
        /// <param name="dMultValue"></param>
        /// <param name="dPhase"></param>
        /// <returns></returns>
        private static Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            var leftMargin = 0;
            var rightMargin = 0;
            var topMargin = 0;
            var bottomMargin = 0;

            var PI2 = 6.28318530717959f;
            var destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            var dBaseAxisLen = bXDir ? Convert.ToDouble(destBmp.Height) : Convert.ToDouble(destBmp.Width);
            for (var i = 0; i < destBmp.Width; i++)
            for (var j = 0; j < destBmp.Height; j++)
            {
                double dx = 0;
                dx = bXDir ? PI2 * Convert.ToDouble(j) / dBaseAxisLen : PI2 * Convert.ToDouble(i) / dBaseAxisLen;
                dx += dPhase;
                var dy = Math.Sin(dx);
                //取得当前点的颜色
                var nOldX = 0;
                var nOldY = 0;
                nOldX = bXDir ? i + Convert.ToInt32(dy * dMultValue) : i;
                nOldY = bXDir ? j : j + Convert.ToInt32(dy * dMultValue);
                var color = srcBmp.GetPixel(i, j);
                if (nOldX >= leftMargin && nOldX < destBmp.Width - rightMargin && nOldY >= bottomMargin && nOldY < destBmp.Height - topMargin) destBmp.SetPixel(nOldX, nOldY, color);
            }

            return destBmp;
        }
    }
}