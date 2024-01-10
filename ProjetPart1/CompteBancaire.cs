using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetPart1
{
    class CompteBancaire
    {
        public int Identifiant { get; set; }
        public decimal Solde { get; set; }

        public CompteBancaire()
        {
            Solde = 0;
        }
        public CompteBancaire(int identifiant, decimal soldeInitial)
        {
            Identifiant = identifiant;
            Solde = soldeInitial;
        }
        public override string ToString()
        {
            return $"Identifiant: {Identifiant}, Solde: {Solde}";
        }
    }

    class GestionCompteBancaire
    {
        private List<CompteBancaire> comptes;
        int dernierIdentifiant = 1;

        public GestionCompteBancaire()
        {
            comptes = new List<CompteBancaire>();
        }
        public CompteBancaire CreateBankAccount(decimal montantInitial)
        {
            CompteBancaire nouveauCompte = new CompteBancaire();

            if (montantInitial.Equals(null) || montantInitial == 0)
            {
                nouveauCompte.Identifiant = dernierIdentifiant;
                nouveauCompte.Solde = 0;

            }
            else
            {
                nouveauCompte.Identifiant = dernierIdentifiant;
                nouveauCompte.Solde = montantInitial;
            }

            comptes.Add(nouveauCompte);
            dernierIdentifiant++;

            return nouveauCompte;
        }
        public CompteBancaire CreateBankAccount()
        {
            
            CompteBancaire nouveauCompte = new CompteBancaire
            {
                Identifiant = dernierIdentifiant,
                Solde = 0
            };
            comptes.Add(nouveauCompte);
            dernierIdentifiant++;

            return nouveauCompte;

        }

        public bool Deposit(int identifiant, decimal montant)
        {
            CompteBancaire compte = GetCompteById(identifiant);

            if (compte != null)
            {
                compte.Solde += montant;
                return true;
            }
            else
            {
                Console.WriteLine($"Le compte avec l'identifiant {identifiant} n'existe pas.");
                return false;
            }
        }
        public bool Withdraw(int identifiant, decimal montant)
        {
            CompteBancaire compte = GetCompteById(identifiant);

            if (compte != null)
            {
                if (compte.Solde >= montant)
                {
                    compte.Solde -= montant;
                    return true;
                }
                else
                {
                    Console.WriteLine("Solde insuffisant.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"Le compte avec l'identifiant {identifiant} n'existe pas.");
                return false;
            }
        }
        public CompteBancaire GetCompteById(int identifiant)
        {
            return comptes.FirstOrDefault(c => c.Identifiant == identifiant);
        }
        public override string ToString()
        {
            string result = "Comptes Bancaires:\n";

            foreach (var compte in comptes)
            {
                result += compte.ToString() +" $"+ "\n";
            }
            
           

            return result;
        }
    }

    class Transaction
    {
        public int Identifiant { get; set; }
        public decimal Montant { get; set; }
        public int Expediteur { get; set; }
        public int Destinataire { get; set; }

        public void AffUneTransac(Transaction transac)
        {
            Console.WriteLine($"ID : {transac.Identifiant} Montant : {transac.Montant} Exp : {transac.Expediteur} Dest : {transac.Destinataire}");
        }
    }

    class GestionTransac
    {
        private List<Transaction> transactions;
        int dernierIdentifiant = 1;

        public GestionTransac()
        {
            transactions = new List<Transaction>();
        }
        public void AjouterTransaction(decimal montant, int expediteur, int destinataire)
        {
            Transaction nouvelleTransaction = new Transaction
            {
                Identifiant = dernierIdentifiant,
                Montant = montant,
                Expediteur = expediteur,
                Destinataire = destinataire
            };

            transactions.Add(nouvelleTransaction);

            dernierIdentifiant++;
        }
        public void AfficheTransac()
        {
            int cpt = 0;
            Console.WriteLine("Liste des transactions :");
            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine($"Identifiant: {transaction.Identifiant}, Montant: {transaction.Montant}, Expediteur: {transaction.Expediteur}, Destinataire: {transaction.Destinataire}");
                cpt++;
            }

            if (cpt == 0)
            {
                Console.WriteLine($"Liste transaction vide.");
            }
        }
        public Transaction GetTransactionById(int identifiant)
        {
            return transactions.FirstOrDefault(t => t.Identifiant == identifiant);
        }
        
        
        public string NatureOfTransac(Transaction transac)
        {
            string res = "";

            int exp = transac.Expediteur;
            int dest = transac.Destinataire;

            if(exp == 0 && dest != 0)
            {
                res = "dep";
            }

            else if(exp != 0 && dest == 0)
            {
                res = "wit";
            }

            else if(exp != 0 && dest != 0)
            {
                res = "vir";
            }

            else if (exp == 0 && dest == 0)
            {
                res = "error";
            }

                return res;
        }


        public bool IsPossible(Transaction transac, GestionCompteBancaire gestionComptes, string code)
        {
            bool res = false;

            CompteBancaire exp = gestionComptes.GetCompteById(transac.Expediteur);
            CompteBancaire dest = gestionComptes.GetCompteById(transac.Destinataire);

            switch (code)
            {
                case "dep":   
                     if(transac.Montant > 0)
                    {
                        res = true;
                    }
                     break;

                case "wit":
                    if(exp.Solde >= transac.Montant)
                    {
                        res = true;
                    }
                     break;

                case "vir":
                    if (!(exp.Solde < transac.Montant))
                    {
                        res = true;
                    }
                    break;

                case "error":
                    res = false;
                     break;

                default:
                    res = false;
                     break;
            }

            return res;
        }
        public bool DoTransac(Transaction transaction, GestionCompteBancaire gestionCompte, string code)
        {
            bool res = false;

            Console.WriteLine(gestionCompte.GetCompteById(transaction.Expediteur));

            
            CompteBancaire exp = gestionCompte.GetCompteById(transaction.Expediteur);
            CompteBancaire dest = gestionCompte.GetCompteById(transaction.Destinataire);

            switch (code)
            {
                case "dep":
                    if(IsPossible(transaction, gestionCompte, code))
                    {
                        gestionCompte.Deposit(dest.Identifiant, transaction.Montant);
                        res = true;
                    }
                    break;

                case "wit":
                    if(IsPossible(transaction, gestionCompte, code))
                    {
                        gestionCompte.Withdraw(exp.Identifiant, transaction.Montant);
                        res = true;
                    }

                    break;
                
                case "vir":
                    if(IsPossible(transaction, gestionCompte, code))
                    {
                        gestionCompte.Withdraw(exp.Identifiant, transaction.Montant);
                        gestionCompte.Deposit(dest.Identifiant, transaction.Montant);
                        res = true;
                    }
                    break;

                case "error":
                    res = false;
                    break;
                default:
                    res = false;
                    break;
            }
            return res;

        }
    }

}
