using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvjecara
{
    public class Mušterija
    {
        #region Atributi

        string identifikacijskiBroj, imeIPrezime;
        int ukupanBrojKupovina;
        List<Poklon> kupljeniPokloni;
        List<Buket> kupljeniBuketi;

        #endregion

        #region Properties

        public string IdentifikacijskiBroj { get => identifikacijskiBroj; }
        public string ImeIPrezime
        {
            get => imeIPrezime;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new NotSupportedException("Ime i prezime mušterije se mora navesti!");
                imeIPrezime = value;
            }
        }
        public int UkupanBrojKupovina { get => ukupanBrojKupovina; }
        public List<Buket> KupljeniBuketi { get => kupljeniBuketi; }
        public List<Poklon> KupljeniPokloni { get => kupljeniPokloni; }

        #endregion

        #region Konstruktor

        public Mušterija(string ime)
        {
            string sifra = "";
            Random r = new Random();
            for (int i = 0; i < 10; i++)
                sifra += r.Next(0, 9).ToString();
            identifikacijskiBroj = sifra;
            ImeIPrezime = ime;
            ukupanBrojKupovina = 0;
            kupljeniBuketi = new List<Buket>();
            kupljeniPokloni = new List<Poklon>();
        }

        #endregion

        #region Metode

        public void RegistrujKupovinu(Buket b, Poklon p)
        {
            if (b == null || p == null)
                throw new NotSupportedException("Buket i poklon se moraju navesti!");

            ukupanBrojKupovina++;
            kupljeniBuketi.Add(b);
            kupljeniPokloni.Add(p);
        }

        /* Simulacija CodeStream komentara:
         * Zbog kompleksnosti metode poželjno je dodati objašnjenje rada metode
        */
        public bool NagradnaKupovina(Poklon nagrada)
        {
            double vrijednost = Math.Log10(UkupanBrojKupovina);

            if (vrijednost == (int)vrijednost)
            {
                // Math.Log10 će vratiti 1 za 10 kupovina, 2 za 100, 3 za 1000 itd. što koristimo za računanje postotka popusta
                if (nagrada.PostotakPopusta == (vrijednost - 1) / (double)10)
                {
                    KupljeniPokloni.Add(nagrada);
                    return true;
                }
                /* Simulacija CodeStream komentara:
                 * Bespotreban else uslov
                */
                else
                {
                    throw new ArgumentException("Proslijeđeni poklon ne ispunjava parametar");
                }
            }

            throw new ArgumentException("Mušterija nije napravila tačan broj kupovina koji se zahtijeva.");
        }

        /* Simulacija CodeStream komentara:
         * Zbog kompleksnosti metode poželjno je dodati objašnjenje rada metode
         * Potrebno uraditi validaciju metoda
        */
        public Vrsta NajčešćiCvijet()
        {
            Dictionary<Vrsta, int> brojPojavljivanja = new Dictionary<Vrsta, int>();

            foreach (var buket in kupljeniBuketi)
            {
                foreach (var cvijet in buket.Cvijeće)
                {
                    Vrsta vrstaCvijeta = cvijet.Vrsta;
                    if (brojPojavljivanja.ContainsKey(vrstaCvijeta))
                    {
                        brojPojavljivanja[vrstaCvijeta]++;
                    }
                    /* Simulacija CodeStream komentara:
                     * Kroz svaku petlju se varijabla brojPonavljanja[vrstaCvijeta] postavlja na 1,
                     * potrebno staviti pod else uvjetom
                    */
                    brojPojavljivanja[vrstaCvijeta] = 1;

                }
            }

            var najcesciCvijet = brojPojavljivanja.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            return najcesciCvijet;
        }
        #endregion
    }
}
