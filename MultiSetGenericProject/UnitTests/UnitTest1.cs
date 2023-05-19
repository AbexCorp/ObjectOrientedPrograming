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
        private void Assert_MultiSet_Char(MultiSet<char> ms1, MultiSet<char> ms2)
        {
            CollectionAssert.AreEqual(ms1.Dictionary, ms2.Dictionary);
        }

        private void Assert_MultiSet_String(MultiSet<string> ms1, MultiSet<char> ms2)
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



        //Something Goes wrong here
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','a','b','b','b',},             'c',   5,2 )]
        [DataRow( new char[] {'a','a','b','b','b','c'},          'c',   5,2 )]
        [DataRow( new char[] {'a','a','b','b','b','c','c'},      'c',   5,2 )]
        [DataRow( new char[] {'a','a','b','b','b','c','c','c'},  'c',   5,2 )]
        public void IMultiSet_RemoveAll_Char(char[] MultiSetArray, char removedValue, int expectedCount, int expectedLength) 
        {
            MultiSet<char> ms = new MultiSet<char>(MultiSetArray);

            ms.RemoveAll(removedValue);

            Assert.AreEqual(ms.Count, expectedCount);
            Assert.AreEqual(ms.Dictionary.Count, expectedLength);
            Assert.IsFalse(ms.Contains(removedValue));
        }

        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new string[] {"aa","aa","bb","bb","bb",},                "c",   5,2 )]
        [DataRow( new string[] {"aa","aa","bb","bb","bb","cc"},            "c",   5,2 )]
        [DataRow( new string[] {"aa","aa","bb","bb","bb","cc","cc"},       "c",   5,2 )]
        [DataRow( new string[] {"aa","aa","bb","bb","bb","cc","cc","cc"},  "c",   5,2 )]
        public void IMultiSet_RemoveAll_String(string[] MultiSetArray, string removedValue, int expectedCount, int expectedLength) 
        {
            MultiSet<string> ms = new MultiSet<string>(MultiSetArray);

            ms.RemoveAll(removedValue);

            Assert.AreEqual(ms.Count, expectedCount);
            Assert.AreEqual(ms.Dictionary.Count, expectedLength);
            Assert.IsFalse(ms.Contains(removedValue));
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {},              new char[] {'a','a','a','b','b','c'} )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},   new char[] {'a','a','a','a','b','b','b','c','c'} )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'c','c'},       new char[] {'a','a','a','b','b','c','c','c'} )]
        public void IMultiSet_UnionWith_Char(char[] MultiSetArray1, char[] MultiSetArray2, char[] expectedResult ) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);
            MultiSet<char> ms3 = new MultiSet<char>(expectedResult);

            ms1.UnionWith(ms2);

            Assert_MultiSet_Char(ms1, ms3);
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {},              new char[] {} )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},   new char[] {'a','b','c'} )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'c','c'},       new char[] {'c'} )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','a','b'},   new char[] {'a','a','b'} )]
        public void IMultiSet_IntersectWith_Char(char[] MultiSetArray1, char[] MultiSetArray2, char[] expectedResult ) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);
            MultiSet<char> ms3 = new MultiSet<char>(expectedResult);

            ms1.IntersectWith(ms2);

            Assert_MultiSet_Char(ms1, ms3);
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {},              new char[] {'a','a','a','b','b','c'} )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},   new char[] {'a','a','b'} )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'c','c'},       new char[] {'a','a','a','b','b'} )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','a','b'},   new char[] {'a','b','c'} )]
        public void IMultiSet_ExceptWith_Char(char[] MultiSetArray1, char[] MultiSetArray2, char[] expectedResult ) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);
            MultiSet<char> ms3 = new MultiSet<char>(expectedResult);

            ms1.ExceptWith(ms2);

            Assert_MultiSet_Char(ms1, ms3);
        }




        [DataTestMethod, TestCategory("IMultiSet")]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {},              new char[] {'a','a','a','b','b','c'} )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','b','c'},   new char[] {'a','a','b'} )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'c','c'},       new char[] {'a','a','a','b','b','c'} )]
        [DataRow( new char[] {'a','a','a','b','b','c'}, new char[] {'a','a','b'},   new char[] {'a','b','c'} )]
        [DataRow( new char[] {'a','b','c'},             new char[] {'b','c','d'},   new char[] {'a','d'} )]
        public void IMultiSet_SymetricExceptWith_Char(char[] MultiSetArray1, char[] MultiSetArray2, char[] expectedResult ) 
        {
            MultiSet<char> ms1 = new MultiSet<char>(MultiSetArray1);
            MultiSet<char> ms2 = new MultiSet<char>(MultiSetArray2);
            MultiSet<char> ms3 = new MultiSet<char>(expectedResult);

            ms1.SymmetricExceptWith(ms2);

            Assert_MultiSet_Char(ms1, ms3);
        }

        #endregion
    }
}