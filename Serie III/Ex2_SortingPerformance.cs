using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Serie_III
{
    public struct SortData
    {
        public long InsertionMean { get; set; }
        public long InsertionStd { get; set; }
        public long QuickMean { get; set; }
        public long QuickStd { get; set; }
    }

    public static class SortingPerformance
    {
        public static void DisplayPerformances(List<int> sizes, int count)
        {
            Console.WriteLine("n;InsertMean;InsertStd;QuickMean;QuicStd");
            List<SortData> resAff = new List<SortData>();

            resAff = PerformancesTest(sizes, count);
            int i = 0;
            foreach(var element in resAff)
            {
                
                Console.WriteLine($"{sizes[i]};{element.InsertionMean};{element.InsertionStd};{element.QuickMean};{element.QuickStd}");
                i++;
            }
            
        }

        public static List<SortData> PerformancesTest(List<int> sizes, int count)
        {
            List<SortData> result = new List<SortData>();
           
            foreach (var element in sizes)
            {
                result.Add(PerformanceTest(element, count));
            }
            return result;
        }

        public static SortData PerformanceTest(int size, int count)
        {
            
            List<int[]> result;
            long qsCumul = 0;
            long isCumul = 0;
            long isVariance = 0;
            long qsVariance = 0;
            for (int i = 0; i < count; i++)
            {
                result = ArraysGenerator(size);
                qsCumul += UseQuickSort(result[1]);
                isCumul += UseInsertionSort(result[0]);
            }

            SortData resultCalc = new SortData();

            resultCalc.InsertionMean = isCumul / count;
            resultCalc.QuickMean = qsCumul / count;

            for (int i = 0; i < count; i++)
            {
               result = ArraysGenerator(size);
               isVariance = (long)Math.Pow(UseInsertionSort(result[0]) - resultCalc.InsertionMean, 2);
               qsVariance = (long)Math.Pow(UseQuickSort(result[1])     - resultCalc.QuickMean,     2);
            }

            resultCalc.InsertionStd = (long)Math.Sqrt(isVariance);
            resultCalc.QuickMean    = (long)Math.Sqrt(qsVariance);

            return resultCalc;
        }

        private static List<int[]> ArraysGenerator(int size)
        {
            int[] tableauQuick = new int[size];
            int[] tableauInser = new int[size];

            List<int[]> result = new List<int[]>();
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                int randomNumber = random.Next();
                tableauInser[i] = randomNumber;
                tableauQuick[i] = randomNumber;
            }

            result.Add(tableauInser);
            result.Add(tableauQuick);

            return result;
        }

        public static long UseInsertionSort(int[] array)
        {
            Stopwatch isw = Stopwatch.StartNew();
            InsertionSort(array);
            isw.Stop();
            return isw.ElapsedMilliseconds;
        }

        public static long UseQuickSort(int[] array)
        {
            Stopwatch qs = Stopwatch.StartNew();
            QuickSort(array, 0, array.Length - 1);
            qs.Stop();
            return qs.ElapsedMilliseconds;
        }

        private static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j = j - 1;
                }

                array[j + 1] = key;
            }
        }

        private static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right);
                QuickSort(array, left, pivot - 1);
                QuickSort(array, pivot + 1, right);
            }
        }

        private static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            int temp1 = array[i + 1];
            array[i + 1] = array[right];
            array[right] = temp1;

            return i + 1;
        }
    }
}
