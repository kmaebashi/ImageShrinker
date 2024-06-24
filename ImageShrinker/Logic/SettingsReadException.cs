using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShrinker.Logic
{
    public class SettingsReadException : Exception
    {
        public SettingsReadException(string message)
            : base(message)
        {

        }
    }
}
