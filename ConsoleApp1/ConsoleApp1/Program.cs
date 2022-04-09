using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleApp1.ListClass;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //*
            var rootDirectory = Directory.GetCurrentDirectory();
            var filename = "\\Files\\testfile.dat";
            var doubleLinkedList = new ListRandom();
            var doubleLinkedList_New = new ListRandom();
            var items = new List<string>() {"1", "2", "3", "4", "5"};
            foreach (var i in items)
            {
                doubleLinkedList.ListNodeAdd(i);
            }
            //doubleLinkedList.ListPrint();
            doubleLinkedList.Serialize(File.Open(rootDirectory + filename, FileMode.OpenOrCreate));
            doubleLinkedList_New.Deserialize(File.Open(rootDirectory + filename, FileMode.Open));
            //*/
            doubleLinkedList.ListPrint();
            Console.WriteLine("-------------\n");
            doubleLinkedList_New.ListPrint();
            Console.ReadLine();
        }
    }
}