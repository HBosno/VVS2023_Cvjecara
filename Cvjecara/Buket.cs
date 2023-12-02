using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvjecara
{
    public class Buket
    {
        #region Atributi

        List<Cvijet> cvijeće;
        List<string> dodaci;
        double cijena;
        Poklon poklon;

        #endregion

        #region Properties

        public List<Cvijet> Cvijeće { get => cvijeće; set => cvijeće = value; }
        public List<string> Dodaci 
        { 
            get => dodaci; 
            set
            {
                foreach (var dodatak in value)
                    if (dodatak != "Lišće" && dodatak != "Slama" && dodatak != "Trava")
                        throw new NotSupportedException("Dodaci koje ste unijeli nisu podržani!");
                dodaci = value;
            }
        }
        public double Cijena { get => cijena; }
        public Poklon Poklon { get => poklon; }

        #endregion

        #region Konstruktor

        public Buket(double c)
        {
            cvijeće = new List<Cvijet>();
            dodaci = new List<string>();
            if (c < 0.01)
                throw new NotSupportedException("Cijena ne može biti manja od 0.01 KM!");
            cijena = c;
        }

        #endregion

        #region Metode

        public void DodajCvijet(Cvijet c)
        {
            cvijeće.Add(c);
        }

        public void DodajDodatak(string d)
        {
            dodaci.Add(d);
            Dodaci = dodaci;
        }

        public void DodajPoklon(Poklon p)
        {
            if (Poklon == null)
                poklon = p;
        }

        #endregion
    }
}
