﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            AffText("Test Fonction");

            for (int i = 0; i < 10; i++)
            {
                // decimal randomDecimal = (decimal)randSoldeEntPart.Next() + ((decimal)randSoldeDecPart.Next(0, 99) / 100);

                gestionComptes.CreateBankAccount(1000, i);
            }



            Console.WriteLine("Liste des comptes bancaires :");
            Console.WriteLine(gestionComptes.ToString());

            gestionComptes.Deposit(1, 33.33M);

            Console.WriteLine(gestionComptes.ToString());

            gestionTransac.AfficheTransac();

            gestionTransac.AjouterTransaction(1, 1000, 2, 1);
            gestionTransac.AjouterTransaction(2, 3330.22M, 4, 5);
            gestionTransac.AjouterTransaction(3, 3330.22M, 9, 10);

            gestionTransac.AfficheTransac();

            transac = gestionTransac.GetTransactionById(1);

            Console.WriteLine("une transac");
            transac.AffUneTransac(transac);



            //gestionTransac.DoTransac(gestionTransac.GetTransactionById(1), gestionComptes);

            Console.WriteLine(gestionComptes.ToString());


            AffText("Mise en place CSV");
            Stopwatch chrono = new Stopwatch();
            chrono.Start();
            string path = Directory.GetCurrentDirectory();
            #region Files
            // Input
            string acctPath = path + @"\Comptes_1.txt";
            string trxnPath = path + @"\Transactions_1.txt";
            // Output
            string sttsPath = path + @"\Statut_1.txt";
            #endregion
            int cptGlo = 1;
            GestionTransac gestionTransacCSV = new GestionTransac();
            GestionCompteBancaire gestionComptesCSV = new GestionCompteBancaire();
            Transaction transactionCSV = new Transaction();
            CompteBancaire compteBancaire = new CompteBancaire();

            StreamReader accountFile = new StreamReader(acctPath);
            StreamReader transacFile = new StreamReader(trxnPath);
            StreamWriter ResultaFile = new StreamWriter(sttsPath);

            while (!accountFile.EndOfStream)
            {
                string line = accountFile.ReadLine();
                string[] stk = line.Split(';');
                int tmp = 0;

                string montantStr = string.IsNullOrEmpty(stk[1]) ? "0" : stk[1];
                montantStr = montantStr.Replace(".", ",");
                if (decimal.Parse(montantStr) == 0)
                {
                    if (int.TryParse(stk[0], out tmp)) { 
                    gestionComptesCSV.CreateBankAccount(0, tmp);
                    }
                }
                else
                {
                    if(int.TryParse(stk[0], out tmp)) { 
                    gestionComptesCSV.CreateBankAccount(decimal.Parse(montantStr), tmp);
                    }
                }
            }


            accountFile.Close();
            Console.WriteLine(gestionComptesCSV.ToString());
            while (!transacFile.EndOfStream)
            {
                try
                {
                    string line = transacFile.ReadLine();
                    string[] stk = line.Split(';');
                    string montantStr = stk[1];
                    montantStr = montantStr.Replace(".", ",");
                    string resultAEcrire = "";
                    int id = 0;

                    if (stk.Length == 4 && decimal.TryParse(montantStr, out decimal montantAvecDecimal) && int.TryParse(stk[0], out id) && montantAvecDecimal > 0)
                    {
                        if (gestionTransacCSV.AjouterTransaction(id, montantAvecDecimal, int.Parse(stk[2]), int.Parse(stk[3])))
                        {
                            bool resultTransac = gestionTransacCSV.DoTransac(gestionTransacCSV.GetTransactionById(id), gestionComptesCSV, gestionTransacCSV.NatureOfTransac(gestionTransacCSV.GetTransactionById(id)));
                            transactionCSV = gestionTransacCSV.GetTransactionById(id);
                            
                            resultAEcrire = $"{GetKOOK(resultTransac)};{transactionCSV.Identifiant};{transactionCSV.Montant};{transactionCSV.Expediteur};{transactionCSV.Destinataire}";
                        }
                        else
                        {
                            HandleErrorCase(ref resultAEcrire, stk);
                        }
                    }
                    else
                    {
                        HandleErrorCase(ref resultAEcrire, stk);
                    }
                    resultAEcrire = resultAEcrire.Replace(',', '.');

                    Console.WriteLine(resultAEcrire);

                    ResultaFile.WriteLine(resultAEcrire);

                    ++cptGlo;
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Une exception s'est produite : {ex.Message}");

                }
            }

        transacFile.Close();

            //gestionTransacCSV.AfficheTransac();
            //gestionTransacCSV.AfficheTransac();
            Console.WriteLine(gestionComptesCSV.ToString());

            ResultaFile.Close();
            chrono.Stop();
            Console.WriteLine($"terminer {cptGlo-1} transac en {chrono.ElapsedMilliseconds} ms");
            Console.ReadKey();
        }


    /// <summary>
    /// Boucle de répétition du message d'erreur
    /// Met KO suivi du contenu de la ligne
    /// </summary>
    /// <param name="resultAEcrire"></param>
    /// <param name="stk"></param>
    private static void HandleErrorCase(ref string resultAEcrire, string[] stk)
    {
        resultAEcrire += "KO";

        for (int i = 0; i < stk.Length; i++)
        {
            resultAEcrire += ";" + stk[i];
        }

        resultAEcrire += ";";
    }

    /// <summary>
    /// Affichage du titre précédé de 100 -
    /// </summary>
    /// <param name="titre"></param>
    public static void AffText(string titre)
        {
            for (int i = 0; i < 100; i++)
                Console.Write("-");
            Console.WriteLine(" ");
            Console.WriteLine(titre);
            Console.WriteLine(" ");
        }

        /// <summary>
        /// Retourne OK ou KO pour l'affichage de la chaîne en fonction d'un booléen en entré
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
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
