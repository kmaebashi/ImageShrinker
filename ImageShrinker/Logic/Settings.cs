using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShrinker.Logic
{
    public class Settings
    {
        public int? ResizedWidth { get; private set; }
        public int? ResizedHeight { get; private set; }
        public string Suffix { get; private set; }
        public bool OverwriteFlag { get; private set; }
        public ResizeMode CurrentResizeMode { get; private set; }
        public string HtmlTemplate { get; set; }
        public string DefaultInputFolder { get; private set; }
        public string OutputFolder { get; private set; }
        public bool SameAsSrcFolder { get; private set; }

        public Settings(int? resizedWidth, int? resizedHeight, string suffix,
            bool overwriteFlag, ResizeMode currentResizeMode,
            string defaultInputFolder, string outputFolder, bool sameAsSrcFolder)
        {
            this.ResizedWidth = resizedWidth;
            this.ResizedHeight = resizedHeight;
            this.Suffix = suffix;
            this.OverwriteFlag = overwriteFlag;
            this.CurrentResizeMode = currentResizeMode;
            this.DefaultInputFolder = defaultInputFolder;
            this.OutputFolder = outputFolder;
            this.SameAsSrcFolder = sameAsSrcFolder;
        }
    }
}
