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

        /// <summary>
        /// Metoda koja dodjeljuje nagradni poklon mušteriji.
        /// Ukoliko je mušterija napravila broj kupovina koji predstavlja stepen broja 10
        /// (minimalno 100, a zatim 1,000, 10,000, 100,000 i sl.)
        /// dodaje mu se nagradni poklon koji je poslan kao parametar,
        /// ali samo pod uslovom da poklon ispunjava kriterij da njegov postotak popusta
        /// odgovara broju kupovina (za 100 kupovina maksimalni popust je 10%, za 1,000
        /// kupovina maksimalni popust je 20%, za 10,000 kupovina je 30% i sl.)
        /// Ukoliko mušterija nije napravila tačan broj kupovina koji se zahtijeva
        /// ili je proslijeđen poklon koji ne ispunjava parametar, potrebno je baciti izuzetak.
        /// </summary>
        /// <param name="nagrada"></param>
        /// <returns></returns>
        public bool NagradnaKupovina(Poklon nagrada)
        {
            // Ovu metodu je implementirao Muhamed Borovac
            double vrijednost = Math.Log10(UkupanBrojKupovina);

            if (vrijednost == (int) vrijednost)
            {
                // Math.Log10 će vratiti 1 za 10 kupovina, 2 za 100, 3 za 1000 itd. što koristimo za računanje postotka popusta
                if (nagrada.PostotakPopusta == (vrijednost - 1) / (double)10)
                {
                    KupljeniPokloni.Add(nagrada);
                    return true;
                }
                else
                {
                    throw new ArgumentException("Proslijeđeni poklon ne ispunjava parametar");
                }
            }
            
            throw new ArgumentException("Mušterija nije napravila tačan broj kupovina koji se zahtijeva.");
        }

        #endregion
    }
}
