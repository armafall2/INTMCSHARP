using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    /*
     * (a) (1 point) Donner le nombre de blocs pour un niveau j donné, 1 ≤ j ≤ N
     *  -> 2j-1
     *
     * (b) (1/2 point) Quel est le nombre total de blocs au niveau N ?
     * -> N^2
     * (c) (1/2 point) Quelle est la position du sommet de la pyramide ?
     * -> N caractère a partir du bord gauche de l'écran
     *
     * (1 point) Déterminer les formules établissant gauche(j), droite(j)
     * -> gauche = N - J
     * -> droite = ?
     */

    public static class Pyramid
    {
        public static void PyramidConstruction(int n, bool isSmooth)
        {
            if(n <= 0)
            {
                Console.WriteLine("Pyramide a 0 ou négative");
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
                    string symbole ="";

                    if (isSmooth && i % 2 == 0)
                    {
                        symbole = "-";
                    }
                    else if (isSmooth && i % 2 != 0 || !isSmooth)
                    {
                        symbole = "+" ;
                    }

                    Console.Write(symbole);
                }

                Console.WriteLine(); 
            }
            Console.WriteLine();
        }
    }
}