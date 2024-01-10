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
            GestionTransac gestionTransac = new GestionTransac();
            GestionCompteBancaire gestionComptes = new GestionCompteBancaire();

            Transaction transac = new Transaction();


            Random randSoldeEntPart = new Random();
            Random randSoldeDecPart = new Random();
            decimal res = 0.1M;
            string affRes = "";

            AffText("Test Fonction");

            for (int i = 0; i < 10; i++)
            {
               // decimal randomDecimal = (decimal)randSoldeEntPart.Next() + ((decimal)randSoldeDecPart.Next(0, 99) / 100);

                gestionComptes.CreateBankAccount(1000);
            }

           

            Console.WriteLine("Liste des comptes bancaires :");
            Console.WriteLine(gestionComptes.ToString());

            gestionComptes.Deposit(1, 33.33M);

            Console.WriteLine(gestionComptes.ToString());

            gestionTransac.AfficheTransac();

            gestionTransac.AjouterTransaction(1000, 2, 1);
            gestionTransac.AjouterTransaction(3330.22M, 4, 5);
            gestionTransac.AjouterTransaction(3330.22M, 9, 10);

            gestionTransac.AfficheTransac();

            transac = gestionTransac.GetTransactionById(1);

            Console.WriteLine("une transac");
            transac.AffUneTransac(transac);


            
            //gestionTransac.DoTransac(gestionTransac.GetTransactionById(1), gestionComptes);

            Console.WriteLine(gestionComptes.ToString());


            AffText("Mise en place CSV");


            GestionTransac        gestionTransacCSV = new GestionTransac();
            GestionCompteBancaire gestionComptesCSV = new GestionCompteBancaire();
            Transaction              transactionCSV = new Transaction();

            gestionComptesCSV.CreateBankAccount(3000);
            gestionComptesCSV.CreateBankAccount(3000);

            Console.WriteLine(gestionComptesCSV.ToString());

            gestionTransacCSV.AjouterTransaction(200, 0, 1);
            gestionTransacCSV.AjouterTransaction(3000, 1, 0);
            gestionTransacCSV.AjouterTransaction(3000, 2, 1);
            gestionTransacCSV.AjouterTransaction(3000, 0, 0);

            gestionTransacCSV.AfficheTransac();

            int identifiant = 1;

            Console.WriteLine("test");

            transactionCSV = gestionTransacCSV.GetTransactionById(identifiant);

            transactionCSV.AffUneTransac(transactionCSV);

            Console.WriteLine(transactionCSV.Expediteur);

            gestionTransacCSV.DoTransac(gestionTransacCSV.GetTransactionById(identifiant), gestionComptesCSV, "dep");


            gestionTransacCSV.DoTransac(gestionTransacCSV.GetTransactionById(identifiant), gestionComptesCSV, gestionTransacCSV.NatureOfTransac(gestionTransacCSV.GetTransactionById(identifiant)));
            Console.WriteLine(gestionComptesCSV.ToString());

            Console.ReadKey();

        }

        public static void AffText(string titre)
        {
            for (int i = 0; i < 100; i++)
                Console.Write("-");
            Console.WriteLine(" ");
            Console.WriteLine(titre);
            Console.WriteLine(" ");
        }

        public static string GetKOOK(bool res)
        {
            string chaine = "";

            if (res)
                chaine = "OK";
            else
                chaine = "KO";

            return chaine;
        }
    }
}
