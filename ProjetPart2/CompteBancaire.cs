using System;
using System.Collections.Generic;
using System.Linq;


class GestionnairesCompte
{
    public int Identifiant { get; set; }
    public string Type { get; set; }
    public int nbrTransac { get; set; }

    public List<CompteBancaire> ListCompteBancaire;

    public GestionnairesCompte()
    {
        ListCompteBancaire = new List<CompteBancaire>();

    }

    public void AffGestionnaire()
    {
        Console.WriteLine($"{Identifiant} {Type} {nbrTransac}");

        if (ListCompteBancaire.Any())
        {
            foreach (var compte in ListCompteBancaire)
            {
                Console.WriteLine($"            Compte Id: {compte.AffCompte()}");
            }
        }
        else
        {
            Console.WriteLine("Aucun compte dans la liste.");
        }
        Console.WriteLine(" ");
    }
    public bool AddCompteToGestionnaire(CompteBancaire compte)
    {
        if (!ListCompteBancaire.Any(c => c.Identifiant == compte.Identifiant))
        {
            ListCompteBancaire.Add(compte);
            Console.WriteLine($"Le compte bancaire (ID: {compte.Identifiant}) a été ajouté au gestionnaire (ID: {Identifiant}).");
            Console.WriteLine(" ");
            return true;
            
        }
        else
        {
            Console.WriteLine($"Le compte bancaire (ID: {compte.Identifiant}) est déjà présent dans le gestionnaire (ID: {Identifiant}).");
            Console.WriteLine(" ");
            return false; 
        }
        
    }
    public bool RemoveCompteFromGestion(CompteBancaire compte)
    {
        if (ListCompteBancaire.Contains(compte))
        {
            ListCompteBancaire.Remove(compte);
            Console.WriteLine($"Le compte bancaire (ID: {compte.Identifiant}) a été retiré du gestionnaire (ID: {Identifiant}).");
            Console.WriteLine(" ");
            return true;
        }
        else
        {
            Console.WriteLine($"Le compte bancaire (ID: {compte.Identifiant}) n'est pas présent dans le gestionnaire (ID: {Identifiant}).");
            Console.WriteLine(" ");
            return false;
        }
    }
    public void CederCompte(GestionnairesCompte gestionnaireCible, CompteBancaire compte)
    {
        if (ListCompteBancaire.Contains(compte))
        {
            if (gestionnaireCible != null)
            {
                // Retirer le compte du gestionnaire actuel
                ListCompteBancaire.Remove(compte);
                Console.WriteLine($"Le compte (ID: {compte.Identifiant}) a été cédé au gestionnaire cible (ID: {gestionnaireCible.Identifiant}).");

                // Ajouter le compte au gestionnaire cible
                gestionnaireCible.AddCompteToGestionnaire(compte);
            }
            else
            {
                Console.WriteLine("Le gestionnaire cible n'existe pas.");
            }
        }
        else
        {
            Console.WriteLine("Impossible de céder le compte. Vérifiez s'il appartient au gestionnaire actuel et s'il est actif.");
        }
    }
    public void ReceptionnerCompte(GestionnairesCompte gestionnaireEmetteur, CompteBancaire compte)
    {
        if (gestionnaireEmetteur != null && gestionnaireEmetteur.ListCompteBancaire.Contains(compte))
        {
            // Retirer le compte du gestionnaire émetteur
            gestionnaireEmetteur.ListCompteBancaire.Remove(compte);
            Console.WriteLine($"Le compte (ID: {compte.Identifiant}) a été réceptionné du gestionnaire émetteur (ID: {gestionnaireEmetteur.Identifiant}).");

            // Ajouter le compte au gestionnaire actuel
            AddCompteToGestionnaire(compte);
        }
        else
        {
            Console.WriteLine("Impossible de réceptionner le compte. Vérifiez s'il appartient au gestionnaire émetteur.");
        }
    }

}
class ListageGestionnaireCompte
{
    List<GestionnairesCompte> gestionnairesComptes;

    public ListageGestionnaireCompte()
    {
        gestionnairesComptes = new List<GestionnairesCompte>();
    }

    public bool CreateGestionnaire(int identifiant, string type, int nbrTransac)
    {
        if (gestionnairesComptes.Any(c => c.Identifiant == identifiant))
        {
            Console.WriteLine("Le gestionnaire avec l'identifiant spécifié existe déjà.");
            return false;
        }

        GestionnairesCompte gestionnaires = new GestionnairesCompte();

        if((type == "Particulier" || type == "Entreprise") && nbrTransac > 0)
        {
            gestionnaires.Identifiant = identifiant;
            gestionnaires.Type = type;
            gestionnaires.nbrTransac = nbrTransac;

            gestionnairesComptes.Add(gestionnaires);

            return true;
        }
        return false;
    }
    public void AffGestionnaire()
    {
        foreach(var element in gestionnairesComptes)
        {
            Console.WriteLine($"Gestionnaire : Id : {element.Identifiant} Type : {element.Type} nbTransac : {element.nbrTransac}");
        }    
    }

