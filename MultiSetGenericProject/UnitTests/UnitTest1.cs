using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using km.Collections.MultiZbior;
using MultiSetGeneric.Structures;
using Microsoft.VisualBasic;

namespace UnitTests
{
    [TestClass]
    public class MultiSetTests
    {
        private void Assert_MultiSet(MultiSet<char> ms1, MultiSet<char> ms2)
        {
            CollectionAssert.AreEqual(ms1.Dictionary, ms2.Dictionary);
        }

        private void Assert_MultiSet(MultiSet<string> ms1, MultiSet<string> ms2)
        {
            CollectionAssert.AreEqual(ms1.Dictionary, ms2.Dictionary);
        }

        #region Constructors ===============================================================

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default_Char()
        {
            MultiSet<char> ms = new MultiSet<char>();
            CollectionAssert.AreEqual(ms.Dictionary, new Dictionary<char, int>());
        }

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default_String()
        {
            MultiSet<string> ms = new MultiSet<string>();
            CollectionAssert.AreEqual(ms.Dictionary, new Dictionary<string, int>());
        }

        [TestMethod, TestCategory("Constructors")]
        public void Static_Property_Empty()
        {
            CollectionAssert.AreEqual(MultiSet<string>.Empty.Dictionary, new Dictionary<string, int>());
            CollectionAssert.AreEqual(MultiSet<char>.Empty.Dictionary, new Dictionary<char, int>());
        }

        
        [DataTestMethod, TestCategory("Constructors")]
        [DataRow( new char[] {'c'}, 1,1 )]
        [DataRow( new char[] {'c','c'}, 2,1 )]
        [DataRow( new char[] {}, 0,0 )]
        [DataRow( new char[] {'c', 'c', 'a'}, 3,2 )]
        public void Constructor_IEnumerable_Sequence_Char(char[] a, int count, int length)
        {
            MultiSet<char> ms = new MultiSet<char>(a);
            Assert.AreEqual(ms.Count, count);
            Assert.AreEqual(ms.Dictionary.Count, length);
            int counter = 0;
            foreach(var item in ms)
            {
                Assert.AreEqual(item, a[counter]);
                counter++;
            }
        }
        

        #endregion


        #region ICollection ===============================================================

        [DataTestMethod, TestCategory("ICollection")]
        [DataRow( new char[] {}, 0 )]
        [DataRow( new char[] {'c'}, 1 )]
        [DataRow( new char[] {'c','a'}, 2 )]
        [DataRow( new char[] {'c','c'}, 2 )]
        [DataRow( new char[] {'c','c','a'}, 3 )]
        public void ICollection_Count_Char(char[] MultiSetArray, int expectedCount) 
        {
            MultiSet<char> ms = new MultiSet<char>(MultiSetArray);
            Assert.AreEqual(ms.Count, expectedCount);
        }

        [DataTestMethod, TestCategory("ICollection")]
        [DataRow( new string[] {}, 0 )]
        [DataRow( new string[] {"cake"}, 1 )]
        [DataRow( new string[] {"cake","apple"}, 2 )]
        [DataRow( new string[] {"cake","cake"}, 2 )]
        [DataRow( new string[] {"cake","cake","apple"}, 3 )]
        public void ICollection_Count_String(string[] MultiSetArray, int expectedCount) 
        {
            MultiSet<string> ms = new MultiSet<string>(MultiSetArray);
            Assert.AreEqual(ms.Count, expectedCount);
        }




        [DataTestMethod, TestCategory("ICollection")]
        [DataRow( new char[] {}, 0,0,     new char[] {}, 0,0 )]
        [DataRow( new char[] {'c'}, 1,1,      new char[] {}, 1,1 )]
        [DataRow( new char[] {'c'}, 1,1,      new char[] {'c'}, 2,1 )]
        [DataRow( new char[] {'c','a'}, 2,2,      new char[] {'c'}, 3,2 )]
        [DataRow( new char[] {'c','c'}, 2,1,      new char[] {'a','c'}, 4,2 )]
        [DataRow( new char[] {'c','c','a'}, 3,2,      new char[] {'a','a','c'}, 6,2 )]
        public void ICollection_Add_Char(char[] MultiSetArray1, int expectedCount1, int lenght1, char[] MultiSetArray2, int expectedCount2, int lenght2) 
        {
            MultiSet<char> ms = new MultiSet<char>(MultiSetArray1);
            Assert.AreEqual(ms.Count, expectedCount1);
            Assert.AreEqual(ms.Dictionary.Count, lenght1);
            foreach(var item in MultiSetArray2)
            {
                ms.Add(item);
            }
            Assert.AreEqual(ms.Count, expectedCount2);
            Assert.AreEqual(ms.Dictionary.Count, lenght2);
        }

