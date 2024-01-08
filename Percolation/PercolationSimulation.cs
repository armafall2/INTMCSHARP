using System;
using System.Collections.Generic;
using System.Linq;

namespace Percolation
{
    public struct PclData
    {
        /// <summary>
        /// Moyenne 
        /// </summary>
        public double Mean { get; set; }
        /// <summary>
        /// Ecart-type
        /// </summary>
        public double StandardDeviation { get; set; }
        /// <summary>
        /// Fraction
        /// </summary>
        public double Fraction { get; set; }
    }

    public class PercolationSimulation
    {
        private Random random;

        public PercolationSimulation()
        {
            random = new Random();
        }

        public PclData MeanPercolationValue(int size, int t)
        {
            double mean = 0;
            double standardDeviation = 0;
            double fraction = 0;
            double cumul = 0;

            List<double> res = new List<double>();

            for(int i = 0; i < t; i++)
            {
                res.Add(PercolationValue(size));
            }

            mean = res.Average();
            double sumSquaredDifferences = res.Sum(value => Math.Pow(value - mean, 2));

            double variance = sumSquaredDifferences / res.Count;

            standardDeviation = Math.Sqrt(variance);

            fraction = standardDeviation / mean;

            return new PclData
            {
                Mean = mean,
                StandardDeviation = standardDeviation,
                Fraction = fraction
            };

        }

        public double PercolationValue(int size)
        {
            Percolation tmp = new Percolation(size);
            double count = 0;
            tmp.Open(0,0);

            while (!tmp.Percolate())
            {
                int rowIndex = random.Next(0, size + 1);
                int colIndex = random.Next(0, size + 1);

                tmp.Open(rowIndex, colIndex);
                count++;
            }
            return (count / (size * size));
        }
    }
}
