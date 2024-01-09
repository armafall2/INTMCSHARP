using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPart1
{
    class Program
    {
        static void Main(string[] args)
        {
            CompteBancaire test = new CompteBancaire();

            test.Identifiant = 1;
           

            Console.WriteLine(test.ToString());





            Console.ReadKey();

        }
    }
}
