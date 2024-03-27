using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace SortingTest
{
    class Program
    {
        static void Main()
        {
            BenchmarkRunner.Run<InsertionVsMergeVsQuickClassicVsQuick>();
        }
    }

    [MemoryDiagnoser]
    public class InsertionVsMergeVsQuickClassicVsQuick
    {
        [Params(20, 1000, 100000)]
        public static int size;

        public int[] random = new int[0];
        public int[] sorted = new int[0];
        public int[] reversed = new int[0];
        public int[] almost = new int[0];
        public int[] fewUnique = new int[0];

        [GlobalSetup]
        public void Setup()
        {
            random = Generators.Generators.GenerateRandom(size, 0, size);
            sorted = Generators.Generators.GenerateSorted(size, 0, size);
            reversed = Generators.Generators.GenerateReversed(size, 0, size);
            almost = Generators.Generators.GenerateAlmostSorted(size, 0, size);
            fewUnique = Generators.Generators.GenerateRandom(size, 0, size/10 + size%10);
        }


        [Benchmark]
        public void Insertion_Random() {   Sorting.InsertionSort.Sort((int[])random.Clone());  }
        [Benchmark]
        public void Insertion_Sorted() {   Sorting.InsertionSort.Sort((int[])sorted.Clone());  }
        [Benchmark]
        public void Insertion_Reversed() {   Sorting.InsertionSort.Sort((int[])reversed.Clone());  }
        [Benchmark]
        public void Insertion_AlmostA() {   Sorting.InsertionSort.Sort((int[])almost.Clone());  }
        [Benchmark]
        public void Insertion_FewUnique() {   Sorting.InsertionSort.Sort((int[])fewUnique.Clone());  }




        [Benchmark]
        public void Merge_Random() {   Sorting.MergeSort.Sort((int[])random.Clone());  }
        [Benchmark]
        public void Merge_Sorted() {   Sorting.MergeSort.Sort((int[])sorted.Clone());  }
        [Benchmark]
        public void Merge_Reversed() {   Sorting.MergeSort.Sort((int[])reversed.Clone());  }
        [Benchmark]
        public void Merge_AlmostA() {   Sorting.MergeSort.Sort((int[])almost.Clone());  }
        [Benchmark]
        public void Merge_FewUnique() {   Sorting.MergeSort.Sort((int[])fewUnique.Clone());  }




        [Benchmark]
        public void QuickSortClassic_Random() {   Sorting.QuickSortClassic.Sort((int[])random.Clone());  }
        [Benchmark]
        public void QuickSortClassic_Sorted() {   Sorting.QuickSortClassic.Sort((int[])sorted.Clone());  }
        [Benchmark]
        public void QuickSortClassic_Reversed() {   Sorting.QuickSortClassic.Sort((int[])reversed.Clone());  }
        [Benchmark]
        public void QuickSortClassic_AlmostA() {   Sorting.QuickSortClassic.Sort((int[])almost.Clone());  }
        [Benchmark]
        public void QuickSortClassic_FewUnique() {   Sorting.QuickSortClassic.Sort((int[])fewUnique.Clone());  }
        



        [Benchmark]
        public void QuickSort_Random() {   Sorting.QuickSort.Sort((int[])random.Clone());  }
        [Benchmark]
        public void QuickSort_Sorted() {   Sorting.QuickSort.Sort((int[])sorted.Clone());  }
        [Benchmark]
        public void QuickSort_Reversed() {   Sorting.QuickSort.Sort((int[])reversed.Clone());  }
        [Benchmark]
        public void QuickSort_AlmostA() {   Sorting.QuickSort.Sort((int[])almost.Clone());  }
        [Benchmark]
        public void QuickSort_FewUnique() {   Sorting.QuickSort.Sort((int[])fewUnique.Clone());  }
    }
}

