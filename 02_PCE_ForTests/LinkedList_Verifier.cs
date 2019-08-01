using System;
using System.IO; // For the Console.Out stuff
using System.Text; // For StringBuilder

using NUnit.Framework;

/*
 * This file contains helper classes for the tests.  It does NOT contain any tests itself.
 * 
 * These helper routines are put here, in a separate file, so that it's easy to
 * copy-and-paste this single file between all the different starter projects that get
 * handed out, and yet still have a single, coherent copy of the code.
 * (Yeah, there's probably a better way to do this, but I wanted to keep things simple
 * for y'all :)  )
 */

namespace PCE_StarterProject
{
    public class LinkedList_Verifier : MyIntList
    {
        private string errorMessage = "";

        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializedLL"></param>
        /// <returns>True, if the list matches what's in the array
        ///         False otherwise (including if the array is null)</returns>
        public bool ValidateLinkedList(int[] serializedLL)
        {
            Console.WriteLine("Beginning list validation");

            if (serializedLL == null)
            {
                Console.WriteLine("TEST ERROR: Given null array to validate against!");
                return false; // probably an error in the test, but better to notice
                // an extra failure than to pass a bad test
            }
            if (serializedLL.Length == 0 && this.first == null)
            {
                Console.WriteLine("Both the list, and the array, contain no elements!");
                return true;
            }
            else if (this.first == null && serializedLL.Length > 0)
            {
                Console.WriteLine("The list it empty, but the array is not!");
                return false;
            }
            else
            {
                IntListNode cur = this.first;
                int i = 0;

                while (cur != null && i < serializedLL.Length)
                {
                    Console.WriteLine("Checking node {0}\tExpecting to find {1}\t\tActually found:{2}",
                        i, serializedLL[i], cur.Data);

                    if (cur.Data != serializedLL[i])
                        return false;

                    cur = cur.Next;
                    i++;
                }
                if (i == serializedLL.Length && cur == null) // got to the end of both
                {
                    Console.WriteLine("Validated all items in the list/array, with no extras in either");
                    return true;
                }

                // must have run out of nodes, or numbers, too early
                return false;
            }
        }
    }
}