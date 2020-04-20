using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib
{
    [SpecialClass(3)]
    public class StatisticsStuff
    {
        public static double Mean(params int[] values)
        {
            if (values == null || values.Length == 0) return double.NaN;
            double mean;
            double sum = 0;

            for (int i = 0; i < values.Length; ++i)
            {
                sum += values[i];
            }

            mean = sum / values.Length;
            return mean;
        }

        public static double Median(params int[] values)
        {
            double median;
            if (values == null || values.Length == 0) return double.NaN;
            // if array has only one value, return the value
            if (values.Length == 1)
            {
                median = values[0];
            }
            else
            {
                int[] sorted = new int[values.Length];
                values.CopyTo(sorted, 0);
                Array.Sort(sorted);
                double middleValue = (sorted.Length / 2);

                if (sorted.Length % 2 == 0)
                {
                    int param1 = (int)middleValue;
                    int param2 = (int)middleValue - 1;
                    median = Mean(sorted[param1], sorted[param2]);
                }
                else
                {
                    median = sorted[(int)middleValue];
                }
            }

            return median;

        }

        public static List<int> Mode(params int[] values)
        {
            int[] sorted = new int[values.Length];
            values.CopyTo(sorted, 0);
            Array.Sort(sorted);

            List<int> result = new List<int>();
            var counts = new Dictionary<int, int>();
            int max = 0;
            foreach (int key in sorted)
            {
                int count = 1;
                if (counts.ContainsKey(key))
                {
                    count += counts[key];
                    if (count > max)
                    {
                        max = count;
                    }
                }
                counts[key] = count;
            }

            foreach (var count in counts)
            {

                if (count.Value == max)
                {
                    result.Add(count.Key);
                }
            }
            return result;
        }
    }
}
