using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_II
{
    public struct Qcm
    {
        public string   Question { get; set; }
        public string[] Answers  { get; set; }
        public int      Solution { get; set; }
        public int        Weight { get; set; }
    }

    public static class Quiz
    {
        public static void AskQuestions(Qcm[] qcms)
        {

            int       i = 1;
            int reponse = 0;
            int     len = 0;
            int   score = 0;

           foreach(Qcm qcm in qcms)
           {
                Console.WriteLine(qcm.Question);
                i = 1;
                foreach(string element in qcm.Answers)
                {
                    Console.Write(" " + i + " : " + element + " ");
                    i++;
                }
                
                Console.WriteLine(" ");
                len = qcm.Answers.Length;

                do
                {
                Console.Write("Votre Réponse : ");
                  reponse = Convert.ToInt32(Console.ReadLine());
                } while (reponse < 1 || reponse > len);

                if(qcm.Solution == reponse)
                {
                    score += qcm.Weight;
                }
                else if(qcm.Solution != reponse)
                {
                    score -= qcm.Weight;
                }

           }
            
           Console.WriteLine("Votre Score est : " + score);
        }

        public static int AskQuestion(Qcm qcm)
        {
            //TODO
            return -1;
        }

        public static bool QcmValidity(Qcm qcm)
        {
            //TODO
            return false;
        }
    }
}
