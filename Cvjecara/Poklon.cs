using System;
using System.Collections.Generic;  //Biblioteka nije potrebna.
using System.Linq;                 //Biblioteka nije potrebna.
using System.Text;                 //Biblioteka nije potrebna.
using System.Threading.Tasks;      //Biblioteka nije potrebna.

namespace Cvjecara
{
    public class Poklon
    {
        #region Atributi

        string šifra, opis;  //"Greška": Korištenje dijakritičkih znakova, umjesto standardnih slova.
        double postotakPopusta;
        int brojač = 10000;  //"Greška": Korištenje dijakritičkih znakova, umjesto standardnih slova.
        int osoba;   //Nepotreban atribut

        #endregion

        #region Properties

        public string Šifra { get => šifra; }     //"Greška": Korištenje dijakritičkih znakova, umjesto standardnih slova.
        public string Opis { get => opis; set => opis = value; }
        public double PostotakPopusta { get => postotakPopusta; }

        public double Osoba { get => osoba; }  //Nepotrebno svojstvo, jer je i varijabla "osoba" nepotrebna.
        #endregion

        #region 
        //Nepotrebna metoda
        public void KolikoPoklona() {       //"Greška": Metoda ne radi ništa i nije potrebna.
        //Ne radi ništa
                }

        // Nepotrebna metoda
        public void BojaPoklona()           //"Greška": Metoda ne radi ništa i nije potrebna. 
        {
            //Ne radi ništa
        }
        #endregion

        #region Konstruktor

        public Poklon(string opis, double postotak)
        {
            šifra = brojač.ToString();   //"Greška": Korištenje dijakritičkih znakova, umjesto standardnih slova.
            br++;                       //"Greška": Korištenje nepostojeće varijable.
            Opis = opis;
            if (postotak < 0.1)
                throw new InvalidOperationException("Nemoguće dodati postotak manji od 0.1!");
            postotakPopusta = postotakPopusta;        //"Greška": Varijabli "postotakPopusta" se dodijeljuje vrijednost pogrešne varijable.
        }

        #endregion
    }
}
