using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_II
{
    public static class Eratosthene
    {
        public static int[] EratosthenesSieve(int n)
        {
         bool[] primes = new bool[n + 1];
         int count = 0;

    
        for (int i = 2; i <= n; i++)
        {
            primes[i] = true;
        }

        for (int p = 2; p * p <= n; p++)
        {
            if (primes[p])
            {
                for (int i = p * p; i <= n; i += p)
                {
                    primes[i] = false;
                }
            }
        }
        for (int i = 2; i <= n; i++)
        {
            if (primes[i])
            {
                count++;
            }
        }
        int[] result = new int[count];
        int index = 0;
        for (int i = 2; i <= n; i++)
        {
            if (primes[i])
            {
                result[index++] = i;
            }
        }
        return result;
    }
    }
}
