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

            
            int   score = 0;
            
           foreach(Qcm qcm in qcms)
           {

                if (QcmValidity(qcm)) { 

                    score += AskQuestion(qcm);

                    }
                else 
                {
                    Console.WriteLine("Question invalide");
                }

           }
            
           Console.WriteLine("Votre Score est : " + score);
        }

        public static int AskQuestion(Qcm qcm)
        {
            int       i = 1;
            int reponse = 0;
            int     len = 0;

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

            if(reponse == qcm.Solution)
            {
                return qcm.Weight;
            }
            else if(reponse != qcm.Solution)
            {
                return (qcm.Weight) * (- 1);
            }
        
            return -1;
        }

        public static bool QcmValidity(Qcm qcm)
        {
            if(qcm.Weight <= 0)
            {
                return false;
            }
            else if((qcm.Solution < 0 || qcm.Solution >= qcm.Answers.Length))
            {
                return false;
            }

            return true;
        }
    }
}
