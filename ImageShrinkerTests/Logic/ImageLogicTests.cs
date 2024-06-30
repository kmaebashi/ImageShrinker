using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageShrinker.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace ImageShrinker.Logic.Tests
{
    [TestClass()]
    public class ImageLogicTests
    {
        [TestMethod()]
        public void ResizeImageTest001()
        {
            string srcImagePath = @"TestInput\ImageLogicTest\horizontal.jpg";
            ImageLogic.ResizeImage(srcImagePath, 500, 500, ResizeMode.AdjustToWidth, "_s", @"TestOutput\ImageLogicTest", true);
            int width, height;
            GetImageSize(@"TestOutput\ImageLogicTest\horizontal_s.jpg", out width, out height);
            Assert.AreEqual(500, width);
            Assert.AreEqual(375, height);
        }

        [TestMethod()]
        public void ResizeImageTest002()
        {
            string srcImagePath = @"TestInput\ImageLogicTest\vertical.jpg";
            ImageLogic.ResizeImage(srcImagePath, 500, 500, ResizeMode.AdjustToWidth, "_s", @"TestOutput\ImageLogicTest", true);
            int width, height;
            GetImageSize(@"TestOutput\ImageLogicTest\vertical_s.jpg", out width, out height);
            Assert.AreEqual(500, width);
            Assert.AreEqual(666, height);
        }

        [TestMethod()]
        public void ResizeImageTest003()
        {
            string srcImagePath = @"TestInput\ImageLogicTest\horizontal.jpg";
            ImageLogic.ResizeImage(srcImagePath, 500, 500, ResizeMode.FitIntoRectangle, "_s2", @"TestOutput\ImageLogicTest", true);
            int width, height;
            GetImageSize(@"TestOutput\ImageLogicTest\horizontal_s2.jpg", out width, out height);
            Assert.AreEqual(500, width);
            Assert.AreEqual(375, height);
        }

        [TestMethod()]
        public void ResizeImageTest004()
        {
            string srcImagePath = @"TestInput\ImageLogicTest\vertical.jpg";
            ImageLogic.ResizeImage(srcImagePath, 500, 500, ResizeMode.FitIntoRectangle, "_s2", @"TestOutput\ImageLogicTest", true);
            int width, height;
            GetImageSize(@"TestOutput\ImageLogicTest\vertical_s2.jpg", out width, out height);
            Assert.AreEqual(375, width);
            Assert.AreEqual(500, height);
        }

        [TestMethod()]
        public void ResizeImageTestOverwrite001()
        {
            string srcImagePath = @"TestInput\ImageLogicTest\existing_horizontal.jpg";
            ResizeImageStatus status 
                = ImageLogic.ResizeImage(srcImagePath, 500, 500, ResizeMode.FitIntoRectangle, "_s3", @"TestOutput\ImageLogicTest", true);
            Assert.AreEqual(ResizeImageStatus.Success, status);
        }

        [TestMethod()]
        public void ResizeImageTestNotOverwrite001()
        {
            string srcImagePath = @"TestInput\ImageLogicTest\existing_horizontal.jpg";
            ResizeImageStatus status
                = ImageLogic.ResizeImage(srcImagePath, 500, 500, ResizeMode.FitIntoRectangle, "_s3", @"TestOutput\ImageLogicTest", false);
            Assert.AreEqual(ResizeImageStatus.FileExists, status);
        }

        [TestMethod()]
        public void ResizeImageTestPng001()
        {
            string srcImagePath = @"TestInput\ImageLogicTest\png_horizontal.png";
            ImageLogic.ResizeImage(srcImagePath, 500, 500, ResizeMode.AdjustToWidth, "_s", @"TestOutput\ImageLogicTest", true);
            int width, height;
            GetImageSize(@"TestOutput\ImageLogicTest\png_horizontal_s.png", out width, out height);
            Assert.AreEqual(500, width);
            Assert.AreEqual(375, height);
        }

        [TestMethod()]
        public void ResizeImageTestPng002()
        {
            string srcImagePath = @"TestInput\ImageLogicTest\png_vertical.png";
            ImageLogic.ResizeImage(srcImagePath, 500, 500, ResizeMode.AdjustToWidth, "_s", @"TestOutput\ImageLogicTest", true);
            int width, height;
            GetImageSize(@"TestOutput\ImageLogicTest\png_vertical_s.png", out width, out height);
            Assert.AreEqual(500, width);
            Assert.AreEqual(666, height);
        }

        [TestMethod()]
        public void ResizeImageTestPng003()
        {
            string srcImagePath = @"TestInput\ImageLogicTest\png_horizontal.png";
            ImageLogic.ResizeImage(srcImagePath, 500, 500, ResizeMode.FitIntoRectangle, "_s2", @"TestOutput\ImageLogicTest", true);
            int width, height;
            GetImageSize(@"TestOutput\ImageLogicTest\png_horizontal_s2.png", out width, out height);
            Assert.AreEqual(500, width);
            Assert.AreEqual(375, height);
        }

        [TestMethod()]
        public void ResizeImageTestPng004()
        {
            string srcImagePath = @"TestInput\ImageLogicTest\png_vertical.png";
            ImageLogic.ResizeImage(srcImagePath, 500, 500, ResizeMode.FitIntoRectangle, "_s2", @"TestOutput\ImageLogicTest", true);
            int width, height;
            GetImageSize(@"TestOutput\ImageLogicTest\png_vertical_s2.png", out width, out height);
            Assert.AreEqual(375, width);
            Assert.AreEqual(500, height);
        }


        [TestMethod()]
        public void ResizeImageTestSmall001()
        {
            string srcImagePath = @"TestInput\ImageLogicTest\small.png";
            ResizeImageStatus status
                = ImageLogic.ResizeImage(srcImagePath, 500, 500, ResizeMode.AdjustToWidth, "_s1", @"TestOutput\ImageLogicTest", false);
            int width, height;
            GetImageSize(@"TestOutput\ImageLogicTest\small_s1.png", out width, out height);
            Assert.AreEqual(32, width);
            Assert.AreEqual(32, height);
        }

        [TestMethod()]
        public void ResizeImageTestSmall002()
        {
            string srcImagePath = @"TestInput\ImageLogicTest\small.png";
            ResizeImageStatus status
                = ImageLogic.ResizeImage(srcImagePath, 500, 500, ResizeMode.FitIntoRectangle, "_s2", @"TestOutput\ImageLogicTest", false);
            int width, height;
            GetImageSize(@"TestOutput\ImageLogicTest\small_s2.png", out width, out height);
            Assert.AreEqual(32, width);
            Assert.AreEqual(32, height);
        }

        private static void GetImageSize(string path, out int width, out int height)
        {
            Bitmap bitmap = new System.Drawing.Bitmap(path);
            width = bitmap.Width;
            height = bitmap.Height;
        }

        [TestMethod()]
        public void IsJpegBitmapTest001()
        {
            Bitmap bitmap = new Bitmap(@"TestInput\ImageLogicTest\horizontal.jpg");
            Assert.IsTrue(ImageLogic.IsJpegBitmap(bitmap));
        }

        [TestMethod()]
        public void IsJpegBitmapTest002()
        {
            Bitmap bitmap = new Bitmap(@"TestInput\ImageLogicTest\png_horizontal.png");
            Assert.IsFalse(ImageLogic.IsJpegBitmap(bitmap));
        }
    }
}