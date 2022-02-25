using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Node<TValue>: IEnumerable<Node<TValue>>, IEnumerable
    {
        public TValue Item { get; set; }
        public Node<TValue> Next { get; private set; }

        public Node(TValue item)
        {
            Item = item;
            Next = this;
        }

        public void Append(TValue item)
        {
            if (Exists(item))
            {
                throw new ArgumentException(message: "This value is already in Linked List");
            }

            Node<TValue> newNode = new(item);

            newNode.Next = Next;
            Next = newNode;
        }

        public IEnumerator<Node<TValue>> GetEnumerator()
        {
            Node<TValue> cur = this;

            while(cur.Next != this)
            {
                yield return cur;
                cur = cur.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<Node<TValue>> ChildItems(int maximum)
        {
            Node<TValue> cur = this;
            int count = 0;
            while (count < maximum)
            {
                yield return cur;
                cur = cur.Next;
                count++;
            }
        }

        public void Clear()
        {
            //We do not need to worry about garbage collection for this method
            //Once the linked list has been cleared, the system will
            //take care of automatically deleting the freed nodes
            Node<TValue> head = this;
            Node<TValue> cur = this;

            if (cur.Next.Equals(head))
            {
                Next = this; ;
            }
            else
            {
                while (cur.Next != null && cur.Next.Equals(head) == false)
                {
                    cur = cur.Next;
                }
                cur.Next = cur;
                head.Next = head;
            }
        }

        public bool Exists(TValue item)
        {
            Node<TValue> current = this;
            do
            {
                if (current.Item is not null && current.Item.Equals(item))
                {
                    return true;
                }
                current = current.Next;
            } while (current != this);

            return false;
        }

        public override string? ToString()
        {
            if (Item == null)
            {
                return "";
            }
            return Item.ToString();
        }
    }
}
