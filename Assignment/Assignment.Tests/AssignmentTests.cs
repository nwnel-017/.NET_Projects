using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment.Tests
{
    [TestClass]
    public class AssignmentTests
    {
        [TestMethod]
        public void InitializeSampleData_Success()
        {
            SampleData sampleData = new();
            Assert.IsNotNull(sampleData);
        }
    }
}