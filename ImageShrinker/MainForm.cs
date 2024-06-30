using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ImageShrinker.Logic;
using System.Configuration;


namespace ImageShrinker
{
    public partial class MainForm : Form
    {
        private OpenFileDialog openFileDialog;
        private string[] selectedFileNames;
        private FolderBrowserDialog outputFolderDialog;
        private ResizeMode currentResizeMode;
        private string selectedInputFolder = null;
        private string selectedOutputFolder = null;
        private Settings settings;
        private const string SettingsFileName = "settings.txt";
        private const string HtmlTemplateFileName = "html_template.txt";

        public MainForm()
        {
            Log.Write("ImageShrinkerを開始します。");
            InitializeComponent();

            this.resizeModePulldown.Items.Add("幅を合わせる");
            this.resizeModePulldown.Items.Add("サイズに収める");
            this.resizeModePulldown.SelectedIndex = 0;
            this.resizeModePulldown.DropDownStyle = ComboBoxStyle.DropDownList;


            this.openFileDialog = new OpenFileDialog();
            this.openFileDialog.Filter = "JPEGファイル(*.jpeg;*.jpg)|*.jpeg;*.jpg|PNGファイル(*.png)|*.png|すべてのファイル(*.*)|*.*";
            this.openFileDialog.FilterIndex = 1;
            this.openFileDialog.Title = "縮小する画像を選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.Multiselect = true;

            this.outputFolderDialog = new FolderBrowserDialog();
            this.outputFolderDialog.Description = "出力先フォルダを指定してください";

            this.settings = ReadSettings();
            SetSettingsToWindow();

        }

        private Settings ReadSettings()
        {
            Settings ret = null;
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                ret = SettingsManager.ReadSettings(Path.Combine(baseDir, SettingsFileName),
                                                                 Path.Combine(baseDir, HtmlTemplateFileName));
            }
            catch (SettingsReadException ex)
            {
                MessageBox.Show("設定ファイルの読み込みエラー\r\n" + ex.Message + "\r\n" + "プログラムを終了します", "ImageShrinker");
                this.Close();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("設定ファイルの読み込みエラー\r\nファイルがありません\r\n" + "プログラムを終了します", "ImageShrinker");
                Log.Write("設定ファイルの読み込みエラー\r\n" + ex.Message);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("設定ファイルの読み込みエラー\r\nプログラムを終了します", "ImageShrinker");
                Log.Write("設定ファイルの読み込みエラー\r\n" + ex.Message);
                this.Close();
            }

            return ret;
        }

        private void SetSettingsToWindow()
        {
            if (this.settings.ResizedWidth != null)
            {
                this.resizedWidthText.Text = "" + this.settings.ResizedWidth;
            }
            if (this.settings.ResizedHeight != null)
            {
                this.resizedHeightText.Text = "" + this.settings.ResizedHeight;
            }
            this.suffixText.Text = this.settings.Suffix ?? "";
            this.overwriteCheckBox.Checked = this.settings.OverwriteFlag;
            if (this.settings.CurrentResizeMode == ResizeMode.AdjustToWidth)
            {
                this.resizeModePulldown.SelectedIndex = 0;
            }
            else if (this.settings.CurrentResizeMode == ResizeMode.FitIntoRectangle)
            {
                this.resizeModePulldown.SelectedIndex = 1;
            }
            this.openFileDialog.InitialDirectory = this.settings.DefaultInputFolder ?? @"C:\";
            if (this.settings.OutputFolder != null)
            {
                this.outputFolderDialog.SelectedPath = this.settings.OutputFolder;
                this.outputFolderLabel.Text = this.settings.OutputFolder;
                this.selectedOutputFolder = this.settings.OutputFolder;
            }
            else
            {
                this.outputFolderDialog.SelectedPath = @"C:\";
            }
            this.sameAsSrcFolder.Checked = this.settings.SameAsSrcFolder;

            this.htmlTemplateTextBox.Text = this.settings.HtmlTemplate;
        }

        private Settings GetSettingsFromWindow()
        {
            int tempIntValue;
            int? resizedWidth = null;
            int? resizedHeight = null;
            string suffix;
            bool overwriteFlag;
            ResizeMode currentResizeMode;
            string defaultInputFolder;
            string outputFolder;
            bool sameAsSrcFolder;

            bool parsed = Int32.TryParse(this.resizedWidthText.Text, out tempIntValue);
            if (parsed)
            {
                resizedWidth = tempIntValue;
            }
            parsed = Int32.TryParse(this.resizedHeightText.Text, out tempIntValue);
            if (parsed)
            {
                resizedHeight = tempIntValue;
            }
            suffix = this.suffixText.Text;
            overwriteFlag = this.overwriteCheckBox.Checked;
            currentResizeMode = (ResizeMode)this.resizeModePulldown.SelectedIndex;
            defaultInputFolder = this.selectedInputFolder;
            outputFolder = this.selectedOutputFolder;
            sameAsSrcFolder = this.sameAsSrcFolder.Checked;

            Settings settings = new Settings(resizedWidth, resizedHeight, suffix, overwriteFlag, currentResizeMode,
                                        defaultInputFolder, outputFolder, sameAsSrcFolder);
            settings.HtmlTemplate = this.htmlTemplateTextBox.Text;

            return settings;
        }

