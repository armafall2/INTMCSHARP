using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Deposit(int Identifiant, decimal montant)
        {

        }

        public void Withdraw(int Identifiant, decimal montant)
        {

        }

        public void Virement(int Identifiant, decimal montant)
        {

        }

        public void Prelevement(int Identifiant, decimal montant)
        {

        }

        public override string ToString()
        {
            return $"CompteBancaire [Identifiant: {Identifiant}, Solde: {Solde}]";
        }
    }

    class Transaction
    {
        public int Identifiant { get; set; }
        public decimal Montant { get; set; }
        public int Expediteur { get; set; }
        public int Destinataire { get; set; }
    }

    class StatutsTransac
    {
        public int Identifiant { get; set; }
        public string Statut { get; set; } 
    }





}
