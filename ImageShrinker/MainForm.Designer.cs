
namespace ImageShrinker
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.fileChooserButton = new System.Windows.Forms.Button();
            this.resizedWidthLabel = new System.Windows.Forms.Label();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.htmlTemplateExplainLabel = new System.Windows.Forms.Label();
            this.htmlTemplateTextBox = new System.Windows.Forms.TextBox();
            this.htmlTemplateLabel = new System.Windows.Forms.Label();
            this.resizeModePulldown = new System.Windows.Forms.ComboBox();
            this.resizeModeLabel = new System.Windows.Forms.Label();
            this.overwriteCheckBox = new System.Windows.Forms.CheckBox();
            this.suffixText = new System.Windows.Forms.TextBox();
            this.suffixLabel = new System.Windows.Forms.Label();
            this.resizedHeightText = new System.Windows.Forms.TextBox();
            this.resizedHeightLabel = new System.Windows.Forms.Label();
            this.resizedWidthText = new System.Windows.Forms.TextBox();
            this.fileChoosedLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.resizeButton = new System.Windows.Forms.Button();
            this.resultHtmlTextBox = new System.Windows.Forms.TextBox();
            this.resultHtmlLabel = new System.Windows.Forms.Label();
            this.outputFolderChooserButton = new System.Windows.Forms.Button();
            this.outputFolderLabel = new System.Windows.Forms.Label();
            this.sameAsSrcFolder = new System.Windows.Forms.CheckBox();
            this.settingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileChooserButton
            // 
            this.fileChooserButton.Location = new System.Drawing.Point(20, 214);
            this.fileChooserButton.Name = "fileChooserButton";
            this.fileChooserButton.Size = new System.Drawing.Size(75, 23);
            this.fileChooserButton.TabIndex = 1;
            this.fileChooserButton.Text = "ファイル選択";
            this.fileChooserButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fileChooserButton.UseVisualStyleBackColor = true;
            this.fileChooserButton.Click += new System.EventHandler(this.fileChooserButton_Click);
            // 
            // resizedWidthLabel
            // 
            this.resizedWidthLabel.AutoSize = true;
            this.resizedWidthLabel.Location = new System.Drawing.Point(6, 28);
            this.resizedWidthLabel.Name = "resizedWidthLabel";
            this.resizedWidthLabel.Size = new System.Drawing.Size(81, 12);
            this.resizedWidthLabel.TabIndex = 2;
            this.resizedWidthLabel.Text = "リサイズ後の幅：";
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.htmlTemplateExplainLabel);
            this.settingsGroupBox.Controls.Add(this.htmlTemplateTextBox);
            this.settingsGroupBox.Controls.Add(this.htmlTemplateLabel);
            this.settingsGroupBox.Controls.Add(this.resizeModePulldown);
            this.settingsGroupBox.Controls.Add(this.resizeModeLabel);
            this.settingsGroupBox.Controls.Add(this.overwriteCheckBox);
            this.settingsGroupBox.Controls.Add(this.suffixText);
            this.settingsGroupBox.Controls.Add(this.suffixLabel);
            this.settingsGroupBox.Controls.Add(this.resizedHeightText);
            this.settingsGroupBox.Controls.Add(this.resizedHeightLabel);
            this.settingsGroupBox.Controls.Add(this.resizedWidthText);
            this.settingsGroupBox.Controls.Add(this.resizedWidthLabel);
            this.settingsGroupBox.Location = new System.Drawing.Point(12, 12);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(681, 196);
            this.settingsGroupBox.TabIndex = 3;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "設定";
            // 
            // htmlTemplateExplainLabel
            // 
            this.htmlTemplateExplainLabel.AutoSize = true;
            this.htmlTemplateExplainLabel.Location = new System.Drawing.Point(107, 172);
            this.htmlTemplateExplainLabel.Name = "htmlTemplateExplainLabel";
            this.htmlTemplateExplainLabel.Size = new System.Drawing.Size(454, 12);
            this.htmlTemplateExplainLabel.TabIndex = 12;
            this.htmlTemplateExplainLabel.Text = "{0}..元ファイル名　{1}..縮小後ファイル名　{2}..元ファイル名(拡張子なし)　{3}..拡張子(ピリオド含む)";
            // 
            // htmlTemplateTextBox
            // 
            this.htmlTemplateTextBox.Location = new System.Drawing.Point(109, 89);
            this.htmlTemplateTextBox.Multiline = true;
            this.htmlTemplateTextBox.Name = "htmlTemplateTextBox";
            this.htmlTemplateTextBox.Size = new System.Drawing.Size(566, 80);
            this.htmlTemplateTextBox.TabIndex = 11;
            // 
            // htmlTemplateLabel
            // 
            this.htmlTemplateLabel.AutoSize = true;
            this.htmlTemplateLabel.Location = new System.Drawing.Point(7, 97);
            this.htmlTemplateLabel.Name = "htmlTemplateLabel";
            this.htmlTemplateLabel.Size = new System.Drawing.Size(95, 12);
            this.htmlTemplateLabel.TabIndex = 10;
            this.htmlTemplateLabel.Text = "テンプレートHTML：";
            // 
            // resizeModePulldown
            // 
            this.resizeModePulldown.FormattingEnabled = true;
            this.resizeModePulldown.Location = new System.Drawing.Point(93, 60);
            this.resizeModePulldown.Name = "resizeModePulldown";
            this.resizeModePulldown.Size = new System.Drawing.Size(121, 20);
            this.resizeModePulldown.TabIndex = 9;
            this.resizeModePulldown.SelectedIndexChanged += new System.EventHandler(this.resizeModePulldown_SelectedIndexChanged);
            // 
            // resizeModeLabel
            // 
            this.resizeModeLabel.AutoSize = true;
            this.resizeModeLabel.Location = new System.Drawing.Point(8, 63);
            this.resizeModeLabel.Name = "resizeModeLabel";
            this.resizeModeLabel.Size = new System.Drawing.Size(75, 12);
            this.resizeModeLabel.TabIndex = 8;
            this.resizeModeLabel.Text = "リサイズモード：";
            // 
            // overwriteCheckBox
            // 
            this.overwriteCheckBox.AutoSize = true;
            this.overwriteCheckBox.Location = new System.Drawing.Point(516, 28);
            this.overwriteCheckBox.Name = "overwriteCheckBox";
            this.overwriteCheckBox.Size = new System.Drawing.Size(136, 16);
            this.overwriteCheckBox.TabIndex = 4;
            this.overwriteCheckBox.Text = "ファイルがあったら上書き";
            this.overwriteCheckBox.UseVisualStyleBackColor = true;
            // 
            // suffixText
            // 
            this.suffixText.Location = new System.Drawing.Point(416, 25);
            this.suffixText.Name = "suffixText";
            this.suffixText.Size = new System.Drawing.Size(77, 19);
            this.suffixText.TabIndex = 7;
            // 
            // suffixLabel
            // 
            this.suffixLabel.AutoSize = true;
            this.suffixLabel.Location = new System.Drawing.Point(304, 28);
            this.suffixLabel.Name = "suffixLabel";
            this.suffixLabel.Size = new System.Drawing.Size(106, 12);
            this.suffixLabel.TabIndex = 6;
            this.suffixLabel.Text = "ファイル名サフィックス：";
            // 
            // resizedHeightText
            // 
            this.resizedHeightText.Location = new System.Drawing.Point(209, 25);
            this.resizedHeightText.Name = "resizedHeightText";
            this.resizedHeightText.Size = new System.Drawing.Size(64, 19);
            this.resizedHeightText.TabIndex = 5;
            // 
            // resizedHeightLabel
            // 
            this.resizedHeightLabel.AutoSize = true;
            this.resizedHeightLabel.Location = new System.Drawing.Point(172, 28);
            this.resizedHeightLabel.Name = "resizedHeightLabel";
            this.resizedHeightLabel.Size = new System.Drawing.Size(31, 12);
            this.resizedHeightLabel.TabIndex = 4;
            this.resizedHeightLabel.Text = "高さ：";
            // 
            // resizedWidthText
            // 
            this.resizedWidthText.Location = new System.Drawing.Point(93, 25);
            this.resizedWidthText.Name = "resizedWidthText";
            this.resizedWidthText.Size = new System.Drawing.Size(64, 19);
            this.resizedWidthText.TabIndex = 4;
            // 
            // fileChoosedLabel
            // 
            this.fileChoosedLabel.AutoSize = true;
            this.fileChoosedLabel.Location = new System.Drawing.Point(103, 219);
            this.fileChoosedLabel.Name = "fileChoosedLabel";
            this.fileChoosedLabel.Size = new System.Drawing.Size(140, 12);
            this.fileChoosedLabel.TabIndex = 4;
            this.fileChoosedLabel.Text = "ファイルが選択されていません";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 324);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(672, 23);
            this.progressBar.TabIndex = 5;
            // 
            // resizeButton
            // 
            this.resizeButton.Location = new System.Drawing.Point(20, 272);
            this.resizeButton.Name = "resizeButton";
            this.resizeButton.Size = new System.Drawing.Size(149, 46);
            this.resizeButton.TabIndex = 6;
            this.resizeButton.Text = "リサイズ実行";
            this.resizeButton.UseVisualStyleBackColor = true;
            this.resizeButton.Click += new System.EventHandler(this.resizeButton_Click);
            // 
            // resultHtmlTextBox
            // 
            this.resultHtmlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultHtmlTextBox.Location = new System.Drawing.Point(118, 353);
            this.resultHtmlTextBox.Multiline = true;
            this.resultHtmlTextBox.Name = "resultHtmlTextBox";
            this.resultHtmlTextBox.Size = new System.Drawing.Size(566, 167);
            this.resultHtmlTextBox.TabIndex = 7;
            // 
            // resultHtmlLabel
            // 
            this.resultHtmlLabel.AutoSize = true;
            this.resultHtmlLabel.Location = new System.Drawing.Point(18, 356);
            this.resultHtmlLabel.Name = "resultHtmlLabel";
            this.resultHtmlLabel.Size = new System.Drawing.Size(65, 12);
            this.resultHtmlLabel.TabIndex = 8;
            this.resultHtmlLabel.Text = "結果HTML：";
            // 
            // outputFolderChooserButton
            // 
            this.outputFolderChooserButton.Location = new System.Drawing.Point(20, 243);
            this.outputFolderChooserButton.Name = "outputFolderChooserButton";
            this.outputFolderChooserButton.Size = new System.Drawing.Size(101, 23);
            this.outputFolderChooserButton.TabIndex = 9;
            this.outputFolderChooserButton.Text = "出力フォルダ選択";
            this.outputFolderChooserButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.outputFolderChooserButton.UseVisualStyleBackColor = true;
            this.outputFolderChooserButton.Click += new System.EventHandler(this.outputFolderChooserButton_Click);
            // 
            // outputFolderLabel
            // 
            this.outputFolderLabel.AutoSize = true;
            this.outputFolderLabel.Location = new System.Drawing.Point(127, 248);
            this.outputFolderLabel.Name = "outputFolderLabel";
            this.outputFolderLabel.Size = new System.Drawing.Size(268, 12);
            this.outputFolderLabel.TabIndex = 10;
            this.outputFolderLabel.Text = "出力フォルダ未選択(元ファイルと同じ場所に出力します)";
            // 
            // sameAsSrcFolder
            // 
            this.sameAsSrcFolder.AutoSize = true;
            this.sameAsSrcFolder.Location = new System.Drawing.Point(528, 243);
            this.sameAsSrcFolder.Name = "sameAsSrcFolder";
            this.sameAsSrcFolder.Size = new System.Drawing.Size(123, 16);
            this.sameAsSrcFolder.TabIndex = 11;
            this.sameAsSrcFolder.Text = "元ファイルと同じ場所";
            this.sameAsSrcFolder.UseVisualStyleBackColor = true;
            this.sameAsSrcFolder.CheckedChanged += new System.EventHandler(this.sameAsSrcFolder_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 532);
            this.Controls.Add(this.sameAsSrcFolder);
            this.Controls.Add(this.outputFolderLabel);
            this.Controls.Add(this.outputFolderChooserButton);
            this.Controls.Add(this.resultHtmlLabel);
            this.Controls.Add(this.resultHtmlTextBox);
            this.Controls.Add(this.resizeButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.fileChoosedLabel);
            this.Controls.Add(this.settingsGroupBox);
            this.Controls.Add(this.fileChooserButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "ImageShrinker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button fileChooserButton;
        private System.Windows.Forms.Label resizedWidthLabel;
        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.TextBox htmlTemplateTextBox;
        private System.Windows.Forms.Label htmlTemplateLabel;
        private System.Windows.Forms.ComboBox resizeModePulldown;
        private System.Windows.Forms.Label resizeModeLabel;
        private System.Windows.Forms.CheckBox overwriteCheckBox;
        private System.Windows.Forms.TextBox suffixText;
        private System.Windows.Forms.Label suffixLabel;
        private System.Windows.Forms.TextBox resizedHeightText;
        private System.Windows.Forms.Label resizedHeightLabel;
        private System.Windows.Forms.TextBox resizedWidthText;
        private System.Windows.Forms.Label fileChoosedLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button resizeButton;
        private System.Windows.Forms.TextBox resultHtmlTextBox;
        private System.Windows.Forms.Label resultHtmlLabel;
        private System.Windows.Forms.Button outputFolderChooserButton;
        private System.Windows.Forms.Label outputFolderLabel;
        private System.Windows.Forms.Label htmlTemplateExplainLabel;
        private System.Windows.Forms.CheckBox sameAsSrcFolder;
    }
}

