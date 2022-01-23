using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Logger
{
    public class FileLogger : BaseLogger
    {
        public object ClassName { get; set; }
        public DateTime dateTime;
        public FileStream file;
        public StreamWriter writer;


        public FileLogger(string Path, object Name)
        {
            ClassName = Name;
            file = File.Open(Path, FileMode.Open, FileAccess.Write, FileShare.None);
            writer = new StreamWriter(file);
        }
        public override void Log(LogLevel logLevel, string message)
        {
            dateTime = DateTime.Now;
            string appendLine = dateTime + " " + ClassName + " "
                + logLevel + " " + message;
            writer.WriteLine(appendLine);
            Console.WriteLine(appendLine);
        }
    }
}
