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
            if (IsJpegBitmap(srcBitmap))
            {
                RotateJpeg(srcBitmap);
            }
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
            if (scale > 1.0)
            {
                scale = 1.0;
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

        public static Boolean IsJpegBitmap(Bitmap bitmap)
        {
            ImageCodecInfo[] decoders = ImageCodecInfo.GetImageDecoders();

            return bitmap.RawFormat.Guid == ImageFormat.Jpeg.Guid;
        }

        private static RotateFlipType[] rotateFlipTypeTable =
        {
            RotateFlipType.RotateNoneFlipNone,
            RotateFlipType.RotateNoneFlipX,
            RotateFlipType.RotateNoneFlipXY,
            RotateFlipType.RotateNoneFlipY,
            RotateFlipType.Rotate90FlipX,
            RotateFlipType.Rotate90FlipNone,
            RotateFlipType.Rotate90FlipY,
            RotateFlipType.Rotate270FlipNone
        };

        private static void RotateJpeg(Bitmap src)
        {
            int exifOrientation = GetJpegOrientation(src);
            if (exifOrientation == 1)
            {
                return;
            }
            RotateFlipType rotateFlipType = rotateFlipTypeTable[exifOrientation - 1];
            src.RotateFlip(rotateFlipType);
        }

        /// <returns>
        /// 1: The 0th row is at the visual top of the image, and the 0th column is the visual left-hand side.
        /// 2: The 0th row is at the visual top of the image, and the 0th column is the visual right-hand side.
        /// 3: The 0th row is at the visual bottom of the image, and the 0th column is the visual right-hand side.
        /// 4: The 0th row is at the visual bottom of the image, and the 0th column is the visual left-hand side.
        /// 5: The 0th row is the visual left-hand side of the image, and the 0th column is the visual top.
        /// 6: The 0th row is the visual right-hand side of the image, and the 0th column is the visual top.
        /// 7: The 0th row is the visual right-hand side of the image, and the 0th column is the visual bottom.
        /// 8: The 0th row is the visual left-hand side of the image, and the 0th column is the visual bottom.
        /// </returns>
        private static int GetJpegOrientation(Bitmap bitmap)
        {
            foreach (var item in bitmap.PropertyItems)
            {
                if (item.Id == 0x0112)
                {
                    if (item.Len != 2 || item.Type != 3 || item.Value[0] < 1 || item.Value[0] > 8)
                    {
                        throw new ResizeImageException("Exifのフォーマットが不正です");
                    }
                    return item.Value[0];
                }
            }
            return 1;
        }
    }
}
