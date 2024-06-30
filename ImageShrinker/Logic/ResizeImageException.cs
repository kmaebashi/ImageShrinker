using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShrinker.Logic
{
    class ResizeImageException : Exception
    {
        public ResizeImageException(string message)
            : base(message)
        {

        }
    }
}
