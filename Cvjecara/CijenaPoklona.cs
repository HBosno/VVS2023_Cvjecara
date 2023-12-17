using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Simulirat cemo implementaciju interfejsa ICijena. Za proslijeđeni poklon se određuje cijena uzimajući u obzir postotak popusta i troškove materijala.
namespace Cvjecara
{
    public interface ICijenaPoklona
    {
        double OdrediCijenuPoklona(Poklon poklon);
    }

    public class CijenaPoklona : ICijenaPoklona
    {
        public double OdrediCijenuPoklona(Poklon poklon)
        {
            double cijena = (1 - poklon.PostotakPopusta) * TroskoviMaterijala(poklon);
            return cijena;
        }

        private double TroskoviMaterijala(Poklon poklon)
        {
            double osnovnaCijena = 20;
            double dodatniTroskovi = 20;
            return osnovnaCijena + dodatniTroskovi;
        }

    }
}