        [DataTestMethod, TestCategory("ICollection")]
        [DataRow( new string[] {}, 0,0,     new string[] {}, 0,0 )]
        [DataRow( new string[] {"cake"}, 1,1,      new string[] {}, 1,1 )]
        [DataRow( new string[] {"cake"}, 1,1,      new string[] {"cake"}, 2,1 )]
        [DataRow( new string[] {"cake","apple"}, 2,2,      new string[] {"cake"}, 3,2 )]
        [DataRow( new string[] {"cake","cake"}, 2,1,      new string[] {"apple","cake"}, 4,2 )]
        [DataRow( new string[] {"cake","cake","apple"}, 3,2,      new string[] {"apple","apple","cake"}, 6,2 )]
        public void ICollection_Add_String(string[] MultiSetArray1, int expectedCount1, int lenght1, string[] MultiSetArray2, int expectedCount2, int lenght2) 
        {
            MultiSet<string> ms = new MultiSet<string>(MultiSetArray1);
            Assert.AreEqual(ms.Count, expectedCount1);
            Assert.AreEqual(ms.Dictionary.Count, lenght1);
            foreach(var item in MultiSetArray2)
            {
                ms.Add(item);
            }
            Assert.AreEqual(ms.Count, expectedCount2);
            Assert.AreEqual(ms.Dictionary.Count, lenght2);
        }




        [DataTestMethod, TestCategory("ICollection")]
        [DataRow( new char[] {}, 0,0,     new char[] {}, 0,0 )]
        [DataRow( new char[] {'c'}, 1,1,      new char[] {}, 1,1 )]
        [DataRow( new char[] {'c','a'}, 2,2,      new char[] {'c'}, 1,1 )]
        [DataRow( new char[] {'c','c'}, 2,1,      new char[] {'a','c'}, 0,0 )]
        [DataRow( new char[] {'c','c','a'}, 3,2,      new char[] {'c'}, 1,1 )]
        [DataRow( new char[] {'c','c','a','a'}, 4,2,      new char[] {'c'}, 2,1 )]
        public void ICollection_Remove_Char(char[] MultiSetArray1, int expectedCount1, int lenght1, char[] MultiSetArray2, int expectedCount2, int lenght2) 
        {
            MultiSet<char> ms = new MultiSet<char>(MultiSetArray1);
            Assert.AreEqual(ms.Count, expectedCount1);
            Assert.AreEqual(ms.Dictionary.Count, lenght1);
            foreach(var item in MultiSetArray2)
            {
                ms.Remove(item);
            }
            Assert.AreEqual(ms.Count, expectedCount2);
            Assert.AreEqual(ms.Dictionary.Count, lenght2);
        }

        [DataTestMethod, TestCategory("ICollection")]
        [DataRow( new string[] {}, 0,0,     new string[] {}, 0,0 )]
        [DataRow( new string[] {"cake"}, 1,1,      new string[] {}, 1,1 )]
        [DataRow( new string[] {"cake","apple"}, 2,2,      new string[] {"cake"}, 1,1 )]
        [DataRow( new string[] {"cake","cake"}, 2,1,      new string[] {"apple","cake"}, 0,0 )]
        [DataRow( new string[] {"cake","cake","apple"}, 3,2,      new string[] {"cake"}, 1,1 )]
        [DataRow( new string[] {"cake","cake","apple","apple"}, 4,2,      new string[] {"cake"}, 2,1 )]
        public void ICollection_Remove_String(string[] MultiSetArray1, int expectedCount1, int lenght1, string[] MultiSetArray2, int expectedCount2, int lenght2) 
        {
            MultiSet<string> ms = new MultiSet<string>(MultiSetArray1);
            Assert.AreEqual(ms.Count, expectedCount1);
            Assert.AreEqual(ms.Dictionary.Count, lenght1);
            foreach(var item in MultiSetArray2)
            {
                ms.Remove(item);
            }
            Assert.AreEqual(ms.Count, expectedCount2);
            Assert.AreEqual(ms.Dictionary.Count, lenght2);
        }




        [DataTestMethod, TestCategory("ICollection")]
        [DataRow('a',true)]
        [DataRow('b',true)]
        [DataRow('c',true)]
        [DataRow('d',false)]
        [DataRow('e',true)]
        [DataRow('f',false)]
        [DataRow('g',true)]
        public void ICollection_Contains_Char(char value, bool expectedValue)
        {
            MultiSet<char> ms = new MultiSet<char>(new char[] {'a','a','b','c','c','e','g'} );
            Assert.AreEqual(ms.Contains(value), expectedValue);
        }

