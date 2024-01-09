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
            GestionTransac test2 = new GestionTransac();
            GestionCompteBancaire gestionComptes = new GestionCompteBancaire();

            Transaction test9 = new Transaction();


            Random randSoldeEntPart = new Random();
            Random randSoldeDecPart = new Random();
            decimal res = 0.1M;


            for(int i = 0; i < 10; i++)
            {
               // decimal randomDecimal = (decimal)randSoldeEntPart.Next() + ((decimal)randSoldeDecPart.Next(0, 99) / 100);

                gestionComptes.CreateBankAccount(1000);
            }

            Console.WriteLine("Liste des comptes bancaires :");
            Console.WriteLine(gestionComptes.ToString());

            gestionComptes.Deposit(1, 33.33M);

            Console.WriteLine(gestionComptes.ToString());



            test2.AfficheTransac();

            test2.AjouterTransaction(1001, 2, 1);
            test2.AjouterTransaction(3330.22M, 4, 5);
            test2.AjouterTransaction(3330.22M, 9, 10);



            test2.AfficheTransac();

            test9 = test2.GetTransactionById(1);

            test9.AffUneTransac(test9);

            test2.DoTransac(test2.GetTransactionById(1), gestionComptes);

            Console.WriteLine(gestionComptes.ToString());


            Console.ReadKey();

        }
    }
}
