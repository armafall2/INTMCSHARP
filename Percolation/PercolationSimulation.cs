using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public int s { get; set; }

        public int t { get; set; }

        public string temps { get; set; }
    }

    public class PercolationSimulation
    {
        private Random random;

        public PercolationSimulation()
        {
            random = new Random();
        }
        /// <summary>
        /// Simule t fois une simulation de taille size * size
        /// et calcule la moyenne
        /// l'écart type 
        /// et la fraction (écart type / moyenne)
        /// </summary>
        /// <param name="size"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public PclData MeanPercolationValue(int size, int t)
        {
            Stopwatch chrono = new Stopwatch();
            chrono.Start();

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

            chrono.Stop();

            return new PclData
            {
                Mean = mean,
                StandardDeviation = standardDeviation,
                Fraction = fraction,
                s = size,
                t = t,
                temps = chrono.ElapsedMilliseconds.ToString(),
            };

        }
        /// <summary>
        /// 1 simulation de taille "size" qui est une grille de size * size
        /// Et qui selectionne des cases que le programme ouvre au hasard des cases
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public double PercolationValue(int size)
        {
            Percolation tmp = new Percolation(size);
            double count = 0;

            while (!tmp.Percolate())
            {
                int rowIndex = random.Next(0, size);
                int colIndex = random.Next(0, size);

                // Vérifier si la case est déjà ouverte
                if (!tmp.IsOpen(rowIndex, colIndex))
                {
                    tmp.Open(rowIndex, colIndex);
                    count++;
                }
            }
            return (count / (size * size));
        }

    }
}
