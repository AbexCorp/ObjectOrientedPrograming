using System;
using System.Text;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        Node<int> head = null;
        PrintSingleLinkedList<int>(head);
        DistinctElementsInLinkedList<int>(ref head);
        PrintSingleLinkedList<int>(head);

        Console.WriteLine();
        Console.WriteLine();
    }

    public static void PrintSingleLinkedList<T>( Node<T> head )
    {
        Console.Write("head -> ");
        while ( head != null )
        {
            Console.Write(head.ToString());
            head = head.Next;
        }
        Console.WriteLine("null");
    }

    public static void AddAtEndOfSingleLinkedList<T>(T element, ref Node<T> head)
    {
        if(head == null)
        {
            head = new Node<T>(element);
            return;
        }
        Node<T> previousNode = head;
        Node<T> node = head;
        while ( node != null )
        {
            previousNode = node;
            node = node.Next;
        }
        previousNode.Next = new Node<T>(element);
    }

    public static Node<T> CreateSingleLinkedList<T>(params T[] arr)
    {
        if (arr == null || arr.Length == 0)
            return null;

        Node<T> list = new Node<T>(arr[0]);
        for(int i = 1; i < arr.Length; i++)
        {
            AddAtEndOfSingleLinkedList(arr[i], ref list);
        }
        return list;
    }

    public static Node<T> ReverseSingleLinkedList<T>( Node<T> head)
    {
        if (head == null || head.Next == null)
            return head;

        Node<T> prevNode = null;
        Node<T> node = head;
        Node<T> nextNode = head.Next;

        while( nextNode != null )
        {
            nextNode = node.Next;
            node.Next = prevNode;
            prevNode = node;
            node = nextNode == null ? node : nextNode;
        }
        head = node;
        return head;
    }

    public static void MoveLastNodeToFront<T>(ref Node<T> head)
    {
        if(head == null || head.Next == null)
            return;

        Node<T> startNode = head;
        Node<T> secondToLastNode = head;
        Node<T> lastNode;

        while (secondToLastNode.Next.Next != null)
        {
            secondToLastNode = secondToLastNode.Next;
        }
        lastNode = secondToLastNode.Next;
        secondToLastNode.Next = null;
        lastNode.Next = startNode;
        head = lastNode;
    }

    public static void RemoveNodeAt<T>(int position, ref Node<T> head)
    {
        if(head == null || (head.Next == null && position > 1))
            return;
        if (position == 0)
            head = head.Next;

        int counter = 0;
        Node<T> previousNode = head;
        Node<T> nodeToDelete = head;

        while (nodeToDelete.Next != null || counter == position)
        {
            counter++;
            nodeToDelete = nodeToDelete.Next;

            if (counter == position)
                break;

            previousNode = nodeToDelete;

            if (counter > position)
                return;
        }
        previousNode.Next = nodeToDelete.Next;
    }

    public static void DistinctElementsInLinkedList<T>(ref Node<T> head)
        where T : IEquatable<T>, IComparable<T>
    {
        if (head == null || head.Next == null)
            return;

        Node<T> current = head;
        Node<T> prev = null;

        T[] seenValues = new T[1000];
        int count = 0;

        while (current != null)
        {
            bool isDuplicate = false;

            for (int i = 0; i < count; i++)
            {
                if (current.Data.Equals(seenValues[i]))
                {
                    isDuplicate = true;
                    break;
                }
            }

            if (isDuplicate)
                prev.Next = current.Next;
            else
            {
                seenValues[count++] = current.Data;
                prev = current;
            }

            current = current.Next;
        }
    }

    public static void RemoveAllDuplicatesFromSortedLinkedList<T>(ref Node<T> head)
        where T : IEquatable<T>, IComparable<T>
    {
        if (head == null || head.Next == null)
            return;

        Node<T> dummy = new Node<T>(default(T), head);
        Node<T> prev = dummy;
        Node<T> current = head;

        while (current != null)
        {
            if (current.Next != null && current.Data.Equals(current.Next.Data))
            {
                T duplicateValue = current.Data;

                while (current != null && current.Data.Equals(duplicateValue))
                {
                    current = current.Next;
                }

                prev.Next = current;
            }
            else
            {
                prev = current;
                current = current.Next;
            }
        }
        head = dummy.Next;
    }

    //public static void MoveLastNodeToFront<T>(ref Node<T> head)
    //{
    //    if(head == null || head.Next == null)
    //        return;

    //    Node<T> startNode = head;
    //    Node<T> lastNode = head;

    //    while (lastNode.Next != null)
    //    {
    //        lastNode = lastNode.Next;
    //    }
    //    T temp = startNode.Data;
    //    startNode.Data = lastNode.Data;
    //    lastNode.Data = temp;
    //}
}