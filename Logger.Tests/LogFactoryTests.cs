using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;

namespace Logger.Tests
{
    [TestClass]
    public class LogFactoryTests
    {
        [TestMethod]
        public void LogFactory_ConfigureFileLogger_Success()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "\\test.txt");
            LogFactory factory = new();
            factory.ConfigureFileLogger(path);
            Assert.AreEqual(path, factory.Path);
        }

        [TestMethod]
        public void CreateLogger_WithLogFactoryNoPath()
        {
            LogFactory factory = new();

            FileLogger? testLogger = factory.CreateLogger();

            Assert.IsNull(testLogger);

        }

        [TestMethod]
        public void CreateLogger_WithLogFactory_UsingGivenPath_SuccessReturnsExistingLogger()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "\\test.txt");
            LogFactory testFactory = new();
            testFactory.ConfigureFileLogger(path);
            FileLogger? resultLogger = testFactory.CreateLogger();
            Assert.IsNotNull(resultLogger); 
        }

        [TestMethod]
        public void CreateLogger_WithGivenPath_CreatesLoggerCorrectly()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "\\test.txt");
            LogFactory testFactory = new();
            testFactory.ConfigureFileLogger(path);
            FileLogger? resultLogger = testFactory.CreateLogger();

            FileLogger expectedLogger = new(path, nameof(LogFactory));
           
            Assert.IsTrue(expectedLogger.Path == resultLogger.Path);
        }
    }
}
