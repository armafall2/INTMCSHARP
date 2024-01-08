using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_IV
{
    public class Morse
    {
        private const string Taah = "===";
        private const string Ti = "=";
        private const string Point = ".";
        private const string PointLetter = "...";
        private const string PointWord = ".....";

        private readonly Dictionary<string, char> _alphabet;

        public Morse()
        {
            _alphabet = new Dictionary<string, char>()
            {
                {$"{Ti}.{Taah}", 'A'},
                {$"{Taah}.{Ti}.{Ti}.{Ti}", 'B'},
                {$"{Taah}.{Ti}.{Taah}.{Ti}", 'C'},
                {$"{Taah}.{Ti}.{Ti}", 'D'},
                {$"{Ti}", 'E'},
                {$"{Ti}.{Ti}.{Taah}.{Ti}", 'F'},
                {$"{Taah}.{Taah}.{Ti}", 'G'},
                {$"{Ti}.{Ti}.{Ti}.{Ti}", 'H'},
                {$"{Ti}.{Ti}", 'I'},
                {$"{Ti}.{Taah}.{Taah}.{Taah}", 'J'},
                {$"{Taah}.{Ti}.{Taah}", 'K'},
                {$"{Ti}.{Taah}.{Ti}.{Ti}", 'L'},
                {$"{Taah}.{Taah}", 'M'},
                {$"{Taah}.{Ti}", 'N'},
                {$"{Taah}.{Taah}.{Taah}", 'O'},
                {$"{Ti}.{Taah}.{Taah}.{Ti}", 'P'},
                {$"{Taah}.{Taah}.{Ti}.{Taah}", 'Q'},
                {$"{Ti}.{Taah}.{Ti}", 'R'},
                {$"{Ti}.{Ti}.{Ti}", 'S'},
                {$"{Taah}", 'T'},
                {$"{Ti}.{Ti}.{Taah}", 'U'},
                {$"{Ti}.{Ti}.{Ti}.{Taah}", 'V'},
                {$"{Ti}.{Taah}.{Taah}", 'W'},
                {$"{Taah}.{Ti}.{Ti}.{Taah}", 'X'},
                {$"{Taah}.{Ti}.{Taah}.{Taah}", 'Y'},
                {$"{Taah}.{Taah}.{Ti}.{Ti}", 'Z'},
            };
        }

        public int LettersCount(string code)
        {
            int countLettres = 0;
            int index = 0;

            while (index < code.Length)
            {
                int indexOfPointLetter = code.IndexOf(PointLetter, index);

                if (indexOfPointLetter != -1)
                {
                    countLettres++;
                    index = indexOfPointLetter + PointLetter.Length;
                }
                else
                {
                    index += PointLetter.Length;
                }
            }
            if (!code.EndsWith(PointLetter))
            {
                countLettres++;
            }

            return countLettres;
        }
        public int WordsCount(string code)
        {
            int countLettres = 0;
            int index = 0;

            while (index < code.Length)
            {
                int indexOfPointLetter = code.IndexOf(PointWord, index);

                if (indexOfPointLetter != -1)
                {
                    countLettres++;
                    index = indexOfPointLetter + PointWord.Length;
                }
                else
                {
                    index += PointWord.Length;
                }
            }
            if (!code.EndsWith(PointWord))
            {
                countLettres++;
            }

            return countLettres;
        }
        public string MorseTranslation(string code)
        {
            string[] motsMorse = code.Split(new string[] { PointWord }, StringSplitOptions.None);

            List<string> motsTraduits = new List<string>();
            foreach (string motMorse in motsMorse)
            {
                motsTraduits.Add(TraduireMot(motMorse, PointLetter));
            }
            string phraseTraduite = string.Join(" ", motsTraduits);

            return phraseTraduite;
        }
        public string EfficientMorseTranslation(string code)
        {
            StringBuilder translation = new StringBuilder();

            string[] words = code.Split(new string[] { PointWord }, StringSplitOptions.None);

            foreach (string word in words)
            {
                string[] letters = word.Split(new string[] { PointLetter }, StringSplitOptions.None);

                foreach (string letter in letters)
                {
                    if (!string.IsNullOrEmpty(letter))
                    {
                        if (letter.StartsWith(Point) || letter.EndsWith(Point))
                        {
                            string trimmedLetter = letter.Trim(Point.ToCharArray());
                            translation.Append(TraduireLettre(trimmedLetter));
                        }
                        else
                        {
                            translation.Append(TraduireLettre(letter));
                        }
                    }
                }

                translation.Append(' ');
            }

            return translation.ToString().Trim();
        }
        public string MorseEncryption(string sentence)
        {
            StringBuilder morseBuilder = new StringBuilder();

            bool isPreviousCharSpace = false;
            bool isFirstCharacter = true;
            sentence = sentence.ToUpper();
            foreach (char character in sentence.ToUpper())
            {
                if (character == ' ')
                {
                    if (!isPreviousCharSpace)
                    {
                        isPreviousCharSpace = true;
                    }
                }
                else
                {
                    if (isPreviousCharSpace || isFirstCharacter)
                    {
                        if (!isFirstCharacter)
                        {
                            morseBuilder.Append(PointWord);
                        }
                        isPreviousCharSpace = false;
                        isFirstCharacter = false;
                    }
                    else
                    {
                        morseBuilder.Append(PointLetter);
                    }

                    string morseCode = GetMorseCode(character);
                    morseBuilder.Append(morseCode);
                }
            }



            return morseBuilder.ToString();
        }
        private string GetMorseCode(char character)
        {
            foreach (var entry in _alphabet)
            {
                if (entry.Value == character)
                {
                    return entry.Key;
                }
            }
            return string.Empty;
        }
        private string TraduireMot(string motMorse, string pointLetter)
        {
            string[] lettresMorse = motMorse.Split(new string[] { pointLetter }, StringSplitOptions.None);

            List<char> lettresTraduites = new List<char>();
            foreach (string lettreMorse in lettresMorse)
            {
                lettresTraduites.Add(TraduireLettre(lettreMorse));
            }
            string motTraduit = string.Concat(lettresTraduites);

            return motTraduit;
        }
        private char TraduireLettre(string lettreMorse)
        {
            return _alphabet.TryGetValue(lettreMorse, out char caractere) ? caractere : '+';
        }
}
}