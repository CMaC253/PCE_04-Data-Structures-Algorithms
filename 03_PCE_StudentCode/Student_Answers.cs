
#define TESTING
using System;

/*
 * STUDENTS: Your answers (your code) goes into this file!!!!
 * 
  * NOTE: In addition to your answers, this file also contains a 'main' method, 
 *      in case you want to run a normal console application.
 * 
 * If you want to / have to put something into 'Main' for these PCEs, then put that 
 * into the Program.Main method that is located below, 
 * then select this project as startup object 
 * (Right-click on the project, then select 'Set As Startup Object'), then run it 
 * just like any other normal, console app: use the menu item Debug->Start Debugging, or 
 * Debug->Start Without Debugging
 * 
 */

namespace PCE_StarterProject
{
    public class UnderflowException : Exception
    { public UnderflowException(string s) : base(s) { } }
    public class OverflowException : Exception
    { public OverflowException(string s) : base(s) { } }


    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, world!");
        }
    }

    // Is this where the IntListNode should go?
    //public class IntListNode // should this class be marked public?
    //{                           // HINT: private is NOT the correct access for this class

    //    public int Data; // should be public (which makes sense, once you've moved it & changed it to not be public)
    //    public IntListNode Next; // should also be public
    //}

    public class MyIntList
    {
        public class IntListNode
        {
            public int Data;
            public IntListNode Next;

            public IntListNode(int data)
            {
                Data = data;
                // m_next = null; // automatically done by CLR, so I don't have to
            }

            public IntListNode(int data, IntListNode next)
                : this(data)
            {
                Next = next;
            }
        }

        // instance variables, if any
        protected IntListNode first; // protected so that the LinkedList_Verifier can get to it

        // constructor(s), if any

        // Implement this!
        public void AddToFront(int numToAdd)
        {
            if(first == null)
            {
                IntListNode nNode;
                nNode = new IntListNode(numToAdd);
                nNode = first;
            }

            IntListNode dupNode;
            dupNode = new IntListNode(numToAdd);
            dupNode.Next = first;
            first = dupNode;
        }

        public void PrintAll()
        {
            IntListNode cur = first;
            while(cur!=null)
            {
                Console.WriteLine(cur.Data);
                cur = cur.Next;
            }
          
        }
        public int RemoveFromFront()
        {
            if (first == null)
                return Int32.MinValue;

            int x = first.Data;
            first = first.Next;
            return x;
        }
        public void PrintNode(uint i)
        {
            int counter = 0;
            IntListNode temp = first;
            while(temp!=null && counter<i)
            {
                temp = temp.Next;
                counter++;
            }
            if(temp!=null)
            Console.WriteLine(temp.Data);

        }
    }




    public class SmartArrayAOD
    {
        int[] rgNums;
        private const int MINIMUM_STARTING_SIZE = 5;
        public SmartArrayAOD(){  }
        public SmartArrayAOD(int startingSize)
        {
            rgNums = new int[startingSize];
        }
        //O(1) just accesses one array element to change it
        public void SetAtIndex(int idx, int val)
        {
            if (rgNums == null)
            {
                int highest = Math.Max(MINIMUM_STARTING_SIZE, idx); // this will check if the array starting size needs to be larger than 5
                rgNums = new int[highest]; // initilizes rgNums to be the higher of the two numbers
            }
            if(idx>rgNums.Length-1)
            {
                int[] newArray = new int[idx +1];
                for (int i = 0; i < rgNums.Length; i++)
                {
                    newArray[i] = rgNums[i];
                }
                rgNums = newArray;
            }
            if (idx < rgNums.Length && idx >= 0)
            {
                rgNums[idx] = val;
            }

            if (idx < 0)
                throw new UnderflowException("Index value is too low");
            if (idx > rgNums.Length - 1)
                throw new OverflowException("Index value is too high");
            
            //else
            //    rgNums[idx] = val;
        }
        //O(1) just accesses one array element to change it
        public int GetAtIndex(int idx)
        {
            if (rgNums == null)
                throw new OverflowException("Nothing in the array");

            if (idx < 0)
                throw new UnderflowException("Index value is too low");
            if (idx >= rgNums.Length)
                throw new OverflowException("Index value is too high");
            if (idx < rgNums.Length && idx >= 0)
                return rgNums[idx];
            else
                return Int32.MinValue;
        }
        //O(n) tries all elements in the array.
        public void PrintAllElements()
        {
            if (rgNums == null)
                Console.WriteLine("ARRAY NOT YET ALLOCATED");
            else
            for (int i = 0; i < rgNums.Length; i++)
                Console.WriteLine(rgNums[i]);

        }
        public bool Find(int val)
        {
            
            if (rgNums == null)
                return false;

           
            for (int i = 0; i < rgNums.Length; i++)
            {
                if (val == rgNums[i])
                    return true;    
            }
            return false;
        }
        public int getSize()
        {
            if (rgNums == null)
                return 0;

            return rgNums.Length;
        }
        

        
    }
   
}