using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_II
{
    public static class Search
    {
        /*
         Dans le pire des cas, tout les éléments sont lu.
         */
        public static int LinearSearch(int[] tableau, int valeur)
        {
            if (tableau.Length == 0)
            {
                return -1;
            }
            else
            {
                for (int i = 0; i < tableau.Length; i++)
                {
                    if (tableau[i] == valeur)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        /*
         * dans le pire des cas, il y a 3 lecture pour ce tableau
         */
        public static int BinarySearch(int[] tableau, int valeur)
        {

            int gauche = 0;
            int droite = tableau.Length - 1;

            while (gauche <= droite)
            {
                int milieu = (gauche + droite) / 2;

                if (tableau[milieu] == valeur)
                {
                    return milieu;
                }
                else if (tableau[milieu] < valeur)
                {
                    gauche = milieu + 1;
                }
                else
                {
                    droite = milieu - 1;
                }
            }

            return -1;
        }
    }
}