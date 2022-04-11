using DoubleLinkedList_UnitTests.ListClass;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace DoubleLinkedList_UnitTests
{
    public class Tests
    {
        string RootDirectory = Directory.GetCurrentDirectory();
        string Filename = "\\Files\\testfile.dat";
        new List<string> Items;

        [SetUp]
        public void Setup()
        {
            Items = new List<string>() { "1", "2", "3", "4", "5" };
        }

        [Test]
        public void SerializationAndDeserializationWorkCorrectly()
        {
            var doubleLinkedList = new ListRandom();
            var doubleLinkedList_New = new ListRandom();
            foreach (var i in Items)
            {
                doubleLinkedList.ListNodeAdd(i);
            }

            doubleLinkedList.Serialize(File.Open(RootDirectory + Filename, FileMode.OpenOrCreate));
            doubleLinkedList_New.Deserialize(File.Open(RootDirectory + Filename, FileMode.Open));

            for (var i = 0; i < doubleLinkedList.Count; i++)
                Assert.AreEqual(doubleLinkedList.GetNodeAt(i), doubleLinkedList_New.GetNodeAt(i), "Error: Lists not equal");
        }
    }
}