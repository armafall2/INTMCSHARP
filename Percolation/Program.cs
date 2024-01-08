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
            percolation.Open(110, 2);


            bool percolateResult = percolation.Percolate();

            //percolation.PrintGrid();

            Console.WriteLine("La percolation a-t-elle lieu ? " + percolateResult);

            // Keep the console window open
            Console.WriteLine("----------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            #region Réponse Question
            #region Question x
            #endregion
            #region Exercice 2
                #region 3.
                    #region Question B
                        //Avec les deux boucles foreach, la complexité de Open() est de O(1) dans le pire des cas
                    #endregion
                    #region Question C
                        //Le nombre de voisin reste le même peut importe le nombre de case (4 : haut, bas, gauche, droite)
                    #endregion
                #endregion
            #endregion
            #endregion




        }
    }
}
