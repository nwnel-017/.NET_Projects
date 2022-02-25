using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Tests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void InitializeNodeClass_Success()
        {
            string item = "Item";
            Node<string> node = new(item);

            Assert.IsNotNull(node);
            Assert.AreEqual(node.Item, item);
        }

        [TestMethod]
        public void AppendToLinkedList_Success()
        {
            Node<string> node = new("item");

            string itemToAppend = "this is data";

            node.Append(itemToAppend);

            Assert.AreNotEqual(node, node.Next);

            Node<string> nodeAfter = node.Next;
            Assert.AreEqual(nodeAfter.Item, itemToAppend);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AppendToLinkedList_WithExistingValue_Fail()
        {
            Node<string> node = new("item");
            node.Append("item");
        }

        [TestMethod]
        public void ClearLinkedList_Success()
        {
            Node<string> node = new("value");
            node.Append("value 2");
            Assert.AreNotEqual(node, node.Next);

            node.Clear();
            Assert.AreEqual(node, node.Next);
        }

        [TestMethod]
        public void ClearLinkedList_AreOldNodesDeleted_Success()
        {
            Node<string> node = new("hey there");
            node.Append("sup");
            node.Append("hello");
            node.Append("string");

            node.Clear();
            Assert.IsFalse(node.Exists("sup"));
            Assert.IsFalse(node.Exists("hello"));
            Assert.IsFalse(node.Exists("string"));
        }

        [TestMethod]
        public void DoesValueExist_WithOneNode_Failure()
        {
            Node<string> node = new("this Node");
            Assert.IsFalse(node.Exists("hey there"));
        }

        [TestMethod]
        public void DoesValueExist_WithMultipleNodes_Failure()
        {
            Node<string> node = new("1");
            node.Append("2");
            node.Append("3");
            node.Append("4");

            Assert.IsFalse(node.Exists("my name is Nate Nelson"));

        }

        [TestMethod]
        public void DoesValueExist_WhenCurrentNode_Success()
        {
            Node<string> node = new("1");
            node.Append("2");
            node.Append("3");
            node.Append("4");

            Assert.IsTrue(node.Exists("1"));
        }

        [TestMethod]
        public void DoesValueExist_NotCurrentNode_SuccessWithTwoNodes()
        {
            Node<string> node = new("1");
            node.Append("2");

            bool result = node.Exists("2");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DoesValueExist_NotCurrentNode_WithMultipleNodes_Success()
        {
            Node<string> node = new("1");
            node.Append("2");
            node.Append("3");
            node.Append("4");

            Assert.IsTrue(node.Exists("2"));
            Assert.IsTrue(node.Exists("3"));
            Assert.IsTrue(node.Exists("4"));
        }

        [TestMethod]//Test passed
        public void Part7_ChildItemsReturn_Success()
        {
            Node<string> myNode = new("First value");
            myNode.Append("Second value");
            myNode.Append("Third value");

            IEnumerable<Node<string>> result = myNode.ChildItems(5);

            Assert.AreEqual(5, result.Count());
        }

        [TestMethod]
        public void Part7_GetEnumerator_ReturnsAllNodes_Success()
        {
            Node<int> myNode = new(1);
            myNode.Append(2);
            myNode.Append(3);
            myNode.Append(4);

            foreach(Node<int> node in myNode)//Test to see if Nodes can be incremented through as collection
            {
                Assert.IsNotNull(node.Item);
            }
        }
    }
}