    public GestionnairesCompte GetGestionnaireById(int identifiant)
    {
        return gestionnairesComptes.FirstOrDefault(t => t.Identifiant == identifiant);
    }

}
class Transaction
{
    public int Identifiant { get; set; }
    public DateTime DateEffet {get; set;}
    public decimal Montant { get; set; }
    public int Expediteur { get; set; }
    public int Destinataire { get; set; }

    public void AffUneTransac(Transaction transac)
    {
        Console.WriteLine($"ID : {transac.Identifiant} Date Effet : {transac.DateEffet} Montant : {transac.Montant} Exp : {transac.Expediteur} Dest : {transac.Destinataire}");
    }
}
class ListageTransaction
{
    List<Transaction> transactions;

    public ListageTransaction()
    {
        transactions = new List<Transaction>();
    }

    public bool AjouterTransaction(int identifiant, DateTime dateEffet, decimal montant, int expediteur, int destinataire)
    {

        if (transactions.Any(t => t.Identifiant == identifiant) && montant > 0)
        {
            Console.WriteLine($"Identifiant {identifiant} deja existant ou montant {montant} incorrect");
            return false;
        }
        else
        {

            Transaction nouvelleTransaction = new Transaction
            {
                Identifiant = identifiant,
                DateEffet = dateEffet,
                Montant = montant,
                Expediteur = expediteur,
                Destinataire = destinataire
            };

            transactions.Add(nouvelleTransaction);
            return true;
        }
    }
    public string NatureOfTransac(Transaction transac)
        {
            string res = "";

            if (transac != null)
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
    public bool IsPossible(Transaction transac, ListageCompteBancaire gestionComptes, string code)
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

    public Transaction GetTransactionById(int identifiant)
    {
        return transactions.FirstOrDefault(t => t.Identifiant == identifiant);
    }

    public void AfficheTransac()
    {
        int cpt = 0;
        Console.WriteLine("Liste des transactions :");
        foreach (Transaction transaction in transactions)
        {
            Console.WriteLine($"Identifiant: {transaction.Identifiant}, Date : {transaction.DateEffet} Montant: {transaction.Montant}, Expediteur: {transaction.Expediteur}, Destinataire: {transaction.Destinataire}");
            cpt++;
        }

        if (cpt == 0)
        {
            Console.WriteLine($"Liste transaction vide.");
        }
    }

    public bool DoTransac(Transaction transaction, ListageCompteBancaire gestionCompte, string code)
    {
        bool res = false;
        if (transaction != null)
        {
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

/// <summary>
/// ID DATE SOLDE ENTRER SORTIE
/// </summary>
class CompteBancaire
{
    public int Identifiant { get; set; }
    public DateTime DateEffet { get; set; }
    public decimal Solde { get; set; }
    public int? Entrer { get; set; }
    public int? sortie { get; set; }

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
        foreach (var element in derniersRetraits)
        {
            Console.WriteLine(element);
        }
    }

    public string AffCompte()
    {
        return ($"{Identifiant} {DateEffet} {Solde} {Entrer} {sortie}");
    }

    internal bool EstRetraitAutorise(decimal montant)
    {
        if (derniersRetraits.Count >= 10)
        {
            derniersRetraits.RemoveAt(0);
        }

        decimal sommeDerniersRetraits = derniersRetraits.Sum();
        return (sommeDerniersRetraits + montant) <= 1000;
    }
}
class ListageCompteBancaire
{
    List<CompteBancaire> listeCompteBancaires;

    public ListageCompteBancaire()
    {
        listeCompteBancaires = new List<CompteBancaire>();
    }

    public bool CreateBankAccount(int dernierIdentifiant, DateTime date, decimal montantInitial, int? entre, int? sortie)
    {
        if (montantInitial < 0)
        {
            Console.WriteLine("Solde Incorect : " + montantInitial);
            return false;
        }
        if(date == null)
        {
            Console.WriteLine("Date nul");
            return false;
        }
        CompteBancaire nouveauCompte = new CompteBancaire();

        if (montantInitial.Equals(null) || montantInitial == 0)
        {
            nouveauCompte.Identifiant = dernierIdentifiant;
            nouveauCompte.Solde = 0000;
            nouveauCompte.DateEffet = date;
            nouveauCompte.Entrer = entre;
            nouveauCompte.sortie = sortie;


        }
        else
        {
            nouveauCompte.Identifiant = dernierIdentifiant;
            nouveauCompte.Solde = montantInitial;
            nouveauCompte.DateEffet = date;
            nouveauCompte.Entrer = entre;
            nouveauCompte.sortie = sortie;
        }
        listeCompteBancaires.Add(nouveauCompte);
        return true;
    }
    
    public void AffCompte()
    {
        foreach(var element in listeCompteBancaires)
        {
            Console.WriteLine($"Id : {element.Identifiant}, Date : {element.DateEffet}, montant : {element.Solde}, Entrer : {element.Entrer} Sortie : {element.sortie}");
        }
    }

    public CompteBancaire GetCompteById(int identifiant)
    {
        return listeCompteBancaires.FirstOrDefault(t => t.Identifiant == identifiant);
        
    }

    public List<CompteBancaire> GetComptesByIdNotUn(int identifiant)
    {
        return listeCompteBancaires.Where(t => t.Identifiant == identifiant).ToList();
    }

    public List<CompteBancaire> GetCompteBancaireList()
    {
        return listeCompteBancaires;
    }
}