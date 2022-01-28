using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using System.Text;

namespace Logger.Tests
{
    [TestClass]
    public class FileLoggerTests
    {
        
        [TestMethod]
        public void Initialize_FileLogger()
        {
            string path = @"C:\Users\natha\source\repos\Assignment2\Logger.Tests\test.txt";
            FileLogger logger = new(path, nameof(FileLoggerTests));
            Assert.AreEqual(logger.Path, path);
        }

        [TestMethod]
        public void FileLogger_WriteToFile_Success()
        {
            string path = @"C:\Users\natha\source\repos\Assignment2\Logger.Tests\test.txt";
            FileLogger logger = new(path, nameof(FileLoggerTests));

            System.IO.File.WriteAllText(path, string.Empty);

            logger.Log(LogLevel.Debug, "This is a test");
            if (new FileInfo(path).Length == 0)
                throw new Exception();

        }

        [TestMethod]
        /*[ExpectedException(typeof(Exception))]*/
        public void FileLogger_WriteToFile_WithBadPath()
        {
            string path = "this is a bad path";
            FileLogger logger = new(path, nameof(FileLoggerTests));

            logger.Log(LogLevel.Debug, "This shouldn't work");

        }
    }
}
