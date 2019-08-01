using System;
using System.IO;
using NUnit.Framework;

/*
 * This file contains all the tests that will be run.
 * 
 * If you want to find out what a test does (or why it's failing), look in here
 * 
 */

namespace PCE_StarterProject
{
    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_DESC)] // tags this as an exercise to be graded...
    class Test_SmartArrayAOD
    {
        protected SmartArrayAOD sa;
        const int SMART_ARRAY_STARTING_SIZE = 5;

        [SetUp]
        virtual protected void SetUp()
        {
            sa = new SmartArrayAOD();
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void SetAtIndex_InBounds([Values(0, 1, 2, 3, 4)]int index)
        {
            sa.SetAtIndex(index, 10);

            Console.WriteLine("Test Passed: Able to set element {0}!", index);
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void SetAtIndex_Force_Resize([Values(5, 6, 10)]int index)
        {
            // These should still work, because the SmartArrayAOD should re-allocate a new array
            // when given an index that's larger than the the current 'high' value.
            sa.SetAtIndex(index, 10);

            Console.WriteLine("Test Passed: Correctly unable to set element {0}!", index);
        }

        [Test]
        [Category("SmartArrayAOD")]
        [ExpectedException("PCE_StarterProject.UnderflowException")]
        public void SetAtIndex_OutOfBounds_Lo([Values(-1, -2, -10)]int index)
        {
            Console.WriteLine("================= SetAtIndex =================");
            sa.SetAtIndex(index, 10);

            Console.WriteLine("Test Passed: Correctly unable to set element {0}!", index);
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void Print_All_Elts_Empty()
        {
            TestHelpers th = new TestHelpers();

            th.StartOutputCapturing();
            sa.PrintAllElements();
            String sResult = th.StopOutputCapturing();

            Console.WriteLine("hello!");

            string sCorrect = "ARRAY NOT YET ALLOCATED";

            Console.WriteLine("Expected the output\n" + sCorrect + "\nActually got:\n" +
                sResult + "END OF YOUR OUTPUT\n(END OF YOUR OUTPUT was added so it's clear what your output ends");

            bool theSame = TestHelpers.EqualsFuzzyString(sCorrect, sResult);
            Assert.That(theSame == true, "Expected the output\n" + sCorrect + "\nBut actually got:\n" +
                sResult + "END OF YOUR OUTPUT\n(END OF YOUR OUTPUT was added so it's clear what your output ends");
        }

        [Test]
        [Category("SmartArrayAOD")]
        // Call this with an array of 4 elements (the minimum to allocate is 5, so last value should stay zero),
        // then again with ~20
        public void Print_All_Elts_Set( [Values( new int[] { 10, 20, 30, 40 },
                                                 new int[] { 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200} )]
                                    int []nums)
        {
            int i;

            for (i = 0; i < nums.Length; i++)
            {
                sa.SetAtIndex(i, nums[i]);
            }

            TestHelpers th = new TestHelpers();

            th.StartOutputCapturing();
            sa.PrintAllElements();
            String sResult = th.StopOutputCapturing();

            StringWriter sw = new StringWriter();
            for (i = 0; i < nums.Length; i++)
                sw.WriteLine(nums[i]);
            // i will be == nums.Length
            // fill any remainder with zeros
            for (; i < SMART_ARRAY_STARTING_SIZE; i++)
                sw.WriteLine( 0 );

            string sCorrect = sw.ToString();

            Console.WriteLine("Expected the output\n" + sCorrect + "\nActually got:\n" +
                sResult + "END OF YOUR OUTPUT\n(END OF YOUR OUTPUT was added so it's clear what your output ends");

            bool theSame = TestHelpers.EqualsFuzzyString(sCorrect, sResult);
            Assert.That(theSame == true, "Expected the output\n" + sCorrect + "\nBut actually got:\n" +
                sResult + "END OF YOUR OUTPUT\n(END OF YOUR OUTPUT was added so it's clear what your output ends");
        }

        [Test]
        [Category("SmartArrayAOD")]
        [ExpectedException("PCE_StarterProject.OverflowException")]
        public void GetAtIndex([Values(0, 1, 2, 3, 4)]int index)
        {
            int valueGotten;
            valueGotten = sa.GetAtIndex(index); // should get OverflowException because array isn't allocated yet

            Console.WriteLine("Test Passed: Able to get expected, UNINITIALIZED value from slot {0}!", index);
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void GetAtIndex_After_Resize([Values(5, 6, 10, 20)]int index)
        {
            int valueToAssign = 150; // something kinda distinctive
            int valueGotten;

            try
            {
                valueGotten = sa.GetAtIndex(index);
                throw new Exception("TEST FAILED: Able to get even though SmartArrayAOD hasn't allocated anything yet");
            }catch (OverflowException)
            {
                Console.WriteLine("Correctly caught the expected OverflowException (this is good :)  )");
            }

            sa.SetAtIndex(index, valueToAssign);

            valueGotten = sa.GetAtIndex(index);
            Assert.That(valueGotten == valueToAssign,
                "TEST FAILED: UNEXPECTED VALUE FROM SLOT {0}: (EXPECTED {1}, GOT {2})", index, valueToAssign, valueGotten);
        }

        [Test]
        [Category("SmartArrayAOD")]
        [ExpectedException("PCE_StarterProject.OverflowException")]
        public void GetAtIndex_OutOfBounds_Hi([Values(5, 6, 10)]int index)
        {
            // Note that since "get" does NOT force a resize, anything out-of-bounds should fail
            sa.SetAtIndex(0, 0); // create an array
            int valueGotten;

            valueGotten = sa.GetAtIndex(index); // this should throw an OverflowException
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void GetAtIndex_OutOfBounds_Hi_After_Resize([Values(5, 6, 10)]int index)
        {
            int valueToAssign = 150; // something kinda distinctive
            int stillTooHigh = index + 20;
            int valueGotten;

            try
            {
                valueGotten = sa.GetAtIndex(index);
                throw new Exception("TEST FAILED: Expected OverflowException but didn't get it");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Correctly caught the expected OverflowException (this is good :)  )");
            }

            sa.SetAtIndex(index, valueToAssign);

            try
            {
                sa.GetAtIndex(stillTooHigh);
                throw new Exception("TEST FAILED: Expected OverflowException but didn't get it (second try)");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Correctly caught the expected OverflowException (this is good :)  )");
            }
        }

        [Test]
        [Category("SmartArrayAOD")]
        [ExpectedException("PCE_StarterProject.UnderflowException")]
        public void GetAtIndex_OutOfBounds_Lo([Values(-1, -2, -10)]int index)
        {
            int valueGotten;
            sa.SetAtIndex(0, 0); // create an array

            valueGotten = sa.GetAtIndex(index); // should throw UnderflowException
            throw new Exception("TEST FAILED: Expected UnderflowException but didn't get it (second try)");
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void Set_Then_Get([Values(0, 1, 2, 3, 4, 5, 10, 20)]int index)
        {
            int valueGotten;
            int valueToAssign = (index + 1) * 10;
            
            sa.SetAtIndex(index, valueToAssign);
            valueGotten = sa.GetAtIndex(index);

            Assert.That(valueGotten == valueToAssign,
                "TEST FAILED: UNEXPECTED VALUE FROM SLOT {0}: (EXPECTED 0, GOT {0})", index, valueGotten);

            Console.WriteLine("Test Passed: Able to get expected, initialized value from slot {0}!", index);
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void Set_All_Then_Get_All()
        {
            int valueGotten;
            int valueToAssign;

            for (int i = 0; i < sa.getSize(); i++)
            {
                valueToAssign = (i + 1) * 10;
                sa.SetAtIndex(i, valueToAssign);
            }

            for (int i = 0; i < sa.getSize(); i++)
            {
                valueToAssign = (i + 1) * 10;
                valueGotten = sa.GetAtIndex(i);
                Assert.That(valueGotten == valueToAssign,
                    "TEST FAILED: UNEXPECTED VALUE FROM SLOT {0}: (EXPECTED 0, GOT {0})", i, valueGotten);
            }
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void Test_Get_Size()
        {
            Assert.That(sa.getSize() == 0,
                "Expected to find the SmartArray was size {0}; getSize actually returned {1}, instead",
                0, sa.getSize());
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void Test_Get_Size_Min_Alloc()
        {
            int valueToAssign = 150;

            // this should force an allocation of an array that's 5 elements in size:
            sa.SetAtIndex(0, valueToAssign);

            Assert.That(sa.getSize() == SMART_ARRAY_STARTING_SIZE,
                "Expected to find the SmartArray was size {0}; getSize actually returned {1}, instead",
                SMART_ARRAY_STARTING_SIZE, sa.getSize());
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void Test_Get_Size_After_Resize([Values(5, 10, 20)]int index)
        {
            int valueToAssign = 150;
            int expectedNewSize = index + 1;

            // this should force an allocation of an array that's 5 elements in size:
            sa.SetAtIndex(0, valueToAssign);

            Assert.That(sa.getSize() == SMART_ARRAY_STARTING_SIZE,
                "Expected to find the SmartArray was size {0}; getSize actually returned {1}, instead",
                SMART_ARRAY_STARTING_SIZE, sa.getSize());

            sa.SetAtIndex(index, valueToAssign);

            Assert.That(sa.getSize() == expectedNewSize,
                "Expected to find the SmartArray was size {0}; getSize actually returned {1}, instead",
                expectedNewSize, sa.getSize());
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void Find_In_Unallocated_Array()
        {
            bool found = sa.Find(0);
            Assert.That(found == false,
                "TEST FAILED: EXPECTED TO GET FALSE, WHEN SEARCHING AN UN-ALLOCATED ARRAY!");
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void Find_Value_Present([Values(0, 1, 2, 3, 4, 5, 6, 10, 40)]int index, [Values(-10, 10, 200, 20)]int val)
        {
            bool found;
            sa.SetAtIndex(index, val);

            found = sa.Find(val);
            Assert.That(found == true, "TEST FAILED: UNABLE TO find value {0} in the array, despite having just placed it at index {1}",
                val, index);
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void Find_Value_Present_After_Resize([Values(0, 1, 2, 3, 4, 5, 6, 10, 40)]int firstIndex, 
            [Values(0, 1, 2, 3, 4, 5, 6, 10, 40)]int secondIndex,
            [Values(17, 22)]int firstVal,
            [Values(-10, 10)]int secondVal)
        {
            // This test will do all possible combinations of firstIndex & secondIndex
            // In some (but not all) cases, this will force a resize of the array
            //      we want to check that auto-resizing the array doesn't lose the first value

            bool found;

            sa.SetAtIndex(firstIndex, firstVal);
            sa.SetAtIndex(secondIndex, secondVal);

            // if indeces are the same, the first one will be overwritten
            // in that case, do NOT look for the first value
            if (firstIndex != secondIndex)
            {
                found = sa.Find(firstVal);
                Assert.That(found == true, "TEST FAILED: UNABLE TO find value {0} in the array, despite having just placed it at index {1}",
                    firstVal, firstIndex);
            }

            // regardless, the second one should be present
            found = sa.Find(secondVal);
            Assert.That(found == true, "TEST FAILED: UNABLE TO find value {0} in the array, despite having just placed it at index {1}",
                secondVal, secondIndex);
        }


        [Test]
        [Category("SmartArrayAOD")]
        public void Set_All_Then_Find( [Values(1, 2, 3, 5, 10, 20, 50)]int sizeToForce)
        {
            int valueToAssign;
            for (int i = 0; i < sizeToForce; i++)
            {
                valueToAssign = (i + 1) * 10;
                Console.WriteLine("val to assign: {0}", valueToAssign);
                sa.SetAtIndex(i, valueToAssign);
            }

            sa.PrintAllElements();

            // for numbers smaller than 5, sizeToForce should be < sa.getSize()
            for (int i = 0; i < sizeToForce; i++)
            {
                valueToAssign = (i + 1) * 10;
                Console.WriteLine("val to assign: {0}", valueToAssign);

                bool found = sa.Find(valueToAssign);
                Assert.That(found == true,
                    "TEST FAILED: UNABLE TO find value {0} in the array, despite having just placed it at index {1}",
                    valueToAssign, i);
            }
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void Find_Value_Absent([Values(-10, 10, 200, 20)]int val)
        {
            bool found;

            found = sa.Find(val);
            Assert.That(found == false, "TEST FAILED: Found non-existent value {0} in the array",
                val);
        }

        [Test]
        [Category("SmartArrayAOD")]
        public void Find_Value_Absent_After_Resize([Values(-10, 10, 200, 20)]int val)
        {
            bool found;
            int index = 10;
            int valueWeWontSearchFor = 700;

            Console.WriteLine("About to set index "+index+" to the value "+val);
            sa.SetAtIndex(index, valueWeWontSearchFor);

            Console.WriteLine("About to find " + val);
            found = sa.Find(val);
            Assert.That(found == false, "TEST FAILED: Found non-existent value {0} in the array",
                val);
        }
    }

    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_DESC)] // tags this as an exercise to be graded...
    public class NUnit_Tests_LL_AddToFront 
    {
        LinkedList_Verifier LL;

        [SetUp]
        protected void SetUp()
        {
            LL = new LinkedList_Verifier();
        }

        [Test]
        [Category("LL Add To Front")]
        public void Add_To_Empty()
        {
            LL.AddToFront( 10 );

            bool result = LL.ValidateLinkedList( new int [] { 10 } );
            
            Assert.That( result == true, "Expected to find just 10 in the list, but didn't!");
        }

        [Test]
        [Category("LL Add To Front")]
        public void Add_To_Single_Item_Asc()
        {
            int[] nums = new int[] { 5, 10 };
            for (int i = nums.Length - 1; i >= 0; i--)
                LL.AddToFront(nums[i]);

            bool result = LL.ValidateLinkedList(nums);

            Assert.That(result == true, "Expected to find "+TestHelpers.Array_ToString(nums)
                +" in the list, but didn't!");
        }

        [Test]
        [Category("LL Add To Front")]
        public void Add_To_Single_Item_Desc()
        {
            int[] nums = new int[] { 10, 5 };
            for (int i = nums.Length - 1; i >= 0; i--)
                LL.AddToFront(nums[i]);

            bool result = LL.ValidateLinkedList(nums);

            Assert.That(result == true, "Expected to find " + TestHelpers.Array_ToString(nums)
                + " in the list, but didn't!");
        }

        [Test]
        [Category("LL Add To Front")]
        public void Add_To_Front_Several_Items()
        {
            int[] nums = new int[] { 5, 10, 15, 20 };
            for (int i = nums.Length -1; i >= 0; i--)
                LL.AddToFront(nums[i]);

            bool result = LL.ValidateLinkedList(nums);

            Assert.That(result == true, "Expected to find " + TestHelpers.Array_ToString(nums)
                + " in the list, but didn't!");
        }
    }

    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_DESC)] // tags this as an exercise to be graded...
    public class NUnit_Tests_LL_PrintAll
    {
        MyIntList LL;

        [SetUp]
        protected void SetUp()
        {
            LL = new MyIntList();
        }

        [Test]
        [Category("LL Print All")]
        public void Print_Empty()
        {
            TestHelpers th = new TestHelpers();
            th.StartOutputCapturing();

            LL.PrintAll(); // this should NOT crash

            String sResult = th.StopOutputCapturing();
            String sCorrect = ""; // no actual output, since the list is empty

            Assert.That( TestHelpers.EqualsFuzzyString(sResult, sCorrect),
                "Expected to get back nothing (not even a new/blank line), but actually got:\n{0}END OF YOUR OUTPUT\n(The above 'END OF YOUR OUTPUT' message was added by the test, so that it's clear if you've got an extra line in your output)", sResult);
        }

        [Test]
        [Category("LL Print All")]
        public void Print_Single()
        {
            LL.AddToFront(10);

            TestHelpers th = new TestHelpers();
            Console.WriteLine("Printing the list, using student implementation");

            th.StartOutputCapturing();

            LL.PrintAll(); // this should NOT crash

            String sResult = th.StopOutputCapturing();
            String sCorrect = "10";
            Console.WriteLine("Expected, correct output:\n" + sCorrect);

            Assert.That(TestHelpers.EqualsFuzzyString(sResult, sCorrect),
                "Expected to get back\n{0}\nActually got:\n{1}END OF YOUR OUTPUT\n(The above 'END OF YOUR OUTPUT' message was added by the test, so that it's clear if you've got an extra line in your output)", sCorrect, sResult);
        }


        [Test]
        [Category("LL Print All")]
        public void PrintAll_Several_Items([Values(new int[] { 1, 2, 3 }, new int[] { -10, 20, 200 })] int[] nums)
        {
            for (int i = nums.Length - 1; i >= 0; i--)
                LL.AddToFront(nums[i]);

            TestHelpers th = new TestHelpers();
            Console.WriteLine("Printing the list, using student implementation");
            th.StartOutputCapturing();

            LL.PrintAll();

            String sResult = th.StopOutputCapturing();
            String sCorrect = TestHelpers.PrintArrayToString(nums);
            Console.WriteLine("Expected, correct output:\n" + sCorrect);

            Assert.That(TestHelpers.EqualsFuzzyString(sResult, sCorrect),
                "Expected to get back\n{0}\nActually got:\n{1}END OF YOUR OUTPUT\n(The above 'END OF YOUR OUTPUT' message was added by the test, so that it's clear if you've got an extra line in your output)", sCorrect, sResult);
        }
    }
    
    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_DESC)] // tags this as an exercise to be graded...
    public class NUnit_Tests_LL_RemoveFromFront 
    {
        LinkedList_Verifier LL;

        [SetUp]
        protected void SetUp()
        {
            LL = new LinkedList_Verifier();
        }

        [Test]
        [Category("LL Remove From Front")]
        public void Remove_From_Empty()
        {
            try
            {
                LL.RemoveFromFront(); // should not crash
            }
            catch (NullReferenceException nre)
            {
                Console.WriteLine("Got a NullReferenceException - are you accessing the first/front reference in an empty list?");
                throw nre;
            }

            bool result = LL.ValidateLinkedList(new int[0] );

            Assert.That(result == true, "Expected to find nothing in the list, but didn't!");
        }

        [Test]
        [Category("LL Remove From Front")]
        public void Remove_Single_Item()
        {
            int[] nums = new int[] { 5 };
            int[] correctNums = new int[0];
            for (int i = nums.Length - 1; i >= 0; i--)
                LL.AddToFront(nums[i]);

            LL.RemoveFromFront();

            bool result = LL.ValidateLinkedList(correctNums);

            Assert.That(result == true, "Expected to find nothing, but actually found one or more items!");
        }

        [Test]
        [Category("LL Remove From Front")]
        public void Add_To_Front_Several_Items()
        {
            int[] nums = new int[] { 5, 10, 15, 20 };
            int[] correctNums = new int[] { 10, 15, 20 };
            for (int i = nums.Length - 1; i >= 0; i--)
                LL.AddToFront(nums[i]);

            LL.RemoveFromFront();

            bool result = LL.ValidateLinkedList(correctNums);

            Assert.That(result == true, "Expected to find " + TestHelpers.Array_ToString(correctNums)
                + " in the list, but actually foudn something different!");
        }
    }


    [TestFixture]
    [Timeout(2000)] // 2 seconds default timeout
    [Description(TestHelpers.TEST_SUITE_IGNORE_DESC)] // tags this as an exercise to be graded...
    public class NUnit_Tests_LL_PrintNode
    {
        MyIntList LL;

        [SetUp]
        protected void SetUp()
        {
            LL = new MyIntList();
        }

        [Test]
        [Category("LL Print Node")]
        public void Print_Empty_Zero()
        {
            TestHelpers th = new TestHelpers();
            th.StartOutputCapturing();

            LL.PrintNode(0); // this should NOT crash

            String sResult = th.StopOutputCapturing();
            String sCorrect = ""; // no actual output, since the list is empty

            Assert.That(TestHelpers.EqualsFuzzyString(sResult, sCorrect),
                "Expected to get back nothing (not even a new/blank line), but actually got:\n{0}END OF YOUR OUTPUT\n(The above 'END OF YOUR OUTPUT' message was added by the test, so that it's clear if you've got an extra line in your output)", sResult);
        }
        [Test]
        [Category("LL Print Node")]
        public void Print_Empty_NonZero()
        {
            TestHelpers th = new TestHelpers();
            th.StartOutputCapturing();

            LL.PrintNode(77); // this should (still) NOT crash

            String sResult = th.StopOutputCapturing();
            String sCorrect = ""; // no actual output, since the list is empty

            Assert.That(TestHelpers.EqualsFuzzyString(sResult, sCorrect),
                "Expected to get back nothing (not even a new/blank line), but actually got:\n{0}END OF YOUR OUTPUT\n(The above 'END OF YOUR OUTPUT' message was added by the test, so that it's clear if you've got an extra line in your output)", sResult);
        }

        [Test]
        [Category("LL Print Node")]
        public void Print_Single_Ok()
        {
            LL.AddToFront(10);

            TestHelpers th = new TestHelpers();
            Console.WriteLine("Printing the node, using student implementation");

            th.StartOutputCapturing();

            LL.PrintNode(0); 

            String sResult = th.StopOutputCapturing();
            String sCorrect = "10";
            Console.WriteLine("Expected, correct output:\n" + sCorrect);

            Assert.That(TestHelpers.EqualsFuzzyString(sResult, sCorrect),
                "Expected to get back\n{0}\nActually got:\n{1}END OF YOUR OUTPUT\n(The above 'END OF YOUR OUTPUT' message was added by the test, so that it's clear if you've got an extra line in your output)", sCorrect, sResult);
        }

        [Test]
        [Category("LL Print Node")]
        public void Print_Single_OutOfBounds()
        {
            LL.AddToFront(10);

            TestHelpers th = new TestHelpers();
            Console.WriteLine("Printing the node, using student implementation");

            th.StartOutputCapturing();

            LL.PrintNode(99);

            String sResult = th.StopOutputCapturing();
            String sCorrect = "";
            Console.WriteLine("Expected, correct output:\n" + sCorrect);

            Assert.That(TestHelpers.EqualsFuzzyString(sResult, sCorrect),
                "Expected to get back\n{0}\nActually got:\n{1}END OF YOUR OUTPUT\n(The above 'END OF YOUR OUTPUT' message was added by the test, so that it's clear if you've got an extra line in your output)", sCorrect, sResult);
        }


        [Test]
        [Category("LL Print Node")]
        // 1u is an UNSIGNED INT (uint) with the value 1
        public void PrintNode_Several_Items_Ok([Values(0u, 1u, 2u)]uint target)
        {
            int[] nums = new int[] { -10, 20, 200 };
            for (int i = nums.Length - 1; i >= 0; i--)
                LL.AddToFront(nums[i]);

            TestHelpers th = new TestHelpers();
            Console.WriteLine("Printing the node, using student implementation");
            th.StartOutputCapturing();

            LL.PrintNode(target);

            String sResult = th.StopOutputCapturing();
            String sCorrect = nums[target].ToString();
            Console.WriteLine("Expected, correct output:\n" + sCorrect);

            Assert.That(TestHelpers.EqualsFuzzyString(sResult, sCorrect),
                "Expected to get back\n{0}\nActually got:\n{1}END OF YOUR OUTPUT\n(The above 'END OF YOUR OUTPUT' message was added by the test, so that it's clear if you've got an extra line in your output)", sCorrect, sResult);
        }

        [Test]
        [Category("LL Print Node")]
        // 3u is an UNSIGNED INT (uint) with the value 3
        public void PrintNode_Several_Items_OutOfBounds([Values(3u, 10u, 77u)]uint target)
        {
            int[] nums = new int[] { -10, 20, 200 };
            for (int i = nums.Length - 1; i >= 0; i--)
                LL.AddToFront(nums[i]);

            TestHelpers th = new TestHelpers();
            Console.WriteLine("Printing the node, using student implementation");
            th.StartOutputCapturing();

            LL.PrintNode(target);

            String sResult = th.StopOutputCapturing();
            String sCorrect = "";
            Console.WriteLine("Expected, correct behavior is to produce no output");

            Assert.That(TestHelpers.EqualsFuzzyString(sResult, sCorrect),
                "Expected to get back\n{0}\nActually got:\n{1}END OF YOUR OUTPUT\n(The above 'END OF YOUR OUTPUT' message was added by the test, so that it's clear if you've got an extra line in your output)", sCorrect, sResult);
        }
    }

}