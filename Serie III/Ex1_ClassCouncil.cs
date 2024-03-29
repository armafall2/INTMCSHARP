﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_III
{
    public static class ClassCouncil
    {

        /// <summary>
        /// Genère un CSV avec des noms "alétoire" (provenant d'un tableau)
        /// Des noms de matière avec le même principe
        /// et une note aleatoire entre 0 et 20
        /// | nb est le nombre de ligne du fichier a générer
        /// </summary>
        /// <param name="nb"></param>
        public static void GenerateCsv(int nb)
        {
            string path = Directory.GetCurrentDirectory();
            string output = path + @"\class.csv";

            string[] matieres = {"Combat Sabre Laser", "Tir Blaster", "Vol", "Chimie", "Tank", "Stratégie", "Destruction de forêt", "Tir Deagle", "Vol de Pain", "Tir arbalete", "Double Saut", "Force", "Addition to his collection", "Lancer de grenade", "Science"};
            string[] noms = { "Master Chief", "Dr Mundo", "Glory Draven", "Albrecht Entrati", "Captain Vor", "Tesla", "Ryan Reinolds", "Jean Valjean", "Cosette", "Daryl Dixon", "Sly Cooper", "Ratchet", "Clank", "Darth Vador", "General Grievous", "Compte Dooku" };

            List<string> res = new List<string>();

            Random matRand = new Random();
            Random nomRand = new Random();
            Random noteRand = new Random();
            

            int newRandNom = 0;
            int newRandMat = 0;
            int newRandNot = 0;

            for (int i = 0; i < nb; i++)
            {
                newRandMat = matRand.Next(0, matieres.Length);
                newRandNom = matRand.Next(0, noms.Length);
                newRandNot = noteRand.Next(0, 21);


                res.Add($"{noms[newRandNom]};{matieres[newRandMat]};{newRandNot}");
            }

            File.WriteAllLines(output, res);

        }

        public static void SchoolMeans(string input, string output)
        {
            try
            {
                string[] lines = File.ReadAllLines(input);
                List<string> resultat = new List<string>();
                List<(string Matiere, int Note)> data = new List<(string Matiere, int Note)>();
                Dictionary<string, (int sum, int count)> stockage = new Dictionary<string, (int, int)>();

                foreach (var line in lines)
                {
                    var fields = line.Split(';');
                    string matiere = fields[1];
                    int note = int.Parse(fields[2]);
                    data.Add((matiere, note));
                }

                foreach (var entry in data)
                {
                    string matiere = entry.Matiere;
                    int note = entry.Note;

                    if (!stockage.ContainsKey(matiere))
                    {
                        stockage[matiere] = (note, 1);
                    }
                    else
                    {
                        var (sum, count) = stockage[matiere];
                        stockage[matiere] = (sum + note, count + 1);
                    }
                }

                Console.WriteLine("Moyenne par matière:");
                foreach (var element in stockage)
                {
                    string matiere = element.Key;
                    var (sum, count) = element.Value;
                    double moyenne = (double)sum / count;
                    Console.WriteLine($"{matiere}: {moyenne:F2}");

                    resultat.Add($"{matiere};{moyenne}");


                }
                File.WriteAllLines(output, resultat);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
            }

        }
    }
}
