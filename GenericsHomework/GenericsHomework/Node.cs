namespace GenericsHomework
{
    public class Node<TValue>
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
            Node<TValue> currentNode = Next;

            while(currentNode != this)
            {
                currentNode = currentNode.Next;
            }
            currentNode.Next = newNode;
            newNode.Next = this;
        }

        public void Clear()
        {
            Next = this;
        }

        public bool Exists(TValue item)
        {
            if (Item != null && Item.Equals(item))
            {
                return true;
            }
            Node<TValue> currentNode = Next;
            
            while(currentNode.Equals(this) == false)
            {
                if(currentNode.Item != null && currentNode.Item.Equals(item))
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
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