using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPart2
{
    class Program
    {
        static void Main()
        {
            string path = Directory.GetCurrentDirectory();

            for (int i = 1; i < 2; i++)
            {
                try
                {
                    #region Files
                    // Input
                    string mngrPath = path + $@"\Gestionnaires_{i}.txt";
                    string oprtPath = path + $@"\Comptes_{i}.txt";
                    string trxnPath = path + $@"\Transactions_{i}.txt";
                    // Output
                    string sttsOprtPath = path + $@"\StatutOpe_{i}.txt";
                    string sttsTrxnPath = path + $@"\StatutTra_{i}.txt";
                    string mtrlPath = path + $@"\Metrologie_{i}.txt";
                    #endregion

                    #region Declarative Stream Reader/Writer
                    StreamReader gestionFile = new StreamReader(mngrPath);
                    StreamReader comptesFile = new StreamReader(oprtPath);
                    StreamReader transactionsFile = new StreamReader(trxnPath);
                    StreamWriter sttOperFile = new StreamWriter(sttsOprtPath);
                    StreamWriter sttTranFile = new StreamWriter(sttsTrxnPath);
                    StreamWriter metroloFile = new StreamWriter(mtrlPath);
                    #endregion

                    #region Declarative Objet
                    ListageGestionnaireCompte listageGestionnaireCompte = new ListageGestionnaireCompte();
                    ListageTransaction listageTransaction = new ListageTransaction();
                    Transaction transaction = new Transaction();
                    ListageCompteBancaire listageCompteBancaire = new ListageCompteBancaire();
                    CompteBancaire compteBancaire = new CompteBancaire();
                    #endregion
                    
                    
                    #region Main
                    if (File.Exists(mngrPath) && File.Exists(oprtPath) && File.Exists(trxnPath))
                    {
                        #region Ajout Fichier Gestion
                        while (!gestionFile.EndOfStream)
                        {
                            string line = gestionFile.ReadLine();
                            string[] stk = line.Split(';');

                            if (int.TryParse(stk[0], out int id) && (stk[1] == "Particulier" || stk[1] == "Entreprise") && int.TryParse(stk[2], out int nbrTransac) && nbrTransac >= 0)
                            {
                                listageGestionnaireCompte.CreateGestionnaire(id, stk[1], nbrTransac);
                            }

                        }
                        listageGestionnaireCompte.AffGestionnaire();
                        Console.WriteLine(" ");
                        #endregion
                        
                        #region Ajout Fichier Transaction
                        while (!transactionsFile.EndOfStream)
                        {

                            string line = transactionsFile.ReadLine();
                            string[] stk = line.Split(';');
                            string res = "";
                            DateTime tmp = DateTime.Now;
                            decimal montant = 0;
                            int exp = 0;
                            int dest = 0;
                            i = 0;

                            if (int.TryParse(stk[0], out int id) && DateTime.TryParse(stk[1], out tmp) && decimal.TryParse(stk[2], out montant) && int.TryParse(stk[3], out exp) && int.TryParse(stk[4], out dest) && stk.Length == 5)
                            {
                                listageTransaction.AjouterTransaction(id, tmp, montant, exp, dest);
                            }
                            else
                            {
                                for (int j = 0; j < stk.Length; j++)
                                {
                                    res += stk[j] + ";";
                                }
                                Console.WriteLine("Erreur : " + res);
                            }

                        }
                        listageTransaction.AfficheTransac();
                        Console.WriteLine(" ");
                        #endregion
                        
                        #region Ajout Fichier Comptes
                        while (!comptesFile.EndOfStream)
                        {
                            string line = comptesFile.ReadLine();
                            string[] stk = line.Split(';');
                            string res = "";
                            DateTime tmp = DateTime.Now;
                            decimal montant = 0;
                            int EntreValue = 0;
                            int SortieValue = 0;

                            if (stk.Length == 5 &&
                                int.TryParse(stk[0], out int id) &&
                                DateTime.TryParse(stk[1], out tmp) &&
                                decimal.TryParse(stk[2], out montant))
                            {

                                if(string.IsNullOrEmpty(stk[3]) && string.IsNullOrEmpty(stk[4]))
                                {
                                    Console.WriteLine("Les deux valeurs ne peuvent pas être null");
                                }

                                else if (string.IsNullOrEmpty(stk[3]) && !int.TryParse(stk[4], out SortieValue))
                                {
                                    Console.WriteLine("Le paramètre de sortie est incorrecte");
                                }
                                else if (string.IsNullOrEmpty(stk[4]) && !int.TryParse(stk[3], out EntreValue))
                                {
                                    Console.WriteLine("Le paramètre d'entrer est incorrecte");
                                }

                                else if (string.IsNullOrEmpty(stk[3]) && int.TryParse(stk[4], out SortieValue)){

                                    listageCompteBancaire.CreateBankAccount(id, tmp, montant, null, SortieValue);
                                }

                                else if (string.IsNullOrEmpty(stk[4]) && int.TryParse(stk[3], out EntreValue)){
                                    listageCompteBancaire.CreateBankAccount(id, tmp, montant, EntreValue, null);
                                }

                                else if(int.TryParse(stk[3], out EntreValue) && int.TryParse(stk[4], out SortieValue))
                                {
                                    listageCompteBancaire.CreateBankAccount(id, tmp, montant, EntreValue, SortieValue);
                                }

                                else
                                {
                                    for (int k = 0; k < stk.Length; k++)
                                    {
                                        res += stk[k] + ";";
                                    }
                                    Console.WriteLine("Erreur : " + res);
                                }

                            }
                            else
                            {
                                for (int k = 0; k < stk.Length; k++)
                                {
                                    res += stk[k] + ";";
                                }
                                Console.WriteLine("Erreur : " + res);
                            }
                        }
                        listageCompteBancaire.AffCompte();
                        Console.WriteLine(" ");
                        #endregion
                    

                    
                    
                    }

                    
                    #endregion

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Une exception s'est produite : {ex.Message}");
                }
            }
            Console.ReadKey();
        }

    }
}