namespace Sorting
{
    public static class InsertionSort
    {
        public static void Sort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i) {
                int key = arr[i];
                int j = i - 1;
 
                // Move elements of arr[0..i-1],
                // that are greater than key,
                // to one position ahead of
                // their current position
                while (j >= 0 && arr[j] > key) {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
        }
    }
    public static class MergeSort
    {
        static void Merge(int[] arr, int l, int m, int r)
        {
            // Find sizes of two
            // subarrays to be merged
            int n1 = m - l + 1;
            int n2 = r - m;
 
            // Create temp arrays
            int[] L = new int[n1];
            int[] R = new int[n2];
            int i, j;
 
            // Copy data to temp arrays
            for (i = 0; i < n1; ++i)
                L[i] = arr[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];
 
            // Merge the temp arrays
 
            // Initial indexes of first
            // and second subarrays
            i = 0;
            j = 0;
 
            // Initial index of merged
            // subarray array
            int k = l;
            while (i < n1 && j < n2) {
                if (L[i] <= R[j]) {
                    arr[k] = L[i];
                    i++;
                }
                else {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }
 
            // Copy remaining elements
            // of L[] if any
            while (i < n1) {
                arr[k] = L[i];
                i++;
                k++;
            }
 
            // Copy remaining elements
            // of R[] if any
            while (j < n2) {
                arr[k] = R[j];
                j++;
                k++;
            }
        }
 
        // Main function that
        // sorts arr[l..r] using
        // merge()
        static void Sort(int[] arr, int l, int r)
        {
            if (l < r) {
 
                // Find the middle point
                int m = l + (r - l) / 2;
 
                // Sort first and second halves
                Sort(arr, l, m);
                Sort(arr, m + 1, r);
 
                // Merge the sorted halves
                Merge(arr, l, m, r);
            }
        }

        public static void Sort(int[] arr)
        {
            Sort(arr, 0, arr.Length - 1);
        }
    }
    public static class QuickSortClassic
    {
        // A utility function to swap two elements
        static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
 
        // This function takes last element as pivot,
        // places the pivot element at its correct position
        // in sorted array, and places all smaller to left
        // of pivot and all greater elements to right of pivot
        static int Partition(int[] arr, int low, int high)
        {
            // Choosing the pivot
            int pivot = arr[high];
 
            // Index of smaller element and indicates
            // the right position of pivot found so far
            int i = (low - 1);
 
            for (int j = low; j <= high - 1; j++) {
 
                // If current element is smaller than the pivot
                if (arr[j] < pivot) {
 
                    // Increment index of smaller element
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, high);
            return (i + 1);
        }
 
        // The main function that implements QuickSort
        // arr[] --> Array to be sorted,
        // low --> Starting index,
        // high --> Ending index
        static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high) {
 
                // pi is partitioning index, arr[p]
                // is now at right place
                int pi = Partition(arr, low, high);
 
                // Separately sort elements before
                // and after partition index
                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }

        public static void Sort(int[] arr)
        {
            QuickSort(arr, 0, arr.Length-1);
        }
    }
    public static class QuickSort
    {
        public static void Sort(int[] arr)
        {
            Array.Sort(arr);
        }
    }
}

namespace Generators
{
    public static class Generators
    {
        static Random random = new Random();
        public static int[] GenerateRandom(int size, int minVal, int maxVal)
        {
            int[] a = new int[size];
            for(int i = 0; i < size; i++)
            {
                a[i] = random.Next(minVal, maxVal);
            }
            return a;
        }
        public static int[] GenerateSorted(int size, int minVal, int maxVal)
        {
            int[] a = GenerateRandom(size, minVal, maxVal);
            Array.Sort(a);
            return a;
        }
        public static int[] GenerateReversed(int size, int minVal, int maxVal)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);
            Array.Reverse(a);
            return a;
        }
        public static int[] GenerateAlmostSorted(int size, int minVal, int maxVal)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);
            int swaps = 0;
            if (size <= 20)
                swaps = 3;
            else if (size <= 1000)
                swaps = 30;
            else if(size <= 100000)
                swaps = 1000;
            
            for(int i = 0; i < swaps; i++)
            {
                int index1 = random.Next(0, size);
                int temp = a[index1];
                int index2 = random.Next(0, size);
                a[index1] = a[index2];
                a[index2] = temp;
            }
            return a;
        }
    }
}