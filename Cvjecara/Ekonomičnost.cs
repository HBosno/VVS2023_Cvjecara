using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvjecara
{
    /*
     * Ovdje simuliramo nedovršenu implementaciju interfejsa IEkonomičnost. Ideja da se za proslijeđeni buket provjeri broj transakcija
       vezanih za isti, te na osnovu određenog kriterija odluči da li je buket isplativ ili ne.
    */
    public interface IEkonomičnost
    {
        public bool ProvjeriIsplativost(Buket b);
    }

    public class Ekonomičnost : IEkonomičnost
    {
        public bool ProvjeriIsplativost(Buket b)
        {
            return true;
        }
    }
}