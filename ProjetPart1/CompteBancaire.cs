using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPart1
{
    class CompteBancaire
    {
        private Dictionary<int, decimal> comptes;

        public CompteBancaire()
        {
            comptes = new Dictionary<int, decimal>();
        }

        public bool CreateBankAccount(int identifiant, decimal montantInitial)
        {
            bool res = false;

            if (!comptes.ContainsKey(identifiant))
            {
                comptes.Add(identifiant, montantInitial);
                res = true;
            }
            else
            {
                res = false;
                throw new Exception($"Le compte avec l'identifiant {identifiant} existe déjà.");
            }
            return res;
        }

        public bool Deposit(int identifiant, decimal montant)
        {
            bool res = false;
            if (comptes.ContainsKey(identifiant))
            {
                comptes[identifiant] += montant;
                res = true;
            }
            else
            {
                res = false;
               throw new Exception($"Le compte avec l'identifiant {identifiant} n'existe pas.");
            }
            return res;
        }

        public bool Withdraw(int identifiant, decimal montant)
        {
            bool res = false;
            if (comptes.ContainsKey(identifiant))
            {
                
                if (comptes[identifiant] >= montant)
                {
                    comptes[identifiant] -= montant;
                    res = true;
                }
                else
                {
                    res = false;
                    throw new Exception("Solde insuffisant.");
                }
            }
            else
            {
                res = false;
                throw new Exception($"Le compte avec l'identifiant {identifiant} n'existe pas.");
            }
            return res;
        }

        public override string ToString()
        {
            string result = "Comptes Bancaires:\n";

            foreach (var compte in comptes)
            {
                result += $"Identifiant: {compte.Key}, Solde: {compte.Value}\n";
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

    }

    class GestionTransac : Transaction
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
            Console.WriteLine("Liste des transactions :");
            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine($"Identifiant: {transaction.Identifiant}, Montant: {transaction.Montant}, Expediteur: {transaction.Expediteur}, Destinataire: {transaction.Destinataire}");
            }
        }
    }

    class StatutsTransac
    {
        public int Identifiant { get; set; }
        public string Statut { get; set; }
    }





}
