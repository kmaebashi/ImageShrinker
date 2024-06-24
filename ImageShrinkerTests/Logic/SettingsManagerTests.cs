using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageShrinker.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageShrinker.Logic.Tests
{
    [TestClass()]
    public class SettingsManagerTests
    {
        private static string htmlTemplateStr =
@"<p>ここに説明を書く</p>
<p><a href=""./{0}""><img src=""./{1}""></a></p>
";

        [TestMethod()]
        public void ReadSettingsTest001()
        {
            Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsTest001.txt",
                                                             @"TestInput\SettingsManagerTest\html_template001.txt");
            Assert.AreEqual(500, settings.ResizedWidth);
            Assert.AreEqual(300, settings.ResizedHeight);
            Assert.AreEqual("_s", settings.Suffix);
            Assert.AreEqual(true, settings.OverwriteFlag);
            Assert.AreEqual(ResizeMode.AdjustToWidth, settings.CurrentResizeMode);
            Assert.AreEqual(htmlTemplateStr, settings.HtmlTemplate);
            Assert.AreEqual(@"C:\input", settings.DefaultInputFolder);
            Assert.AreEqual(@"C:\output", settings.OutputFolder);
        }

        [TestMethod()]
        public void ReadSettingsTest002()
        {
            Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsTest002.txt",
                                                             @"TestInput\SettingsManagerTest\html_template001.txt");
            Assert.AreEqual(null, settings.ResizedWidth);
            Assert.AreEqual(null, settings.ResizedHeight);
            Assert.AreEqual("_s", settings.Suffix);
            Assert.AreEqual(false, settings.OverwriteFlag);
            Assert.AreEqual(ResizeMode.FitIntoRectangle, settings.CurrentResizeMode);
            Assert.AreEqual(htmlTemplateStr, settings.HtmlTemplate);
            Assert.AreEqual(null, settings.DefaultInputFolder);
            Assert.AreEqual(null, settings.OutputFolder);
        }

        [TestMethod()]
        public void ReadSettingsTest003()
        {
            Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsTest003.txt",
                                                             @"TestInput\SettingsManagerTest\html_template001.txt");
            Assert.AreEqual(null, settings.ResizedWidth);
            Assert.AreEqual(null, settings.ResizedHeight);
            Assert.AreEqual("_s", settings.Suffix);
            Assert.AreEqual(false, settings.OverwriteFlag);
            Assert.AreEqual(ResizeMode.FitIntoRectangle, settings.CurrentResizeMode);
            Assert.AreEqual(htmlTemplateStr, settings.HtmlTemplate);
            Assert.AreEqual("a=b", settings.DefaultInputFolder);
            Assert.AreEqual(null, settings.OutputFolder);
        }

        [TestMethod()]
        public void ReadSettingsError001()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\not_exists.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (FileNotFoundException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError002()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsTest001.txt",
                                                                 @"TestInput\SettingsManagerTest\not_exists.txt");
            }
            catch (FileNotFoundException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError101()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError101.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("設定値ファイルの書式が不正です(ResizedWidth)", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError102()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError102.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("設定値が重複しています(ResizedWidth)", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError103()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError103.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("ResizedWidthの値が不正です(abc)", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError104()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError104.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("ResizedHeightの値が不正です(３００)", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError105()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError105.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("OverwriteFlagの値が不正です(abc)。trueかfalseを指定してください。", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError106()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError106.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("ResizeModeの値が不正です(abc)", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError107()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError107.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("設定項目名が不正です(Hoge)", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError108()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError108.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("未設定の設定項目があります", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError109()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError109.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("未設定の設定項目があります", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError110()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError110.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("未設定の設定項目があります", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError111()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError111.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("未設定の設定項目があります", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError112()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError112.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("未設定の設定項目があります", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError113()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError113.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("未設定の設定項目があります", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError114()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError114.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("未設定の設定項目があります", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError115()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError115.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("未設定の設定項目があります", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSettingsError116()
        {
            try
            {
                Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsError116.txt",
                                                                 @"TestInput\SettingsManagerTest\html_template001.txt");
            }
            catch (SettingsReadException ex)
            {
                Assert.AreEqual("SameAsSrcFolderの値が不正です(abc)。trueかfalseを指定してください。", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteSettingsTest001()
        {
            Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsTest001.txt",
                                                             @"TestInput\SettingsManagerTest\html_template001.txt");
            SettingsManager.WriteSettings(settings, @"TestOutput\SettingsManagerTest\WriteSettingsTest001.txt",
                                          @"TestOutput\SettingsManagerTest\write_html_template001.txt");
            CompareFile(@"TestOutput\SettingsManagerTest\WriteSettingsTest001.txt",
                        @"TestOutput\SettingsManagerTest\WriteSettingsCorrect001.txt");
            CompareFile(@"TestOutput\SettingsManagerTest\write_html_template001.txt",
                        @"TestOutput\SettingsManagerTest\html_template_correct001.txt");
        }

        [TestMethod()]
        public void WriteSettingsTest002()
        {
            Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsTest002.txt",
                                                             @"TestInput\SettingsManagerTest\html_template001.txt");
            SettingsManager.WriteSettings(settings, @"TestOutput\SettingsManagerTest\WriteSettingsTest002.txt",
                                          @"TestOutput\SettingsManagerTest\write_html_template002.txt");
            CompareFile(@"TestOutput\SettingsManagerTest\WriteSettingsTest002.txt",
                        @"TestOutput\SettingsManagerTest\WriteSettingsCorrect002.txt");
            CompareFile(@"TestOutput\SettingsManagerTest\write_html_template002.txt",
                        @"TestOutput\SettingsManagerTest\html_template_correct001.txt");
        }

        [TestMethod()]
        public void WriteSettingsTest010()
        {
            Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsTest001.txt",
                                                             @"TestInput\SettingsManagerTest\html_template001.txt");

            if (File.Exists(@"TestOutput\SettingsManagerTest\WriteSettingsTest010.txt"))
            {
                File.Delete(@"TestOutput\SettingsManagerTest\WriteSettingsTest010.txt");
            }
            if (File.Exists(@"TestOutput\SettingsManagerTest\write_html_template10.txt"))
            {
                File.Delete(@"TestOutput\SettingsManagerTest\write_html_template10.txt");
            }
            SettingsManager.WriteSettings(settings, @"TestOutput\SettingsManagerTest\WriteSettingsTest010.txt",
                                          @"TestOutput\SettingsManagerTest\write_html_template010.txt");
            CompareFile(@"TestOutput\SettingsManagerTest\WriteSettingsTest010.txt",
                        @"TestOutput\SettingsManagerTest\WriteSettingsCorrect001.txt");
            CompareFile(@"TestOutput\SettingsManagerTest\write_html_template010.txt",
                        @"TestOutput\SettingsManagerTest\html_template_correct001.txt");
        }

        [TestMethod()]
        public void WriteSettingsTest011()
        {
            Settings settings = SettingsManager.ReadSettings(@"TestInput\SettingsManagerTest\SettingsTest001.txt",
                                                             @"TestInput\SettingsManagerTest\html_template001.txt");

            if (File.Exists(@"TestOutput\SettingsManagerTest\WriteSettingsTest011.txt"))
            {
                File.Delete(@"TestOutput\SettingsManagerTest\WriteSettingsTest011.txt");
            }
            if (File.Exists(@"TestOutput\SettingsManagerTest\write_html_template11.txt"))
            {
                File.Delete(@"TestOutput\SettingsManagerTest\write_html_template11.txt");
            }
            SettingsManager.WriteSettings(settings, @"TestOutput\SettingsManagerTest\WriteSettingsTest011.txt",
                                          @"TestOutput\SettingsManagerTest\write_html_template011.txt");
            SettingsManager.WriteSettings(settings, @"TestOutput\SettingsManagerTest\WriteSettingsTest011.txt",
                                          @"TestOutput\SettingsManagerTest\write_html_template011.txt");
            Assert.IsTrue(File.Exists(@"TestOutput\SettingsManagerTest\WriteSettingsTest011_bak.txt"));
            Assert.IsTrue(File.Exists(@"TestOutput\SettingsManagerTest\write_html_template011_bak.txt"));
        }

        private void CompareFile(string path1, string path2)
        {
            using (var sr1 = new StreamReader(path1))
            using (var sr2 = new StreamReader(path2))
            {
                string text1 = sr1.ReadToEnd();
                string text2 = sr2.ReadToEnd();

                Assert.AreEqual(text1, text2);
            }
        }

    }
}