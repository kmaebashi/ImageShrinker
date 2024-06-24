using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageShrinker.Logic
{
    public static class Log
    {
        private static StreamWriter logStream = null;

        public static void Write(string message)
        {
            if (logStream == null)
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string logDir = Path.Combine(baseDir, "logs");
                string logFileName = "log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                string logPath = Path.Combine(logDir, logFileName);

                logStream = new StreamWriter(logPath);
            }
            logStream.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " " + message);
            logStream.Flush();
        }
    }
}
