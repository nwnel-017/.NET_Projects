using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using System.Text;

namespace Logger.Tests
{
    [TestClass]
    public class FileLoggerTests
    {
        public FileLogger? logger;
        
        [TestMethod]
        public void Initialize_FileLogger()
        {
            var path = @"C:\Users\natha\source\repos\Assignment2\Logger.Tests\test.txt";
            logger = new FileLogger(path, nameof(FileLoggerTests));
            Assert.IsNotNull(logger);
        }

        [TestMethod]
        public void FileLogger_WriteToFile()
        {
            var path = @"C:\Users\natha\source\repos\Assignment2\Logger.Tests\test.txt";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }

            if (logger != null)
            {
                logger.Log(LogLevel.Debug, "This is a test");
                if (new FileInfo(path).Length == 0)
                    throw new Exception();
            }
            
        }


    }
}
