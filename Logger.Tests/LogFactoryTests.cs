using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Logger.Tests
{
    [TestClass]
    public class LogFactoryTests
    {
        [TestMethod]
        public void CreateLogger_WithLogFactoryNoPath()
        {
            var factory = new LogFactory();
            var testLogger = factory.CreateLogger();
            Assert.IsNull(testLogger);
        }

        [TestMethod]
        public void CreateLogger_WithLogFactory_UsingGivenPath()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\test.txt");
            var factory = new LogFactory();
            factory.ConfigureFileLogger(path);
            var testLogger = factory.CreateLogger();
            Assert.IsNotNull(testLogger);
        }
    }
}
