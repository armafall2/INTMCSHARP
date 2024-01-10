using System;
using System.Collections.Generic;
using System.IO;
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

            string path = Directory.GetCurrentDirectory();

            string acctPath = path + @"\Comptes_1.txt";

            string transacPath = path + @"\Transac_1.txt";

            string resultPath = path + @"\result_1.txt";
 
            int cpt = 0;

            GestionTransac        gestionTransacCSV = new GestionTransac();
            GestionCompteBancaire gestionComptesCSV = new GestionCompteBancaire();
            Transaction              transactionCSV = new Transaction();
            CompteBancaire           compteBancaire = new CompteBancaire();

            StreamReader accountFile = new StreamReader(acctPath);
            StreamReader transacFile = new StreamReader(transacPath);
            StreamWriter ResultaFile = new StreamWriter(resultPath);

            while (!accountFile.EndOfStream)
            {
                string line = accountFile.ReadLine();
                string[] stk = line.Split(';');


                string montantStr = string.IsNullOrEmpty(stk[1]) ? "0" : stk[1];

                montantStr = montantStr.Replace(".", ",");

                if (decimal.TryParse(montantStr, out decimal montantAvecDecimal))
                {

                    gestionComptesCSV.CreateBankAccount(montantAvecDecimal);
                }
                else
                {
                    Console.WriteLine("La conversion en décimal a échoué. Format incorrect.");
                }

            }
            accountFile.Close();

            Console.WriteLine(gestionComptesCSV.ToString());

            while (!transacFile.EndOfStream)
            {
                string line = transacFile.ReadLine();
                string[] stk = line.Split(';');
                string montantStr = stk[0];
                montantStr = montantStr.Replace(".", ",");
                string resultAEcrire = "";
                if (stk.Length == 3)
                {
                    if (decimal.TryParse(montantStr, out decimal montantAvecDecimal))
                    {

                        gestionTransacCSV.AjouterTransaction(montantAvecDecimal, int.Parse(stk[1]), int.Parse(stk[2]));
                        cpt++;

                        bool resultTransac = gestionTransacCSV.DoTransac(gestionTransacCSV.GetTransactionById(cpt), gestionComptesCSV, gestionTransacCSV.NatureOfTransac(gestionTransacCSV.GetTransactionById(cpt)));
                        decimal montant = gestionTransacCSV.GetTransactionById(cpt).Montant;
                        int exp = gestionTransacCSV.GetTransactionById(cpt).Expediteur;
                        int dest = gestionTransacCSV.GetTransactionById(cpt).Destinataire;
                        int id = gestionTransacCSV.GetTransactionById(cpt).Identifiant;

                        resultAEcrire = $"{GetKOOK(resultTransac)};{id};{montant};{exp};{dest}";
                    }
                    else
                    {
                        resultAEcrire = $"KO;{stk[0]};{stk[1]};{stk[2]}";

                    }
                    
                }
                else
                {
                    resultAEcrire += "KO";
                    for(int i = 0; i < stk.Length; i++)
                    {
                        resultAEcrire += ";" + stk[i];
                    }
                    resultAEcrire += ";";
                }

                Console.WriteLine(resultAEcrire);
                ResultaFile.WriteLine(resultAEcrire);

            }

            transacFile.Close();

            #region
            /*
            for (int j = 0; j < cpt; j++)
            {
                bool resultTransac = gestionTransacCSV.DoTransac(gestionTransacCSV.GetTransactionById(j + 1), gestionComptesCSV, gestionTransacCSV.NatureOfTransac(gestionTransacCSV.GetTransactionById(j + 1)));
                decimal montant = gestionTransacCSV.GetTransactionById(j + 1).Montant;
                int exp = gestionTransacCSV.GetTransactionById(j + 1).Expediteur;
                int dest = gestionTransacCSV.GetTransactionById(j + 1).Destinataire;
                int id = gestionTransacCSV.GetTransactionById(j + 1).Identifiant;


                string resultAEcrire = $"{GetKOOK(resultTransac)};{id};{montant};{exp};{dest}";

                Console.WriteLine(resultAEcrire);

                ResultaFile.WriteLine(resultAEcrire);
            }
            */
            #endregion
            Console.WriteLine(gestionComptesCSV.ToString());
            ResultaFile.Close();
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
