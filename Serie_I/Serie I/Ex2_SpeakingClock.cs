using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    public static class SpeakingClock
    {
        public static string GoodDay(int heure)
        {

            if (heure >= 0 && heure <= 5)
            {
                Console.WriteLine($"il est {heure}H, Merveilleuse nuit !");
            }
            else if (heure >= 6 && heure <= 11)
            {
                Console.WriteLine($"il est {heure}H, Bonne Matinée !");
            }
            else if (heure == 12)
            {
                Console.WriteLine($"il est {heure}H, Bon App !");
            }
            else if (heure >= 13 && heure <= 18)
            {
                Console.WriteLine($"il est {heure}H, Profitez de votre aprem !");
            }
            else if (heure > 18)
            {
                Console.WriteLine($"il est {heure}H, Bonne soirée !");
            }



            return string.Empty;
        }
    }
}
