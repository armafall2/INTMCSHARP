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
        private static int dernierIdentifiant = 1;

        public GestionCompteBancaire()
        {
            comptes = new List<CompteBancaire>();
        }

        public CompteBancaire CreateBankAccount(decimal montantInitial)
        {
            CompteBancaire nouveauCompte = new CompteBancaire
            {
                Identifiant = dernierIdentifiant,
                Solde = montantInitial
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
                result += compte.ToString() + "\n";
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
        private static int dernierIdentifiant = 1;

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

        public bool IsPossible(Transaction transac, GestionCompteBancaire gestionComptes)
        {
            bool res = false;

            CompteBancaire exp = gestionComptes.GetCompteById(transac.Expediteur);
           
            if(!(exp.Solde < transac.Montant))
            {
                res = true;
            }
            
            return res;
        }
    
        public void DoTransac(Transaction transaction, GestionCompteBancaire gestionCompte)
        {
            
            if(IsPossible(transaction, gestionCompte))
            {
                gestionCompte.Deposit(transaction.Destinataire, transaction.Montant);
                gestionCompte.Withdraw(transaction.Expediteur, transaction.Montant);
            }
            else
            {
                Console.WriteLine("Impossible");
            }

        }
    }

    class StatutsTransac
    {
        public int Identifiant { get; set; }
        public string Statut { get; set; }
    }
}
