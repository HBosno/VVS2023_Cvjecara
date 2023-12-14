using System;   //Ostala je samo potrebna biblioteka, ostale su obrisane.

namespace Cvjecara
{
    public class Poklon
    {
        #region Atributi

        string sifra, opis;  //"Ispravljena greška": Ime varijable je promijenjeno iz "brojač" u "brojac".
        double postotakPopusta;
        int brojac = 10000;  //"Ispravljena greška": Ime varijable je promijenjeno iz "brojač" u "brojac".


        #endregion

        #region Properties

        public string Šifra { get => sifra; }     //"Ispravljena greška": Ime varijable je promijenjeno iz "brojač" u "brojac".
        public string Opis { get => opis; set => opis = value; }
        public double PostotakPopusta { get => postotakPopusta; }

        #endregion

        #region Konstruktor

        public Poklon(string opis, double postotak)
        {
            sifra = brojac.ToString();   //"Ispravljena greška": Ime varijable je promijenjeno iz "brojač" u "brojac".
            brojac++;                       //"Ispravljena greška": Koristi se deklarisana varijabla "brojac".
            Opis = opis;
            if (postotak < 0.1)
                throw new InvalidOperationException("Nemoguće dodati postotak manji od 0.1!");
            postotakPopusta = postotak;        //"Ispravljena greška": Varijabli "postotakPopusta" se dodijeljuje vrijednost "postotak".
        }

        #endregion
    }
}
