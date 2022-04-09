using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace ConsoleApp1.ListClass
{
    public class ListNode
    {
        public ListNode Previous;
        public ListNode Next;
        public ListNode Random;
        public string Data;
    }

    public class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count = 0;

        public void ListNodeAdd(string data)
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

        public ListNode GetNodeAt(int i)
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

        public int GetNodeIndex(string data)
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

        public void ListPrint()
        {
            var index = 0;            
            for (var currentNode = Head; currentNode != null; currentNode = currentNode.Next)
            {
                Console.WriteLine($"List №{index} = {currentNode.Data}; rand = {currentNode.Random.Data}\n");
                index++;
            }
        }

        public void ListPrintRand()
        {
            var index = 0;
            for (var currentNode = Head; currentNode != null; currentNode = currentNode.Next)
            {
                Console.WriteLine($"List №{index} = {currentNode.Random.Data}\n");
                index++;
            }
        }

        public void Serialize(Stream s)
        {
            using (var writer = new BinaryWriter(s))
            {
                for(var currentNode = Head; currentNode != null; currentNode = currentNode.Next)
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
