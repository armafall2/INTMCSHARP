using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_III
{
    public static class ClassCouncil
    {
        public static void GenerateCsv(int nb)
        {
            string path = Directory.GetCurrentDirectory();
            string output = path + @"\class.csv";

            string[] matieres = {"Mathématiques", "Français", "Physique", "Chimie", "Histoire", "Anglais", "Biologie", "Géographie", "Informatique", "Économie", "Arts plastiques", "Éducation physique", "Musique", "Technologie", "Philosophie"};
            string[] noms = { "John Doe", "Jane Smith", "Bob Johnson", "Alice Williams", "David Brown", "Emily Davis", "Michael Wilson", "Olivia Lee", "Daniel Miller", "Sophia Taylor", "Christopher Moore", "Isabella Garcia", "Matthew Anderson", "Ava Martinez", "William Jones" };

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
