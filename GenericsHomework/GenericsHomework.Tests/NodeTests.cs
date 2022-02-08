using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericsHomework.Tests
{
    [TestClass]
    public class NodeTests<TValue>
    {
        [TestMethod]
        public void InitializeNodeClass_Success()
        {
            string item = "Item";
            Node<string> node = new(item);

            Assert.IsNotNull(node);
            Assert.IsNotNull(node.Item);
            Assert.IsTrue(node.Item.Equals(item));

        }

        [TestMethod]
        public void AppendToLinkedList_Success()
        {
            Node<string> node = new("item");
        }
    }
}