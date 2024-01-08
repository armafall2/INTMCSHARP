using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Percolation
{
    class Program
    {
        static void Main(string[] args)
        {
            Percolation percolation = new Percolation(8);

            percolation.Open(0, 1);
            percolation.Open(0, 2);
            percolation.Open(0, 3);

            percolation.Open(1, 1);

            percolation.Open(2, 1);
            percolation.Open(2, 2);
            percolation.Open(2, 3);

            percolation.Open(3, 2);
            percolation.Open(3, 2);

            percolation.Open(4, 2);
            percolation.Open(5, 2);
            percolation.Open(6, 2);
            percolation.Open(7, 2);

            bool percolateResult = percolation.Percolate();
            percolation.PrintGrid();

            Console.WriteLine("La percolation a-t-elle lieu ? " + percolateResult);

            // Keep the console window open
            Console.WriteLine("----------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            #region Réponse Question
            #region Question x
            #endregion
            #region Question 1

            #endregion
            #endregion




        }
    }
}
