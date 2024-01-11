using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetPart1
{
    class CompteBancaire
    {
        public int Identifiant { get; set; }
        public decimal Solde { get; set; }

        public List<decimal> derniersRetraits = new List<decimal>();

        public CompteBancaire()
        {
            Solde = 0;
        }

        public CompteBancaire(int identifiant, decimal soldeInitial)
        {
            Identifiant = identifiant;
            Solde = soldeInitial;
        }

        public void test()
        {
            foreach(var element in derniersRetraits)
            {
                Console.WriteLine(element);
            }
        }

        /// <summary>
        /// Vérifie si un retrait est autorisé en fonction du montant max de retrait (1000)
        /// </summary>
        /// <param name="montant"></param>
        /// <returns></returns>
        internal bool EstRetraitAutorise(decimal montant)
        {
            if (derniersRetraits.Count >= 10)
            {
                derniersRetraits.RemoveAt(0);
            }

            decimal sommeDerniersRetraits = derniersRetraits.Sum();
            return (sommeDerniersRetraits + montant) <= 1000;
        }

        /// <summary>
        /// Ajoute le retrait a la list
        /// </summary>
        /// <param name="montant"></param>
        internal void AjouterRetrait(decimal montant)
        {
            derniersRetraits.Add(montant);
        }

        /// <summary>
        /// Retourne le compte avec un affichage
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Identifiant: {Identifiant}, Solde: {Solde}";
        }
    }

    class GestionCompteBancaire
    {
        private List<CompteBancaire> comptes;

        public GestionCompteBancaire()
        {
            comptes = new List<CompteBancaire>();
        }

        /// <summary>
        /// Crée un compte dans la liste GestionCompteBancaire
        /// avec un solde minimum de 0 ( il y a une surcharge si pas de solde)
        /// l'identifiant est autoincrémenté
        /// </summary>
        /// <param name="montantInitial"></param>
        /// <returns></returns>
        public bool CreateBankAccount(decimal montantInitial, int dernierIdentifiant)
        {
            
            if (comptes.Any(c => c.Identifiant == dernierIdentifiant))
            {
                Console.WriteLine("Le compte avec l'identifiant spécifié existe déjà.");
                return false;
            }

            if(montantInitial < 0)
            {
                Console.WriteLine("Solde nul");
                return false;
            }

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
            return true;
        }

        /// <summary>
        /// Dépot sur le compte identifié par identifiant, avec un montant
        /// </summary>
        /// <param name="identifiant"></param>
        /// <param name="montant"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrait sur le compte identifié par identifiant d'un montant
        /// </summary>
        /// <param name="identifiant"></param>
        /// <param name="montant"></param>
        /// <returns></returns>
        public bool Withdraw(int identifiant, decimal montant)
        {
            CompteBancaire compte = GetCompteById(identifiant);

            if (compte != null)
            {
                if (compte.Solde >= montant && compte.EstRetraitAutorise(montant))
                {
                    compte.Solde -= montant;
                  
                    return true;
                }
                else
                {
                    Console.WriteLine("Solde insuffisant ou retrait non autorisé.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"Le compte avec l'identifiant {identifiant} n'existe pas.");
                return false;
            }
        }

        /// <summary>
        /// Retourne un compte bancaire depuis la liste de gestionCompteBancaire
        /// en fonction d'un identifiant
        /// </summary>
        /// <param name="identifiant"></param>
        /// <returns></returns>
        public CompteBancaire GetCompteById(int identifiant)
        {
            return comptes.FirstOrDefault(c => c.Identifiant == identifiant);
        }

        /// <summary>
        /// Renvoi l'intégralité de liste de gestionCompteBancaire
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "Comptes Bancaires:\n";

            foreach (var compte in comptes)
            {
                result += compte.ToString() + " $\n";
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

        /// <summary>
        /// Affichage d'une transaction
        /// </summary>
        /// <param name="transac"></param>
        public void AffUneTransac(Transaction transac)
        {
            Console.WriteLine($"ID : {transac.Identifiant} Montant : {transac.Montant} Exp : {transac.Expediteur} Dest : {transac.Destinataire}");
        }
    }


    class GestionTransac
    {
        decimal montantMax = 1000;
        private List<Transaction> transactions;
      
        public GestionTransac()
        {
            transactions = new List<Transaction>();
        }

        /// <summary>
        /// Ajoute une transaction a la liste gestionTransac, identifiant auto-incrémenté
        /// </summary>
        /// <param name="montant"></param>
        /// <param name="expediteur"></param>
        /// <param name="destinataire"></param>
        public bool AjouterTransaction(int identifiant, decimal montant, int expediteur, int destinataire)
        {
            // Vérifier si la transaction avec le même identifiant existe déjà
            if (transactions.Any(t => t.Identifiant == identifiant))
            {
                return false;
            }
            else { 
            Transaction nouvelleTransaction = new Transaction
            {
                Identifiant = identifiant,
                Montant = montant,
                Expediteur = expediteur,
                Destinataire = destinataire
            };

            transactions.Add(nouvelleTransaction);
            return true;
            }
        }


        /// <summary>
        /// Affiche toute les transactions
        /// </summary>
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
        /// <summary>
        /// Récuperer Recuperer une transaction par rapport a un identifiant
        /// </summary>
        /// <param name="identifiant"></param>
        /// <returns></returns>
        public Transaction GetTransactionById(int identifiant)
        {
            return transactions.FirstOrDefault(t => t.Identifiant == identifiant);
        }
        /// <summary>
        /// Récupere la nature d'une transaction en fonction d'un identifiant
        /// la nature est retourné sous la forme d'un code string (de 3 char)
        /// </summary>
        /// <param name="transac"></param>
        /// <returns></returns>
        public string NatureOfTransac(Transaction transac)
        {
            string res = "";

            if(transac != null)
            {
                
            

            int exp = transac.Expediteur;

            int dest = transac.Destinataire;

            if (exp == 0 && dest != 0)
            {
                res = "dep";
            }
            else if (exp != 0 && dest == 0)
            {
                res = "wit";
            }
            else if (exp != 0 && dest != 0)
            {
                res = "vir";
            }
            else if (exp == 0 && dest == 0)
            {
                res = "error";
            }
            }
            return res;
            
        }
        /// <summary>
        /// Determine si une transaction est possible
        /// en fonction d'un code bancaire et d'une action a réaliser
        /// les conditions sont determiné dans l'énoncer
        /// </summary>
        /// <param name="transac"></param>
        /// <param name="gestionComptes"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool IsPossible(Transaction transac, GestionCompteBancaire gestionComptes, string code)
        {
            CompteBancaire exp = gestionComptes.GetCompteById(transac.Expediteur);
            CompteBancaire dest = gestionComptes.GetCompteById(transac.Destinataire);
            switch (code)
            {
                case "dep":
                    if (dest != null && transac.Montant > 0)
                    {
                        return true;
                    }
                    break;

                case "wit":
                    if (exp != null && exp.Solde >= transac.Montant && exp.EstRetraitAutorise(transac.Montant))
                    {
                        return true;
                    }
                    break;

                case "vir":
                    if (exp != null && dest != null && transac.Montant > 0 && exp.Solde >= transac.Montant && exp.EstRetraitAutorise(transac.Montant))
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Réalise la transaction si elle est possible
        /// et ajoute le montant au total de retrait pour savoir si un retrait ne 
        /// depasse ou ne va pas dépasser le total
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="gestionCompte"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool DoTransac(Transaction transaction, GestionCompteBancaire gestionCompte, string code)
        {
            bool res = false;
            if(transaction != null) {
            CompteBancaire exp = gestionCompte.GetCompteById(transaction.Expediteur);
            CompteBancaire dest = gestionCompte.GetCompteById(transaction.Destinataire);

            switch (code)
            {
                case "dep":
                    if (IsPossible(transaction, gestionCompte, code))
                    {
                        gestionCompte.Deposit(dest.Identifiant, transaction.Montant);
                        res = true;
                    }
                    break;

                case "wit":
                    if (IsPossible(transaction, gestionCompte, code))
                    {
                        gestionCompte.Withdraw(exp.Identifiant, transaction.Montant);
                        exp.AjouterRetrait(transaction.Montant); 
                        res = true;
                    }
                    break;

                case "vir":
                    if (IsPossible(transaction, gestionCompte, code))
                    {
                        gestionCompte.Withdraw(exp.Identifiant, transaction.Montant);
                        gestionCompte.Deposit(dest.Identifiant, transaction.Montant);
                        exp.AjouterRetrait(transaction.Montant);
                        res = true;
                    }
                    break;

            }
            }
            return res;
        }
    }
}
