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
            //TODO
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
