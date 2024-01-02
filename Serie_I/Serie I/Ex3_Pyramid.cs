using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    public static class Pyramid
    {
        public static void PyramidConstruction(int n, bool isSmooth)
        {
            if(n == 0)
            {
                Console.WriteLine("Pyramide a 0");
            }

            // Boucle principale en fonction du nombre en paramètre
            for (int i = 1; i <= n; i++)
            {
                // Boucle pour ajouter les espaces et ainsi centré la pyramide
                for (int espace = 0; espace < n - i; espace++)
                {
                    Console.Write(" ");
                }

                //Boucle pour le choix de quel bloc a placer en fonction de IsSmooth
                //Et ainsi changer les blocs qui sont dans les étages impairs par des "-"

                for (int bloc = 0; bloc < i * 2 - 1; bloc++)
                {
                    char symbole;

                    if (isSmooth || i % 2 == 0)
                    {
                        symbole = '+';
                    }
                    else
                    {
                        symbole = '-';
                    }

                    Console.Write(symbole);
                }

                Console.WriteLine();
            }
        }
    }
}
