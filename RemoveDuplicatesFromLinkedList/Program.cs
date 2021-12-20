using System;
using System.Collections.Generic;
using System.Linq;

namespace RemoveDuplicatesFromLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Remove Duplicates From Linked List");

            ListNodeHead listNodeHead = new ListNodeHead(777);
            int[] childNodeValues = new int[] { 7, 11, 22, 34, 47, 56, 65, 34, 47, 74, 74, 11, 83, 92, 11 };


            childNodeValues.ToList()
                .ForEach(childNodeValue =>
                {
                    listNodeHead.AppendChild(childNodeValue);
                });

            Console.WriteLine("Linked list before removing duplicates :");
            PrintList(listNodeHead);

            // search for nodes to determine if any duplicte nodes are found
            // if so, remove the duplicates.
            childNodeValues.ToList()
                .ForEach(searchNodeValue =>
                {
                    IEnumerable<ListNodeChild> searchResult = listNodeHead.GetChildrenWithValue(searchNodeValue);

                    if(searchResult.Count() > 1)
                    {
                        Console.WriteLine($"Found : {searchResult.Count()} nodes with the value : {searchNodeValue}");
                        listNodeHead.RemoveDuplicateChildNodeWithValue(searchNodeValue);
                    }
                });


            Console.WriteLine("Linked list AFTER removing duplicates :");
            PrintList(listNodeHead);

            Console.ReadLine();
        }

        // Function to print nodes in a
        // given linked list
        static void PrintList(ListNodeHead listNodeHead)
        {
            Console.WriteLine(listNodeHead.Print());
            
            foreach (ListNodeChild listNodeChild in listNodeHead.ChildNodes)
            {                
                Console.WriteLine(listNodeChild.Print());
            }
        }
    }
    
  
}
