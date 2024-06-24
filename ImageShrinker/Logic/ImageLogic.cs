using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ImageShrinker.Logic
{
    public static class ImageLogic
    {
        public static ResizeImageStatus ResizeImage(string srcImagePath, int destWidth, int destHeight, ResizeMode resizeMode,
                                                    string suffix, string outputFolder, Boolean overwrite)
        {
            Bitmap srcBitmap = new Bitmap(srcImagePath);
            int srcWidth = srcBitmap.Width;
            int srcHeight = srcBitmap.Height;

            double scale;
            if (resizeMode == ResizeMode.AdjustToWidth)
            {
                scale = (double)destWidth / srcWidth;
            }
            else if (resizeMode == ResizeMode.FitIntoRectangle)
            {
                double xScale = (double)destWidth / srcWidth;
                double yScale = (double)destHeight / srcHeight;
                scale = Math.Min(xScale, yScale);
            }
            else
            {
                throw new ApplicationException("不正なresizeMode.." + resizeMode);
            }
            Bitmap destBitmap = new Bitmap((int)(srcWidth * scale), (int)(srcHeight * scale));
            Graphics g = Graphics.FromImage(destBitmap);

            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(srcBitmap, 0, 0, destBitmap.Width, destBitmap.Height);

            ImageFormat imageFormat;
            string fileSuffix = Path.GetExtension(srcImagePath).ToLower();
            if (fileSuffix == ".jpg" || fileSuffix == ".jpeg")
            {
                imageFormat = ImageFormat.Jpeg;
            }
            else if (fileSuffix == ".png")
            {
                imageFormat = ImageFormat.Png;
            }
            else
            {
                throw new ApplicationException("画像フォーマット" + fileSuffix + "は対応していません");
            }
            string destFileName = Path.GetFileNameWithoutExtension(srcImagePath) + suffix + fileSuffix;
            string destPath = Path.Combine(outputFolder, destFileName);

            if (File.Exists(destPath))
            {
                if (overwrite)
                {
                    File.Delete(destPath);
                }
                else
                {
                    return ResizeImageStatus.FileExists;
                }
            }
            destBitmap.Save(destPath, imageFormat);



            return ResizeImageStatus.Success;
        }
    }
}
