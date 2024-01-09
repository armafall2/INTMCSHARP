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
            Random randsolde = new Random();


            for(int i = 0; i < 10; i++)
            {
                test.CreateBankAccount(i+1, randsolde.Next());
            }
           
           

            Console.WriteLine(test.ToString());





            Console.ReadKey();

        }
    }
}
