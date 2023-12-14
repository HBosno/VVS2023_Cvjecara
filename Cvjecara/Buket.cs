using System;
using System.Collections.Generic;
//Nepotrebno
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cvjecara
{
    public class Buket
    {
        // Nepotreban atribut
        string nepotrebanAtribut; // "Greška": Atribut se inicijalizuje, ali se ne koristi.

        // Nepotrebna metoda
        void NepotrebnaMetoda() // "Greška": Metoda ne radi ništa i ne spominje se u kodu.
        {
            // Ništa se ne radi
        }

        #region Atributi

        List<Cvijet> cvijeće;
        List<string> dodaci;
        double a; // "Greška": Promenjen naziv atributa "cijena" u "a".
        Poklon poklon;

        #endregion

        #region Properties

        public List<Cvijet> Cvijeće { get => cvijeće; set => cvijeće = value; }
        public List<string> Dodaci
        {
            get => dodaci;
            set
            {
                // Nepotreban kod unutar foreach petlje
                foreach (var dodatak in value)
                {
                    if (dodatak != "Lišće" && dodatak != "Slama" && dodatak != "Trava")
                        throw new NotSupportedException("Dodaci koje ste unijeli nisu podržani!");

                    // Nepotreban kod
                    Console.WriteLine("Nepotreban kod unutar foreach petlje");
                }

                dodaci = value;
            }
        }
        public double Cijena { get => a; } // "Greška": Promenjen naziv atributa "cijena" u "a".
        public Poklon Poklon { get => poklon; }

        #endregion

        #region Konstruktor

        public Buket(double c)
        {
            cvijeće = new List<Cvijet>();
            dodaci = new List<string>();
            if (c < 0.01)
                throw new NotSupportedException("Cijena ne može biti manja od 0.01 KM!");
            a = c; // "Greška": Promenjen naziv atributa "cijena" u "a".
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

        // Nepostojeća metoda
        NepostojećaMetoda(); // "Greška": Poziv nepostojeće metode.

        // Poziv nepotrebne metode
        NepotrebnaMetoda(); // "Greška": Poziv metode koja ništa ne radi i ne spominje se u kodu.

        #endregion
    }
}

