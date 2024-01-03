using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    public static class Factorial
    {
        /*
         la version récursive peut être plus lente mais plus simple à l'écriture donc plus facilement maintenable et produit
        un programme plus petit en taille pour un même résultat.
        C'est donc la meilleur des deux.

        */

        public static int Factorial_(int n)
        {
            int result = 1;
            if (n == 0)
            {
                result = 1;
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    result *= (i + 1);
                }
            }
            return result;
        }

        public static int FactorialRecursive(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            else
            {
                return n * FactorialRecursive(n - 1);
            }
        }
    }
}