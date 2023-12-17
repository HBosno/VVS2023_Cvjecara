using System;
using System.Collections.Generic;
using System.Linq;
//Izbrisane nepotrebne biblioteke

namespace Cvjecara
{
    public class Buket
    {
       
        //Izbrisan nepotreban atribut, kao i nepotrebna metoda

        #region Atributi

        List<Cvijet> cvijeće;
        List<string> dodaci;
        //Promjenjen naziv atributa "a" u "cijena"
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
                {
                    if (dodatak != "Lišće" && dodatak != "Slama" && dodatak != "Trava")
                        throw new NotSupportedException("Dodaci koje ste unijeli nisu podržani!");
                    // Izbacen nepotreban kod unutar petlje
                }

                dodaci = value;
            }
        }
        //Promjenjen naziv atributa "a" u "cijena"
        public double Cijena
        {
            get => cijena;
            set
            {
                if (value < 0.01)
                    throw new NotSupportedException("Cijena ne može biti manja od 0.01  KM!");
                cijena = value;
            }
        }

        public Poklon Poklon { get => poklon; }

        #endregion

        #region Konstruktor

        public Buket(double c)
        {
            cvijeće = new List<Cvijet>();
            dodaci = new List<string>();
            if (c < 0.01)
                throw new NotSupportedException("Cijena ne može biti manja od 0.01 KM!");
            //Promjenjen naziv atributa "a" u "cijena"
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
        //Izbrisana nepostojeća i nepotrebna metoda

        public void PopustZaVelikiBuket()
        {
            int ukupnoCvijeca = Cvijeće.Sum(cvijet => cvijet.Kolicina);
            if (ukupnoCvijeca > 10)
            {
                double postotakPopusta = 10;
                double faktor = 1 - (postotakPopusta / 100);
                Cijena = Cijena * faktor;
            }
        }
        #endregion
    }
}