        [DataTestMethod, TestCategory("ICollection")]
        [DataRow("Apple",true)]
        [DataRow("Banana",true)]
        [DataRow("Cake",true)]
        [DataRow("Dofus",false)]
        [DataRow("Eleven",true)]
        [DataRow("False",false)]
        [DataRow("Ground",true)]
        public void ICollection_Contains_String(string value, bool expectedValue)
        {
            MultiSet<string> ms = new MultiSet<string>(new string[] {"Apple","Apple","Banana","Cake","Cake","Eleven","Ground"} );
            Assert.AreEqual(ms.Contains(value), expectedValue);
        }



        [TestMethod, TestCategory("ICollection")]
        public void ICollection_Clear()
        {
            MultiSet<char> ms1 = new MultiSet<char>(new char[] {'a','a','b','c','c','e','g'} );
            MultiSet<string> ms2 = new MultiSet<string>(new string[] {"Apple","Apple","Banana","Cake","Cake","Eleven","Ground"} );

            ms1.Clear();
            ms2.Clear();

            Assert.AreEqual(ms1.Count, 0);
            Assert.AreEqual(ms2.Count, 0);
            Assert.AreEqual(ms1.Dictionary.Count, 0);
            Assert.AreEqual(ms2.Dictionary.Count, 0);
        }




        [TestMethod, TestCategory("ICollection")]
        public void ICollection_CopyTo_Char()
        {
            char[] a1 = new char[] { 'a', 'a', 'b', 'c', 'c', 'e', 'g' };
            MultiSet<char> ms = new MultiSet<char>(a1);
            char[] a2 = new char[ms.Count+1];
            a2[0] = 'z';

            ms.CopyTo(a2, 1);
            Assert.AreEqual(a2[0], 'z');

            for(int i = 1; i < ms.Count+1; i++)
            {
                Assert.AreEqual(a2[i], a1[i-1]);
            }
        }

        [TestMethod, TestCategory("ICollection")]
        public void ICollection_CopyTo_String()
        {
            string[] a1 = new string[] {"Apple","Apple","Banana","Cake","Cake","Eleven","Ground"};
            MultiSet<string> ms = new MultiSet<string>(a1);
            string[] a2 = new string[ms.Count+1];
            a2[0] = "Ztype";

            ms.CopyTo(a2, 1);
            Assert.AreEqual(a2[0], "Ztype");

            for(int i = 1; i < ms.Count+1; i++)
            {
                Assert.AreEqual(a2[i], a1[i-1]);
            }
        }

        [TestMethod, TestCategory("ICollection")]
        [ExpectedException(typeof(ArgumentException))]
        public void ICollection_CopyTo_ArgumentException()
        {
            MultiSet<char> ms1 = new MultiSet<char>(new char[] {'a','a','b','c','c','e','g'} );
            MultiSet<string> ms2 = new MultiSet<string>(new string[] {"Apple","Apple","Banana","Cake","Cake","Eleven","Ground"} );

            char[] a1 = new char[2];
            string[] a2 = new string[2];

            ms1.CopyTo( a1, 0 );
            ms2.CopyTo( a2, 0 );
        }

        #endregion


        #region IEnumerable ===============================================================

        [TestMethod, TestCategory("IEnumerable")]
        public void IEnumerable_Iterator_Char()
        {
            char[] a = new char[] {'a','a','b','c','c','e','g'};
            MultiSet<char> ms = new MultiSet<char>(a);
            int counter = 0;
            foreach(var item in ms)
            {
                Assert.AreEqual(item, a[counter]);
                counter++;
            }
        }

        #endregion