        private void fileChooserButton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.selectedFileNames = this.openFileDialog.FileNames;
                this.fileChoosedLabel.Text = "" + this.openFileDialog.FileNames.Length + "個のファイルを選択しました";
                this.selectedInputFolder = Path.GetDirectoryName(this.openFileDialog.FileNames[0]);
            }
        }


        private void outputFolderChooserButton_Click(object sender, EventArgs e)
        {
            if (this.outputFolderDialog.ShowDialog() == DialogResult.OK)
            {
                this.outputFolderLabel.Text = this.outputFolderDialog.SelectedPath;
                this.selectedOutputFolder = this.outputFolderDialog.SelectedPath;
                this.sameAsSrcFolder.Checked = false;
            }
        }


        private void resizeButton_Click(object sender, EventArgs e)
        {
            if (this.selectedFileNames == null || this.selectedFileNames.Length == 0)
            {
                MessageBox.Show("画像ファイルを選んでください");
                return;
            }
            int resizedWidth;
            bool parsed = Int32.TryParse(this.resizedWidthText.Text, out resizedWidth);
            if (!parsed)
            {
                MessageBox.Show("リサイズ後の幅を入力してください");
            }
            int resizedHeight = 0;
            if (this.currentResizeMode != ResizeMode.AdjustToWidth)
            {
                parsed = Int32.TryParse(this.resizedHeightText.Text, out resizedHeight);
                if (!parsed)
                {
                    MessageBox.Show("リサイズ後の高さを入力してください");
                }

            }
            this.progressBar.Minimum = 0;
            this.progressBar.Maximum = this.selectedFileNames.Length;

            string suffix = this.suffixText.Text;
            string htmlTemplate = this.htmlTemplateTextBox.Text;
            StringBuilder htmlSb = new StringBuilder();

            for (int i = 0; i < this.selectedFileNames.Length; i++)
            {
                string outputFolder = this.selectedOutputFolder ?? Path.GetDirectoryName(this.selectedFileNames[i]);
                try
                {
                    ResizeImageStatus status
                        = ImageLogic.ResizeImage(this.selectedFileNames[i], resizedWidth, resizedHeight, this.currentResizeMode, suffix,
                                           outputFolder, overwriteCheckBox.Checked);
                    if (status == ResizeImageStatus.FileExists)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(this.selectedFileNames[i])
                                        + suffix
                                        + Path.GetExtension(this.selectedFileNames[i]);
                        MessageBox.Show("ファイル" + fileName + "は既に存在します。処理を中断します。");
                        break;
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("画像のリサイズでエラーが発生しました。\r\n" + ex.Message);
                    Log.Write("画像のリサイズでエラーが発生しました。\r\n" + ex);
                    break;
                }
                try
                {
                    htmlSb.Append(convertHtml(this.selectedFileNames[i], suffix, htmlTemplate));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("HTMLテンプレートのフォーマットが不正です。");
                    Log.Write("HTMLテンプレートのフォーマットが不正です。\r\n" + ex);
                    break;
                }
                this.progressBar.Value = i + 1;
            }
            this.resultHtmlTextBox.Text = htmlSb.ToString();
        }

        private static string convertHtml(string srcPath, string suffix, string template)
        {
            string srcFileName = Path.GetFileName(srcPath);
            string destFileName = Path.GetFileNameWithoutExtension(srcPath) + suffix + Path.GetExtension(srcPath);
            string srcFileNameWithoutExtension = Path.GetFileNameWithoutExtension(srcPath);
            string extension = Path.GetExtension(srcPath);
            return string.Format(template, srcFileName, destFileName, srcFileNameWithoutExtension, extension);

        }

        private void resizeModePulldown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.resizeModePulldown.SelectedIndex == 0)
            {
                this.currentResizeMode = ResizeMode.AdjustToWidth;
                this.resizedHeightText.Enabled = false;
            }
            else if (this.resizeModePulldown.SelectedIndex == 1)
            {
                this.currentResizeMode = ResizeMode.FitIntoRectangle;
                this.resizedHeightText.Enabled = true;
            }

        }

        private void sameAsSrcFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (this.sameAsSrcFolder.Checked)
            {
                this.selectedOutputFolder = null;
                this.outputFolderLabel.Text = "元ファイルと同じフォルダに出力します";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings newSettings = GetSettingsFromWindow();

            if (!SettingsManager.CompareSettings(this.settings, newSettings))
            {
                DialogResult result = MessageBox.Show("設定値を上書き保存しますか?", "ImageShrinker",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Exclamation,
                                                      MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    WriteSettings(newSettings);
                }
            }
        }

        private void WriteSettings(Settings settings)
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                SettingsManager.WriteSettings(settings, Path.Combine(baseDir, SettingsFileName),
                                              Path.Combine(baseDir, HtmlTemplateFileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show("設定ファイルの保存時にエラーが発生しました。", "ImageShrinker");
                Log.Write("設定ファイルの保存時にエラーが発生しました。\r\n" + ex);
            }
            Log.Write("ImageShrinkerを終了します。");

        }
    }
}
