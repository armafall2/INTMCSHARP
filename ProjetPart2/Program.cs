using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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
                    string mngrPath = Path.Combine(path, $"Gestionnaires_{i}.txt");
                    string oprtPath = Path.Combine(path, $"Comptes_{i}.txt");
                    string trxnPath = Path.Combine(path, $"Transactions_{i}.txt");
                    // Output
                    string sttsOprtPath = Path.Combine(path, $"StatutOpe_{i}.txt");
                    string sttsTrxnPath = Path.Combine(path, $"StatutTra_{i}.txt");
                    string mtrlPath = Path.Combine(path, $"Metrologie_{i}.txt");
                    #endregion

                    #region Declarative Objet
                    ListageGestionnaireCompte listageGestionnaireCompte = new ListageGestionnaireCompte();
                    ListageTransaction listageTransaction = new ListageTransaction();
                    Transaction transaction = new Transaction();
                    ListageCompteBancaire listageCompteBancaire = new ListageCompteBancaire();
                    CompteBancaire compteBancaire = new CompteBancaire();
                    GestionnairesCompte gestionnairesCompte = new GestionnairesCompte();
                    #endregion

                    int cpt = 0;
                    int cptCompte = 0;

                    int compteurNbCompte = 0;
                    int compteurNbTransa = 0;
                    int compteurNbReussi = 0;
                    int compteurNbEchecs = 0;

                    decimal MontantTotalReussite = 0;
                    decimal MontantFrais = 0;

                    #region Main
                    if (File.Exists(mngrPath) && File.Exists(oprtPath) && File.Exists(trxnPath))
                    {
                        using (var gestionFile = new StreamReader(mngrPath)) 
                        using (var comptesFile = new StreamReader(oprtPath))
                        using (var transactionsFile = new StreamReader(trxnPath))
                        using (var sttOperFile = new StreamWriter(sttsOprtPath))
                        using (var sttTranFile = new StreamWriter(sttsTrxnPath))
                        using (var metroloFile = new StreamWriter(mtrlPath))
                        { 
                            {
                                #region Ajout Fichier Gestion
                                while (!gestionFile.EndOfStream)
                                {
                                    string line = gestionFile.ReadLine();
                                    string[] stk = line.Split(';');

                                    if (int.TryParse(stk[0], out int id) && (stk[1] == "Particulier" || stk[1] == "Entreprise") && int.TryParse(stk[2], out int nbrTransac) && nbrTransac >= 0)
                                    {
                                        listageGestionnaireCompte.CreateGestionnaire(id, stk[1], nbrTransac);
                                        cpt++;
                                    }
                                    else
                                    {
                                        compteurNbEchecs++;
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
                                        compteurNbTransa++;
                                        compteurNbReussi++;
                                        listageTransaction.AjouterTransaction(id, tmp, montant, exp, dest);

                                    }
                                    else
                                    {
                                        for (int j = 0; j < stk.Length; j++)
                                        {
                                            res += stk[j] + ";";
                                        }
                                        Console.WriteLine("Erreur : " + res);
                                        compteurNbEchecs++;
                                    }
                                }
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

                                    if (string.IsNullOrEmpty(stk[2]))
                                    {
                                        stk[2] = "0";
                                    }

                                    if (stk.Length == 5 &&
                                        int.TryParse(stk[0], out int id) &&
                                        DateTime.TryParse(stk[1], out tmp) &&
                                        decimal.TryParse(stk[2], out montant))
                                    {
                                        if (string.IsNullOrEmpty(stk[3]) && string.IsNullOrEmpty(stk[4]))
                                        {
                                            Console.WriteLine("Les deux valeurs ne peuvent pas être null");
                                            compteurNbEchecs++;
                                        }
                                        else if (string.IsNullOrEmpty(stk[3]) && !int.TryParse(stk[4], out SortieValue))
                                        {
                                            Console.WriteLine("Le paramètre de sortie est incorrecte");
                                            compteurNbEchecs++;
                                        }
                                        else if (string.IsNullOrEmpty(stk[4]) && !int.TryParse(stk[3], out EntreValue))
                                        {
                                            Console.WriteLine("Le paramètre d'entrer est incorrecte");
                                            compteurNbEchecs++;
                                        }
                                        else if (string.IsNullOrEmpty(stk[3]) && int.TryParse(stk[4], out SortieValue))
                                        {
                                            listageCompteBancaire.CreateBankAccount(id, tmp, montant, null, SortieValue);
                                            cptCompte++;
                                        }
                                        else if (string.IsNullOrEmpty(stk[4]) && int.TryParse(stk[3], out EntreValue))
                                        {
                                            listageCompteBancaire.CreateBankAccount(id, tmp, montant, EntreValue, null);
                                            cptCompte++;
                                        }
                                        else if (int.TryParse(stk[3], out EntreValue) && int.TryParse(stk[4], out SortieValue))
                                        {
                                            listageCompteBancaire.CreateBankAccount(id, tmp, montant, EntreValue, SortieValue);
                                            cptCompte++;
                                        }
                                        else
                                        {
                                            for (int k = 0; k < stk.Length; k++)
                                            {
                                                res += stk[k] + ";";
                                            }
                                            Console.WriteLine("Erreur : " + res);
                                            compteurNbEchecs++;
                                        }
                                    }
                                    else
                                    {
                                        for (int k = 0; k < stk.Length; k++)
                                        {
                                            res += stk[k] + ";";
                                        }
                                        Console.WriteLine("Erreur : " + res);
                                        compteurNbEchecs++;
                                    }
                                }
                                Console.WriteLine(" ");
                                #endregion

                                #region Logique Ajout Compte a Gestionnaire
                                string res2 = "";
                                for (int z = 0; z < cptCompte; z++)
                                {
                                    List<CompteBancaire> comptesTrouves = listageCompteBancaire.GetComptesByIdNotUn(z + 1);
                                    foreach (CompteBancaire element in comptesTrouves)
                                    {
                                        int id = element.Identifiant;
                                        int? entrer = element.Entrer;
                                        int? sortie = element.sortie;

                                        if (element.sortie == null && element.Entrer.HasValue)
                                        {
                                            GestionnairesCompte gestio = listageGestionnaireCompte.GetGestionnaireById((int)entrer);
                                            res2 += $"{element.AffCompte()} {GetKOOK(gestio.AddCompteToGestionnaire(element))}\n";
                                            
                                            compteurNbCompte++;
                                            compteurNbReussi++;
                                        }
                                        else if (element.Entrer == null && element.sortie.HasValue)
                                        {
                                            GestionnairesCompte gestio = listageGestionnaireCompte.GetGestionnaireById((int)sortie);
                                            CompteBancaire compt = listageCompteBancaire.GetCompteById(id);
                                            res2 += $"{element.AffCompte()} {GetKOOK(gestio.RemoveCompteFromGestion(compt))}\n";
                                            
                                            compteurNbCompte--;
                                            compteurNbReussi++;
                                        }
                                        else if (element.sortie.HasValue && element.Entrer.HasValue)
                                        {
                                            /**/
                                            bool resBoolAdd = false;
                                            bool resBoolDel = false;
                                            string tmp = "";
                                            GestionnairesCompte gestio = listageGestionnaireCompte.GetGestionnaireById((int)sortie);
                                            resBoolAdd = gestio.AddCompteToGestionnaire(element);
                                            gestio = listageGestionnaireCompte.GetGestionnaireById((int)entrer);
                                            CompteBancaire compt = listageCompteBancaire.GetCompteById(id);
                                            resBoolDel = gestio.RemoveCompteFromGestion(compt);

                                            if (!resBoolAdd && resBoolDel)
                                            {
                                                tmp = $"{element.AffCompte()} {GetKOOK(true)}\n";
                                                
                                                compteurNbCompte++;

                                            }
                                            if (resBoolAdd && !resBoolDel)
                                            {
                                                tmp = $"{element.AffCompte()} {GetKOOK(false)}\n";


                                            }
                                            if (!resBoolAdd && !resBoolDel)
                                            {
                                                tmp = $"{element.AffCompte()} {GetKOOK(false)}\n";

                                            }
                                            res2 += tmp;
                                            /**/



                                        }

                                    }
                                }
                                Console.WriteLine(res2);
                                sttOperFile.Write(res2);
                                #endregion

                                #region Affichage des Gestionnaires
                                for (int w = 0; w < cpt; w++)
                                {               
                                    GestionnairesCompte gestio = listageGestionnaireCompte.GetGestionnaireById(w + 1);
                                    gestio.AffGestionnaire();

                                }
                                #endregion

                                ListageCompteBancaire listCompteTypé = new ListageCompteBancaire();

                                for (int w = 0; w < cpt; w++)
                                {
                                    GestionnairesCompte gestio = listageGestionnaireCompte.GetGestionnaireById(w + 1);
                                    List<CompteBancaire> listeCompteTemporaire = gestio.GetCompteBancaireList();

                                    foreach(var element in listeCompteTemporaire)
                                    {
                                        listCompteTypé.CreateBankAccount(element.Identifiant, element.DateEffet, element.Solde, element.Entrer, element.sortie);
                                        
                                        if (gestio.Type == "Particulier")
                                        {
                                            listCompteTypé.GetCompteById(element.Identifiant).type = gestio.Type;
                                            listCompteTypé.GetCompteById(element.Identifiant).gestio = gestio.Identifiant;
                                        }
                                        else if (gestio.Type == "Entreprise")
                                        {
                                            listCompteTypé.GetCompteById(element.Identifiant).type = gestio.Type;
                                            listCompteTypé.GetCompteById(element.Identifiant).gestio = gestio.Identifiant;
                                        }
                                    }
                                }
                                
                                
                                listCompteTypé.AffCompte();

                                listageTransaction.AfficheTransac();
                                
                                for(int boucleTransaction = 0; boucleTransaction < listageTransaction.nbDeTransac(); boucleTransaction++)
                                {
                                    //listageTransaction.GetTransactionById(boucleTransaction + 1).AffUneTransac(listageTransaction.GetTransactionById(boucleTransaction + 1));
                                    Transaction transactionAVerifier = listageTransaction.GetTransactionById(boucleTransaction + 1);
                                    string codeOpe = listageTransaction.NatureOfTransac(transactionAVerifier);

                                    bool resIsPossible = listageTransaction.IsPossible(transactionAVerifier, listCompteTypé, codeOpe);
                                    if(listageTransaction.IsPossible(transactionAVerifier, listCompteTypé, codeOpe))
                                    {
                                        bool resDoTransac = listageTransaction.DoTransac(transactionAVerifier, listCompteTypé, codeOpe);
                                    }
                                }
                                listCompteTypé.AffCompte();
                                /* foreach(var element in listCompteTypé.GetCompteBancaireList())
                                 {

                                 }
                                /**/



                                Console.WriteLine(" ");

                                string contentStat = "";

                                contentStat += $"Statistiques: \n" +
                                    $"Nombre de comptes: {compteurNbCompte}\n" +
                                    $"Nombre de transactions : {compteurNbTransa - 1}\n" +
                                    $"Nombre de réussites : {compteurNbReussi - 1}\n" +
                                    $"Nombre d'échecs : {compteurNbEchecs + CompterOccurrences(res2, "KO")}\n" +
                                    $"Montant total des réussites : {MontantTotalReussite} euros\n";


                                Console.WriteLine(contentStat);














                                gestionFile.Close();
                                comptesFile.Close();
                                transactionsFile.Close();
                                sttOperFile.Close();
                                sttTranFile.Close();
                                metroloFile.Close();

                                break;
                            }
                        }
                    }


                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Une exception s'est produite : {ex.Message}");
                }
                finally
                {
                    
                }
            }
            Console.ReadKey();
        }

        static string GetKOOK(bool entry)
        {
            return entry ? "OK" : "KO";
        }

        static int CompterOccurrences(string texte, string recherche)
        {
            // Utilisation de Regex pour compter les occurrences
            MatchCollection matches = Regex.Matches(texte, recherche);

            return matches.Count;
        }
    }
}