        #region IMultiSet ===============================================================

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','a','b','c'}, 'c',2,  6,3,3 )]
        [DataRow( new char[] {'a','a','b','c'}, 'd',2,  6,4,2 )]
        [DataRow( new char[] {},                'd',2,  2,1,2 )]
        [DataRow( new char[] {'a','a'},         'd',3,  5,2,3 )]
        public void IMultiSet_Add_Char(char[] MultiSetArray, char addedValue, int quantity, int expectedCount, int expectedLength, int fullAddedCount) 
        {
            MultiSet<char> ms = new MultiSet<char>(MultiSetArray);

            ms.Add(addedValue, quantity);

            Assert.AreEqual(ms.Count, expectedCount);
            Assert.AreEqual(ms.Dictionary.Count, expectedLength);
            Assert.AreEqual(ms[addedValue], fullAddedCount);
        }

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new string[] {"aa","aa","bb","cc"}, "cc",2,  6,3,3 )]
        [DataRow( new string[] {"aa","aa","bb","cc"}, "dd",2,  6,4,2 )]
        [DataRow( new string[] {},                    "dd",2,  2,1,2 )]
        [DataRow( new string[] {"aa","aa"},           "dd",3,  5,2,3 )]
        public void IMultiSet_Add_String(string[] MultiSetArray, string addedValue, int quantity, int expectedCount, int expectedLength, int fullAddedCount) 
        {
            MultiSet<string> ms = new MultiSet<string>(MultiSetArray);

            ms.Add(addedValue, quantity);

            Assert.AreEqual(ms.Count, expectedCount);
            Assert.AreEqual(ms.Dictionary.Count, expectedLength);
            Assert.AreEqual(ms[addedValue], fullAddedCount);
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','a','b','b','b','c','c'}, 'c',-1,  7,3,2 )]
        [DataRow( new char[] {'a','a','b','b','b','c','c'}, 'c',0,   7,3,2 )]
        [DataRow( new char[] {'a','a','b','b','b','c','c'}, 'c',1,   6,3,1 )]
        [DataRow( new char[] {'a','a','b','b','b','c','c'}, 'c',2,   5,2,0 )]
        [DataRow( new char[] {'a','a','b','b','b','c','c'}, 'c',3,   5,2,0 )]
        public void IMultiSet_Remove_Char(char[] MultiSetArray, char removedValue, int quantity, int expectedCount, int expectedLength, int fullRemovedCount) 
        {
            MultiSet<char> ms = new MultiSet<char>(MultiSetArray);

            ms.Remove(removedValue, quantity);

            Assert.AreEqual(ms.Count, expectedCount);
            Assert.AreEqual(ms.Dictionary.Count, expectedLength);
            if(fullRemovedCount > 0)
                Assert.AreEqual(ms[removedValue], fullRemovedCount);
        }

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new string[] {"aa","aa","bb","bb","bb","aa","bb"}, "cc",1,   7,2,0 )]
        [DataRow( new string[] {"aa","aa","bb","bb","bb","cc","cc"}, "cc",-1,  7,3,2 )]
        [DataRow( new string[] {"aa","aa","bb","bb","bb","cc","cc"}, "cc",0,   7,3,2 )]
        [DataRow( new string[] {"aa","aa","bb","bb","bb","cc","cc"}, "cc",1,   6,3,1 )]
        [DataRow( new string[] {"aa","aa","bb","bb","bb","cc","cc"}, "cc",2,   5,2,0 )]
        [DataRow( new string[] {"aa","aa","bb","bb","bb","cc","cc"}, "cc",3,   5,2,0 )]
        public void IMultiSet_Remove_String(string[] MultiSetArray, string removedValue, int quantity, int expectedCount, int expectedLength, int fullRemovedCount) 
        {
            MultiSet<string> ms = new MultiSet<string>(MultiSetArray);

            ms.Remove(removedValue, quantity);

            Assert.AreEqual(ms.Count, expectedCount);
            Assert.AreEqual(ms.Dictionary.Count, expectedLength);
            if(fullRemovedCount > 0)
                Assert.AreEqual(ms[removedValue], fullRemovedCount);
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','b','b','b',},                 'c',   4,2 )]
        [DataRow( new char[] {'a','a','b','b','b','c'},          'c',   5,2 )]
        [DataRow( new char[] {'a','a','a','b','b','b','c','c'},  'c',   6,2 )]
        [DataRow( new char[] {'a','a','b','b','d','c','c','c'},  'c',   5,3 )]
        [DataRow( new char[] {'a','a','b','d','e','c','c','c'},  'c',   5,4 )]
        public void IMultiSet_RemoveAll_Char(char[] MultiSetArray, char removedValue, int expectedCount, int expectedLength) 
        {
            MultiSet<char> ms = new MultiSet<char>(MultiSetArray);

            ms.RemoveAll(removedValue);

            Assert.AreEqual(ms.Count, expectedCount);
            Assert.AreEqual(ms.Dictionary.Count, expectedLength);
            Assert.IsFalse(ms.Contains(removedValue));
        }

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new string[] {"aa","bb","bb","bb",},                     "cc",   4,2 )]
        [DataRow( new string[] {"aa","aa","bb","bb","bb","cc"},            "cc",   5,2 )]
        [DataRow( new string[] {"aa","aa","aa","bb","bb","bb","cc","cc"},  "cc",   6,2 )]
        [DataRow( new string[] {"aa","aa","bb","bb","dd","cc","cc","cc"},  "cc",   5,3 )]
        [DataRow( new string[] {"aa","aa","bb","dd","ee","cc","cc","cc"},  "cc",   5,4 )]
        public void IMultiSet_RemoveAll_String(string[] MultiSetArray, string removedValue, int expectedCount, int expectedLength) 
        {
            MultiSet<string> ms = new MultiSet<string>(MultiSetArray);

            ms.RemoveAll(removedValue);

            Assert.AreEqual(ms.Count, expectedCount);
            Assert.AreEqual(ms.Dictionary.Count, expectedLength);
            Assert.IsFalse(ms.Contains(removedValue));
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {},              new char[] {'a','a','a','b','b','c'}, 1 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},   new char[] {'a','a','a','a','b','b','b','c','c'}, 2 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'c','c'},       new char[] {'a','a','a','b','b','c','c','c'}, 3 )]
        public void IMultiSet_UnionWith_Char(char[] MultiSetArray1, char[] MultiSetArray2, char[] expectedResult, int temp) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);
            MultiSet<char> ms3 = new MultiSet<char>(expectedResult);

            ms1.UnionWith(ms2);

            Assert_MultiSet(ms1, ms3);
        }

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {},              new string[] {"a","a","a","b","b","c"}, 1 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","b","c"},   new string[] {"a","a","a","a","b","b","b","c","c"}, 2 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"c","c"},       new string[] {"a","a","a","b","b","c","c","c"}, 3 )]
        public void IMultiSet_UnionWith_String(string[] MultiSetArray1, string[] MultiSetArray2, string[] expectedResult, int temp) 
        {
            MultiSet<string> ms1 = new MultiSet<string>(MultiSetArray1);
            MultiSet<string> ms2 = new MultiSet<string>(MultiSetArray2);
            MultiSet<string> ms3 = new MultiSet<string>(expectedResult);

            ms1.UnionWith(ms2);

            Assert_MultiSet(ms1, ms3);
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {},              new char[] {}, 1 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},   new char[] {'a','b','c'}, 2 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'c','c'},       new char[] {'c'}, 3 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','a','b'},   new char[] {'a','a','b'}, 4 )]
        public void IMultiSet_IntersectWith_Char(char[] MultiSetArray1, char[] MultiSetArray2, char[] expectedResult, int temp) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);
            MultiSet<char> ms3 = new MultiSet<char>(expectedResult);

            ms1.IntersectWith(ms2);

            Assert_MultiSet(ms1, ms3);
        }

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {},              new string[] {}, 1 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","b","c"},   new string[] {"a","b","c"}, 2 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"c","c"},       new string[] {"c"}, 3 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","a","b"},   new string[] {"a","a","b"}, 4 )]
        public void IMultiSet_IntersectWith_String(string[] MultiSetArray1, string[] MultiSetArray2, string[] expectedResult, int temp) 
        {
            MultiSet<string> ms1 = new MultiSet<string>(MultiSetArray1);
            MultiSet<string> ms2 = new MultiSet<string>(MultiSetArray2);
            MultiSet<string> ms3 = new MultiSet<string>(expectedResult);

            ms1.IntersectWith(ms2);

            Assert_MultiSet(ms1, ms3);
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {},              new char[] {'a','a','a','b','b','c'}, 1 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},   new char[] {'a','a','b'}, 2 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'c','c'},       new char[] {'a','a','a','b','b'}, 3 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','a','b'},   new char[] {'a','b','c'}, 4 )]
        public void IMultiSet_ExceptWith_Char(char[] MultiSetArray1, char[] MultiSetArray2, char[] expectedResult, int temp) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);
            MultiSet<char> ms3 = new MultiSet<char>(expectedResult);

            ms1.ExceptWith(ms2);

            Assert_MultiSet(ms1, ms3);
        }

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {},              new string[] {"a","a","a","b","b","c"}, 1 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","b","c"},   new string[] {"a","a","b"}, 2 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"c","c"},       new string[] {"a","a","a","b","b"}, 3 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","a","b"},   new string[] {"a","b","c"}, 4 )]
        public void IMultiSet_ExceptWith_String(string[] MultiSetArray1, string[] MultiSetArray2, string[] expectedResult, int temp) 
        {
            MultiSet<string> ms1 = new MultiSet<string>(MultiSetArray1);
            MultiSet<string> ms2 = new MultiSet<string>(MultiSetArray2);
            MultiSet<string> ms3 = new MultiSet<string>(expectedResult);

            ms1.ExceptWith(ms2);

            Assert_MultiSet(ms1, ms3);
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {},              new char[] {'a','a','a','b','b','c'}, 1 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},   new char[] {'a','a','b'},2 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'c','c'},       new char[] {'a','a','a','b','b','c'},3 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','a','b'},   new char[] {'a','b','c'},4 )]
        [DataRow( new char[] {'a','b','c'},             new char[] {'b','c','d'},   new char[] {'a','d'},5 )]
        public void IMultiSet_SymetricExceptWith_Char(char[] MultiSetArray1, char[] MultiSetArray2, char[] expectedResult, int temp) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);
            MultiSet<char> ms3 = new MultiSet<char>(expectedResult);

            ms1.SymmetricExceptWith(ms2);

            Assert_MultiSet(ms1, ms3);
        }

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {},              new string[] {"a","a","a","b","b","c"}, 1 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","b","c"},   new string[] {"a","a","b"},2 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"c","c"},       new string[] {"a","a","a","b","b","c"},3 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","a","b"},   new string[] {"a","b","c"},4 )]
        [DataRow( new string[] {"a","b","c"},             new string[] {"b","c","d"},   new string[] {"a","d"},5 )]
        public void IMultiSet_SymetricExceptWith_String(string[] MultiSetArray1, string[] MultiSetArray2, string[] expectedResult, int temp) 
        {
            MultiSet<string> ms1 = new MultiSet<string>(MultiSetArray1);
            MultiSet<string> ms2 = new MultiSet<string>(MultiSetArray2);
            MultiSet<string> ms3 = new MultiSet<string>(expectedResult);

            ms1.SymmetricExceptWith(ms2);

            Assert_MultiSet(ms1, ms3);
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {},                  true,false,  1 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},       true,false,  2 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'b','c'},           true,false,  3 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','a','b'},       true,false,  4 )]
        [DataRow( new char[] {'a','b','c'},             new char[] {'a','b','c'},       true,true,   5 )]
        [DataRow( new char[] {'a','b','c'},             new char[] {'b','c','d','a'},   false,true,  6 )]
        [DataRow( new char[] {'a','b','c'},             new char[] {'b','c','d'},       false,false, 7 )]
        public void IMultiSet_IsSubsetOf_IsSupersetOf_Char(char[] MultiSetArray1, char[] MultiSetArray2, bool expectedResult1, bool expectedResult2, int temp) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);

            Assert.AreEqual(ms2.IsSubsetOf(ms1), expectedResult1);
            Assert.AreEqual(ms1.IsSubsetOf(ms2), expectedResult2);

            Assert.AreEqual(ms1.IsSupersetOf(ms2), expectedResult1);
            Assert.AreEqual(ms2.IsSupersetOf(ms1), expectedResult2);
        }

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {},                  true,false,  1 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},       true,false,  2 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'b','c'},           true,false,  3 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','a','b'},       true,false,  4 )]
        [DataRow( new char[] {'a','b','c'},             new char[] {'a','b','c'},       false,false, 5 )]
        [DataRow( new char[] {'a','b','c'},             new char[] {'b','c','d','a'},   false,true,  6 )]
        [DataRow( new char[] {'a','b','c'},             new char[] {'b','c','d'},       false,false, 7 )]
        public void IMultiSet_IsProperSubsetOf_IsProperSupersetOf_Char(char[] MultiSetArray1, char[] MultiSetArray2, bool expectedResult1, bool expectedResult2, int temp) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);

            Assert.AreEqual(ms2.IsProperSubsetOf(ms1), expectedResult1);
            Assert.AreEqual(ms1.IsProperSubsetOf(ms2), expectedResult2);

            Assert.AreEqual(ms1.IsProperSupersetOf(ms2), expectedResult1);
            Assert.AreEqual(ms2.IsProperSupersetOf(ms1), expectedResult2);
        }

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {},                  true,false,  1 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","b","c"},       true,false,  2 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"b","c"},           true,false,  3 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","a","b"},       true,false,  4 )]
        [DataRow( new string[] {"a","b","c"},             new string[] {"a","b","c"},       true,true,   5 )]
        [DataRow( new string[] {"a","b","c"},             new string[] {"b","c","d","a"},   false,true,  6 )]
        [DataRow( new string[] {"a","b","c"},             new string[] {"b","c","d"},       false,false, 7 )]
        public void IMultiSet_IsSubsetOf_IsSupersetOf_String(string[] MultiSetArray1, string[] MultiSetArray2, bool expectedResult1, bool expectedResult2, int temp) 
        {
            MultiSet<string> ms1 = new MultiSet<string>(MultiSetArray1);
            MultiSet<string> ms2 = new MultiSet<string>(MultiSetArray2);

            Assert.AreEqual(ms2.IsSubsetOf(ms1), expectedResult1);
            Assert.AreEqual(ms1.IsSubsetOf(ms2), expectedResult2);

            Assert.AreEqual(ms1.IsSupersetOf(ms2), expectedResult1);
            Assert.AreEqual(ms2.IsSupersetOf(ms1), expectedResult2);
        }

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {},                  true,false,  1 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","b","c"},       true,false,  2 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"b","c"},           true,false,  3 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","a","b"},       true,false,  4 )]
        [DataRow( new string[] {"a","b","c"},             new string[] {"a","b","c"},       false,false, 5 )]
        [DataRow( new string[] {"a","b","c"},             new string[] {"b","c","d","a"},   false,true,  6 )]
        [DataRow( new string[] {"a","b","c"},             new string[] {"b","c","d"},       false,false, 7 )]
        public void IMultiSet_IsProperSubsetOf_IsProperSupersetOf_String(string[] MultiSetArray1, string[] MultiSetArray2, bool expectedResult1, bool expectedResult2, int temp) 
        {
            MultiSet<string> ms1 = new MultiSet<string>(MultiSetArray1);
            MultiSet<string> ms2 = new MultiSet<string>(MultiSetArray2);

            Assert.AreEqual(ms2.IsProperSubsetOf(ms1), expectedResult1);
            Assert.AreEqual(ms1.IsProperSubsetOf(ms2), expectedResult2);

            Assert.AreEqual(ms1.IsProperSupersetOf(ms2), expectedResult1);
            Assert.AreEqual(ms2.IsProperSupersetOf(ms1), expectedResult2);
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {},                        new char[] {},                  false, 1 )]
        [DataRow( new char[] {'a'},                     new char[] {},                  false, 2 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},       true,  3 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'b'},               true,  4 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'z','x','y'},       false, 5 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'c','x','y'},       true,  6 )]
        public void IMultiSet_Overlaps_Char(char[] MultiSetArray1, char[] MultiSetArray2, bool expectedResult, int temp) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);

            Assert.AreEqual(ms1.Overlaps(ms2), expectedResult);
            Assert.AreEqual(ms2.Overlaps(ms1), expectedResult);
        }

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new string[] {},                        new string[] {},                  false, 1 )]
        [DataRow( new string[] {"a"},                     new string[] {},                  false, 2 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","b","c"},       true,  3 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"b"},               true,  4 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"z","x","y"},       false, 5 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"c","x","y"},       true,  6 )]
        public void IMultiSet_Overlaps_String(string[] MultiSetArray1, string[] MultiSetArray2, bool expectedResult, int temp) 
        {
            MultiSet<string> ms1 = new MultiSet<string>(MultiSetArray1);
            MultiSet<string> ms2 = new MultiSet<string>(MultiSetArray2);

            Assert.AreEqual(ms1.Overlaps(ms2), expectedResult);
            Assert.AreEqual(ms2.Overlaps(ms1), expectedResult);
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {},                        new char[] {},                  true,  1 )]
        [DataRow( new char[] {'a'},                     new char[] {},                  false, 2 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},       false, 3 )]
        [DataRow( new char[] {'a','b','c'},             new char[] {'a','b','c'},       true,  4 )]
        [DataRow( new char[] {'a','b','c'},             new char[] {'a','b','d'},       false, 5 )]
        [DataRow( new char[] {'a','b','c'},             new char[] {'c','a','b'},       true,  6 )]
        [DataRow( new char[] {'a','b','c','c'},         new char[] {'c','a','c','b'},   true,  7 )]
        public void IMultiSet_MultiSetEquals_Char(char[] MultiSetArray1, char[] MultiSetArray2, bool expectedResult, int temp) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);

            Assert.AreEqual(ms1.MultiSetEquals(ms2), expectedResult);
            Assert.AreEqual(ms2.MultiSetEquals(ms1), expectedResult);
        }

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new string[] {},                              new string[] {},                    true,  1 )]
        [DataRow( new string[] {"aa"},                          new string[] {},                    false, 2 )]
        [DataRow( new string[] {"aa","aa","aa","bb","bb","cc"}, new string[] {"aa","bb","cc"},      false, 3 )]
        [DataRow( new string[] {"aa","bb","cc"},                new string[] {"aa","bb","cc"},      true,  4 )]
        [DataRow( new string[] {"aa","bb","cc"},                new string[] {"aa","bb","dd"},      false, 5 )]
        [DataRow( new string[] {"aa","bb","cc"},                new string[] {"cc","aa","bb"},      true,  6 )]
        [DataRow( new string[] {"aa","bb","cc","cc"},           new string[] {"cc","aa","cc","bb"}, true,  7 )]
        public void IMultiSet_MultiSetEquals_String(string[] MultiSetArray1, string[] MultiSetArray2, bool expectedResult, int temp) 
        {
            MultiSet<string> ms1 = new MultiSet<string>(MultiSetArray1);
            MultiSet<string> ms2 = new MultiSet<string>(MultiSetArray2);

            Assert.AreEqual(ms1.MultiSetEquals(ms2), expectedResult);
            Assert.AreEqual(ms2.MultiSetEquals(ms1), expectedResult);
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {}, true,  1 )]
        [DataRow( new char[] {'a'}, false,  2 )]
        [DataRow( new char[] {'a','g'}, false,  3 )]
        public void IMultiSet_MultiSetEquals_Char(char[] MultiSetArray, bool expectedResult, int temp) 
        {
            MultiSet<char> ms = new MultiSet<char>(MultiSetArray);
            Assert.AreEqual(ms.IsEmpty, expectedResult);
        }

        #endregion


        #region More IMultiSet ===============================================================

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','b','c','a','a','c'}, 'a', 3 )]
        [DataRow( new char[] {'a','b','c','a','a','c'}, 'c', 2 )]
        [DataRow( new char[] {'a','b','c','a','a','c'}, 'b', 1 )]
        [DataRow( new char[] {'a','b','c','a','a','c'}, 'd', 0 )]
        public void IMultiSet_TIterator_Char(char[] MultiSetArray, char index, int expectedResult) 
        {
            MultiSet<char> ms = new MultiSet<char>(MultiSetArray);
            Assert.AreEqual(ms[index], expectedResult);
        }

        [TestMethod, TestCategory("IMultiSet")]
        public void IMultiSet_AsSet_Char() 
        {
            MultiSet<char> ms1 = new MultiSet<char>(new char[] {'a','a','b','c','c','a'});
            MultiSet<char> ms2 = new MultiSet<char>(new char[] {'a','b','c'});
            Assert.IsTrue(ms2.MultiSetEquals(ms1.AsSet()));
        }

        #endregion


        #region Operators ===============================================================

        [DataTestMethod, TestCategory("Operators")]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {},              new char[] {'a','a','a','b','b','c'}, 1 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},   new char[] {'a','a','a','a','b','b','b','c','c'}, 2 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'c','c'},       new char[] {'a','a','a','b','b','c','c','c'}, 3 )]
        public void Operator_Plus_UnionWith_Char(char[] MultiSetArray1, char[] MultiSetArray2, char[] expectedResult, int temp) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);
            MultiSet<char> ms3 = new MultiSet<char>(expectedResult);

            ms1 = ms1+ms2;

            Assert_MultiSet(ms1, ms3);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {},              new string[] {"a","a","a","b","b","c"}, 1 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","b","c"},   new string[] {"a","a","a","a","b","b","b","c","c"}, 2 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"c","c"},       new string[] {"a","a","a","b","b","c","c","c"}, 3 )]
        public void Operator_Plus_UnionWith_String(string[] MultiSetArray1, string[] MultiSetArray2, string[] expectedResult, int temp) 
        {
            MultiSet<string> ms1 = new MultiSet<string>(MultiSetArray1);
            MultiSet<string> ms2 = new MultiSet<string>(MultiSetArray2);
            MultiSet<string> ms3 = new MultiSet<string>(expectedResult);

            ms1 = ms1+ms2;

            Assert_MultiSet(ms1, ms3);
        }




        [DataTestMethod, TestCategory("Operators")]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {},              new char[] {}, 1 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},   new char[] {'a','b','c'}, 2 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'c','c'},       new char[] {'c'}, 3 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','a','b'},   new char[] {'a','a','b'}, 4 )]
        public void Operator_Multiply_IntersectWith_Char(char[] MultiSetArray1, char[] MultiSetArray2, char[] expectedResult, int temp) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);
            MultiSet<char> ms3 = new MultiSet<char>(expectedResult);

            ms1 = ms1*ms2;

            Assert_MultiSet(ms1, ms3);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {},              new string[] {}, 1 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","b","c"},   new string[] {"a","b","c"}, 2 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"c","c"},       new string[] {"c"}, 3 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","a","b"},   new string[] {"a","a","b"}, 4 )]
        public void Operator_Multiply_IntersectWith_String(string[] MultiSetArray1, string[] MultiSetArray2, string[] expectedResult, int temp) 
        {
            MultiSet<string> ms1 = new MultiSet<string>(MultiSetArray1);
            MultiSet<string> ms2 = new MultiSet<string>(MultiSetArray2);
            MultiSet<string> ms3 = new MultiSet<string>(expectedResult);

            ms1 = ms1*ms2;

            Assert_MultiSet(ms1, ms3);
        }




        [DataTestMethod, TestCategory("Operators")]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {},              new char[] {'a','a','a','b','b','c'}, 1 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},   new char[] {'a','a','b'}, 2 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'c','c'},       new char[] {'a','a','a','b','b'}, 3 )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','a','b'},   new char[] {'a','b','c'}, 4 )]
        public void Operator_Minus_ExceptWith_Char(char[] MultiSetArray1, char[] MultiSetArray2, char[] expectedResult, int temp) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);
            MultiSet<char> ms3 = new MultiSet<char>(expectedResult);

            ms1 = ms1-ms2;

            Assert_MultiSet(ms1, ms3);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {},              new string[] {"a","a","a","b","b","c"}, 1 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","b","c"},   new string[] {"a","a","b"}, 2 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"c","c"},       new string[] {"a","a","a","b","b"}, 3 )]
        [DataRow( new string[] {"a","a","a","b","b","c"}, new string[] {"a","a","b"},   new string[] {"a","b","c"}, 4 )]
        public void Operator_Minus_ExceptWith_String(string[] MultiSetArray1, string[] MultiSetArray2, string[] expectedResult, int temp) 
        {
            MultiSet<string> ms1 = new MultiSet<string>(MultiSetArray1);
            MultiSet<string> ms2 = new MultiSet<string>(MultiSetArray2);
            MultiSet<string> ms3 = new MultiSet<string>(expectedResult);

            ms1 = ms1-ms2;

            Assert_MultiSet(ms1, ms3);
        }

        #endregion
    }
}