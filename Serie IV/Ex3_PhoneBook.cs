using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Serie_IV
{
    public class PhoneBook
    {
        private Dictionary<string, string> contacts = new Dictionary<string, string>();

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 10 && phoneNumber[0] == '0' && phoneNumber[1] != '0')
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool ContainsPhoneContact(string phoneNumber)
        {
            return contacts.ContainsKey(phoneNumber);
        }

        public void AddPhoneNumber(string phoneNumber, string name)
        {
            if (IsValidPhoneNumber(phoneNumber) && !ContainsPhoneContact(phoneNumber))
            {
                contacts.Add(phoneNumber, name);
                Console.WriteLine($"Le contact {name} avec le numéro {phoneNumber} a été ajouté.");
            }
            else
            {
                Console.WriteLine("Numéro invalide ou déjà existant dans le carnet.");
            }
        }
        public bool DeletePhoneNumber(string phoneNumber)
        {
            if (ContainsPhoneContact(phoneNumber))
            {
                contacts.Remove(phoneNumber);
                Console.WriteLine($"Le contact avec le numéro {phoneNumber} a été supprimé.");
                return true;
            }
            else
            {
                Console.WriteLine($"Le contact avec le numéro {phoneNumber} n'existe pas dans le carnet.");
                return false;
            }
        }
            public void PhoneContact(string phoneNumber)
        {
            if (IsValidPhoneNumber(phoneNumber))
            {
                string contactName = GetPhoneContact(phoneNumber);

                if (contactName != null)
                {
                    Console.WriteLine($"Le nom associé au numéro {phoneNumber} est : {contactName}");
                }
                else
                {
                    Console.WriteLine($"Aucun contact trouvé avec le numéro {phoneNumber}.");
                }
            }
            else
            {
                Console.WriteLine($"Numéro de téléphone invalide : {phoneNumber}");
            }
        }

        public void DisplayPhoneBook()
        {
            Console.WriteLine("Carnet téléphonique :");
            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.Key}: {contact.Value}");
            }
        }

        public string GetPhoneContact(string phoneNumber)
        {
            if (contacts.TryGetValue(phoneNumber, out string contactName))
            {
                return contactName;
            }
            else
            {
                return null;
            }
        }
    }
}
