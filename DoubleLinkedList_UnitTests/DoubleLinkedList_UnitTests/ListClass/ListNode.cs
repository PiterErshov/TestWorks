using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DoubleLinkedList_UnitTests.ListClass
{
    public class ListNode
    {
        public ListNode Previous;
        public ListNode Next;
        public ListNode Random;
        public string Data;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            ListNode p = (ListNode)obj;

            return p.Data == Data &&
                   p.Random.Data == Random.Data;
        }
    }

    public class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count = 0;

        public void ListNodeAdd(string data) // added new node
        {
            var new_node = new ListNode();
            new_node.Data = data;


            if (Count == 0)
            {
                new_node.Random = new_node;
                Head = new_node;
                Tail = Head;
            }
            else
            {
                var rand = new Random().Next(0, Count);
                new_node.Random = GetNodeAt(rand);
                Tail.Next = new_node;
                new_node.Previous = Tail;
                Tail = new_node;
            }
            Count++;
        }

        public ListNode GetNodeAt(int i) // get node by index
        {
            int index = 0;
            for (var currentNode = Head; currentNode != null; currentNode = currentNode.Next)
            {
                if (index == i)
                    return currentNode;
                index++;
            }
            return new ListNode();
        }

        public int GetNodeIndex(string data) // get node index by data
        {
            int index = 0;
            for (var currentNode = Head; currentNode != null; currentNode = currentNode.Next)
            {
                if (data == currentNode.Data)
                    return index;
                index++;
            }
            return -1;
        }

        public void ListPrint() // print all nodes
        {
            var index = 0;
            for (var currentNode = Head; currentNode != null; currentNode = currentNode.Next)
            {
                Console.WriteLine($"List №{index} = {currentNode.Data}; rand = {currentNode.Random.Data}\n");
                index++;
            }
        }

        public void Serialize(Stream s)
        {
            using (var writer = new BinaryWriter(s))
            {
                for (var currentNode = Head; currentNode != null; currentNode = currentNode.Next)
                {
                    writer.Write(currentNode.Data);
                    writer.Write(GetNodeIndex(currentNode.Random.Data));
                }
            }
        }

        public void Deserialize(Stream s)
        {
            var indices = new List<int>();
            int i = 0;
            using (var reader = new BinaryReader(s))
            {
                while (reader.PeekChar() != -1)
                {
                    ListNodeAdd(reader.ReadString());
                    indices.Add(reader.ReadInt32());
                }
            }

            for (var currentNode = Head; currentNode != null; currentNode = currentNode.Next)
            {
                currentNode.Random = GetNodeAt(indices.ElementAt(i));
                i++;
            }
        }
    }
}
