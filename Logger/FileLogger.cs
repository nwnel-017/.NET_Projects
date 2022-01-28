using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Logger
{
    public class FileLogger : BaseLogger
    {
        public object? ClassName { get; set; }
        public DateTime? Date;
        public string? Path;


        public FileLogger(string path, object name)
        {
            ClassName = name;
            Path = path;
        }
        public override void Log(LogLevel logLevel, string message)
        {
            Date = DateTime.Now;
            string appendLine = Date + " " + ClassName + " "
                + logLevel + " " + message + "\n";
           
                StreamWriter writer = File.AppendText(Path);
                writer.WriteLine(appendLine);
                Console.WriteLine(appendLine);
                writer.Close();
        }
    }
}
