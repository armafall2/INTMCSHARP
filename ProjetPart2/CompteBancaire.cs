using System;
using System.Collections.Generic;
using System.Linq;

class GestionnairesCompte
{
    public int Identifiant { get; set; }
    public string Type { get; set; }
    public int nbrTransac { get; set; }


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


}

class Transaction
{
    public int Identifiant { get; set; }
    public DateTime DateEffet {get; set;}
    public decimal Montant { get; set; }
    public int? Expediteur { get; set; }
    public int? Destinataire { get; set; }

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
}

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

        if (listeCompteBancaires.Any(c => c.Identifiant == dernierIdentifiant))
        {
            Console.WriteLine("Le compte avec l'identifiant spécifié existe déjà.");
            return false;
        }

        if (montantInitial < 0)
        {
            Console.WriteLine("Solde nul");
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
            nouveauCompte.Solde = 0;
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

}