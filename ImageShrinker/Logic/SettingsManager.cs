using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageShrinker.Logic
{
    public static class SettingsManager
    {
        private static string ChopComment(string src)
        {
            int sharpIdx = src.IndexOf("#");
            if (sharpIdx < 0)
            {
                return src;
            }
            return src.Substring(0, sharpIdx);
        }

        public static Settings ReadSettings(string settingsPath, string htmlTemplatePath)
        {
            Settings ret = null;

            using (var sr = new StreamReader(settingsPath))
            {
                bool resizedWidthEmpty = false;
                bool resizedHeightEmpty = false;
                int? resizedWidth = null;
                int? resizedHeight = null;
                String suffix = null;
                bool? overwriteFlag = null;
                ResizeMode? currentResizeMode = null;
                String defaultInputFolder = null;
                String outputFolder = null;
                bool? sameAsSrcFolder = null;
                HashSet<string> keySet = new HashSet<string>();

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string line2 = ChopComment(line).TrimEnd();
                    if (line2.Length == 0)
                    {
                        continue;
                    }
                    string[] splitted = line2.Split(new[] { '=' }, 2);
                    if (splitted.Length != 2)
                    {
                        throw new SettingsReadException("設定値ファイルの書式が不正です(" + line + ")");
                    }
                    string key = splitted[0];
                    string valueStr = splitted[1];

                    if (keySet.Contains(key))
                    {
                        throw new SettingsReadException("設定値が重複しています(" + key + ")");
                    }
                    keySet.Add(key);

                    int tempIntValue;
                    bool tempBoolValue;
                    ResizeMode tempResizeMode;

                    switch (key)
                    {
                        case "ResizedWidth":
                            if (valueStr.Length == 0)
                            {
                                resizedWidthEmpty = true;
                            }
                            else if (Int32.TryParse(valueStr, out tempIntValue))
                            {
                                resizedWidth = tempIntValue;
                            }
                            else
                            {
                                throw new SettingsReadException("ResizedWidthの値が不正です(" + valueStr + ")");
                            }
                            break;
                        case "ResizedHeight":
                            if (valueStr.Length == 0)
                            {
                                resizedHeightEmpty = true;
                            }
                            else if (Int32.TryParse(valueStr, out tempIntValue))
                            {
                                resizedHeight = tempIntValue;
                            }
                            else
                            {
                                throw new SettingsReadException("ResizedHeightの値が不正です(" + valueStr + ")");
                            }
                            break;
                        case "Suffix":
                            suffix = valueStr;
                            break;
                        case "OverwriteFlag":
                            if (Boolean.TryParse(valueStr, out tempBoolValue))
                            {
                                overwriteFlag = tempBoolValue;
                            }
                            else
                            {
                                throw new SettingsReadException("OverwriteFlagの値が不正です(" + valueStr + ")。trueかfalseを指定してください。");
                            }
                            overwriteFlag = tempBoolValue;
                            break;
                        case "ResizeMode":
                            if (ResizeMode.TryParse(valueStr, out tempResizeMode))
                            {
                                currentResizeMode = tempResizeMode;
                            }
                            else
                            {
                                throw new SettingsReadException("ResizeModeの値が不正です(" + valueStr + ")");
                            }
                            break;
                        case "DefaultInputFolder":
                            defaultInputFolder = valueStr;
                            break;
                        case "OutputFolder":
                            outputFolder = valueStr;
                            break;
                        case "SameAsSrcFolder":
                            if (Boolean.TryParse(valueStr, out tempBoolValue))
                            {
                                sameAsSrcFolder = tempBoolValue;
                            }
                            else
                            {
                                throw new SettingsReadException("SameAsSrcFolderの値が不正です(" + valueStr + ")。trueかfalseを指定してください。");
                            }
                            break;
                        default:
                            throw new SettingsReadException("設定項目名が不正です(" + key + ")");
                    }
                }
                if ((resizedWidth == null && !resizedWidthEmpty) || (resizedHeight == null && !resizedHeightEmpty)
                    || suffix == null || overwriteFlag == null || currentResizeMode == null
                    || defaultInputFolder == null || outputFolder == null || sameAsSrcFolder == null)
                {
                    throw new SettingsReadException("未設定の設定項目があります");
                }
                if (defaultInputFolder.Length == 0)
                {
                    defaultInputFolder = null;
                }
                if (outputFolder.Length == 0)
                {
                    outputFolder = null;
                }

                ret = new Settings(resizedWidth, resizedHeight, suffix, (bool)overwriteFlag, (ResizeMode)currentResizeMode,
                                   defaultInputFolder, outputFolder, (bool)sameAsSrcFolder);
            }
            using (var sr = new StreamReader(htmlTemplatePath))
            {
                ret.HtmlTemplate = sr.ReadToEnd();
            }

            return ret;
        }

        public static void WriteSettings(Settings settings, string settingsPath, string htmlTemplatePath)
        {
            BackupFile(settingsPath);
            using (var sw = new StreamWriter(settingsPath))
            {
                sw.WriteLine("ResizedWidth=" + settings.ResizedWidth ?? "");
                sw.WriteLine("ResizedHeight=" + settings.ResizedHeight ?? "");
                sw.WriteLine("Suffix=" +  settings.Suffix);
                sw.WriteLine("OverwriteFlag=" + settings.OverwriteFlag);
                sw.WriteLine("ResizeMode=" + settings.CurrentResizeMode);
                sw.WriteLine("DefaultInputFolder=" + settings.DefaultInputFolder ?? "");
                sw.WriteLine("OutputFolder=" + settings.OutputFolder ?? "");
                sw.WriteLine("SameAsSrcFolder=" + settings.SameAsSrcFolder);
            }

            BackupFile(htmlTemplatePath);
            using (var sw = new StreamWriter(htmlTemplatePath))
            {
                sw.Write(settings.HtmlTemplate);
            }
        }

        private static void BackupFile(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }
            string backupFilePath
                = Path.Combine(Path.GetDirectoryName(path),
                               (Path.GetFileNameWithoutExtension(path) + "_bak" + Path.GetExtension(path)));
            if (File.Exists(backupFilePath))
            {
                File.Delete(backupFilePath);
            }
            File.Move(path, backupFilePath);
        }

        public static bool CompareSettings(Settings settings1, Settings settings2)
        {
            return (settings1.ResizedWidth == settings2.ResizedWidth)
                && (settings1.ResizedHeight == settings2.ResizedHeight)
                && (settings1.Suffix == settings2.Suffix)
                && (settings1.OverwriteFlag == settings2.OverwriteFlag)
                && (settings1.CurrentResizeMode == settings2.CurrentResizeMode)
                && (settings1.DefaultInputFolder == settings2.DefaultInputFolder)
                && (settings1.OutputFolder == settings2.OutputFolder)
                && (settings1.SameAsSrcFolder == settings2.SameAsSrcFolder);
        }
    }
}
