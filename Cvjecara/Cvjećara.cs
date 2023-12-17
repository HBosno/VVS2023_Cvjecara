using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvjecara
{
    public class Cvjećara
    {
        #region Atributi

        List<Cvijet> cvijeće;
        List<Buket> buketi;
        List<Mušterija> mušterije;
        List<Poklon> naručeniPokloni;

        #endregion

        #region Properties

        public List<Cvijet> Cvijeće { get => cvijeće; }
        public List<Poklon> NaručeniPokloni { get => naručeniPokloni; set => naručeniPokloni = value; }
        public List<Mušterija> Mušterije { get => mušterije; set => mušterije = value; }

        #endregion

        #region Konstruktor

        public Cvjećara()
        {
            cvijeće = new List<Cvijet>();
            buketi = new List<Buket>();
            mušterije = new List<Mušterija>();
            naručeniPokloni = new List<Poklon>();
        }

        #endregion

        #region Metode
        /// <summary>
        /// Obavlja određene radnje s cvijetom, ovisno o zadanoj opciji.
        /// </summary>
        /// <param name="c">Cvijet s kojim se vrše određene radnje.</param>
        /// <param name="opcija">
        /// Opcija koja određuje vrstu radnje:
        ///  - 0: Dodavanje cvijeta.
        ///  - 1: Izmjena postojećeg cvijeta.
        ///  - 2: Brisanje postojećeg cvijeta.
        /// </param>
        public void RadSaCvijećem(Cvijet c, int opcija)
        {
            if (opcija == 0)
            {
                RadSaCvijećemValidacija(c, opcija);
                cvijeće.Add(c);
            }
            else if (opcija == 1)
            {
                RadSaCvijećemValidacija(c, opcija);
                cvijeće.Remove(cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme));
                cvijeće.Add(c);
            }
            else if (opcija == 2)
            {
                RadSaCvijećemValidacija(c, opcija);
                cvijeće.Remove(cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme));
            }
            else
                throw new InvalidOperationException("Unijeli ste nepoznatu opciju!");
        }

        public void RadSaCvijećemValidacija(Cvijet c, int opcija)
        {
            if (c == null)
            {
                switch (opcija)
                {
                    case 0: throw new NullReferenceException("Nemoguće dodati cvijet koji ne postoji!");
                    case 1: throw new NullReferenceException("Nemoguće izmijeniti cvijet koji ne postoji!");
                    case 2: throw new NullReferenceException("Nemoguće obrisati cvijet koji ne postoji!");
                }
            }
            if (opcija == 0 && cvijeće.Contains(c))
            {
                throw new InvalidOperationException("Nemoguće dodati cvijet koji već postoji!");
            }
            if (cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme) == null && (opcija == 1 || opcija == 2))
            {
                switch (opcija)
                {
                    case 1: throw new InvalidOperationException("Nemoguće izmijeniti cvijet koji ne postoji!");
                    case 2: throw new InvalidOperationException("Nemoguće obrisati cvijet koji ne postoji!");
                }
            }
        }

        public void DodajBuket(List<Cvijet> cvijeće, List<string> dodaci, Poklon poklon, double cijena)
        {
            Buket b = new Buket(cijena);
            b.DodajPoklon(poklon);
            foreach (Cvijet c in cvijeće)
                b.DodajCvijet(c);
            foreach (string dodatak in dodaci)
                b.DodajDodatak(dodatak);
            buketi.Add(b);
        }

        public bool ObrišiBuket(Buket b)
        {
            return buketi.Remove(b);
        }

        public List<Buket> DajSveBukete()
        {
            return buketi;
        }

        /// <summary>
        /// Izvršava nabavku cvijeća ovisno o godišnjem dobu i veličini narudžbe.
        /// </summary>
        /// <param name="godišnjeDoba">Godišnje doba u kojem se vrši nabavka (Proljeće, Jesen).</param>
        /// <param name="veličinaNarudžbe">Veličina narudžbe (Mala, Velika).</param>
        public void IzvršiNabavku(string godišnjeDoba, string veličinaNarudžbe)
        {
            if (godišnjeDoba.Equals("Ljeto") || godišnjeDoba.Equals("Zima"))
                throw new ArgumentException("Nabavka nije dozvoljena ljeti ili zimi.");
            if (veličinaNarudžbe.Equals("Srednja"))
                throw new ArgumentException("Nije dozvoljena nabavka srednje velicine.");
            if (!(godišnjeDoba.Equals("Proljeće") || godišnjeDoba.Equals("Jesen")))
                throw new ArgumentException("Nabavka je dozvoljena samo u proljeće ili jesen.");
            if (!(veličinaNarudžbe.Equals("Mala") || veličinaNarudžbe.Equals("Velika")))
                throw new ArgumentException("Nesmislen parametar za veličinu narudžbe.");
            if (veličinaNarudžbe.Equals("Mala"))
            {
                Cvijet neven = new Cvijet(Vrsta.Neven, "Calendula officinalis", "Žuta", DateTime.Now.AddDays(-1), 10);
                RadSaCvijećem(neven, 0);
                Cvijet margareta = new Cvijet(Vrsta.Margareta, "Leucanthemum vulgare", "Žuta", DateTime.Now.AddDays(-1), 10);
                RadSaCvijećem(margareta, 0);
                Cvijet ljiljan = new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now.AddDays(-1), 10);
                RadSaCvijećem(ljiljan, 0);
            }
            if (veličinaNarudžbe.Equals("Velika"))
            {
                Cvijet orhideja = new Cvijet(Vrsta.Orhideja, "Orchidaceae", "Roza", DateTime.Now.AddDays(-1), 100);
                RadSaCvijećem(orhideja, 0);
                Cvijet ruza = new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Narandžasta", DateTime.Now.AddDays(-1), 100);
                RadSaCvijećem(ruza, 0);
            }
        }

        /// <summary>
        /// Pregledava sve cvjetove u kolekciji i provodi određene akcije za svaki cvijet.
        /// </summary>
        /// <remarks>
        /// Za svaki cvijet u kolekciji:
        /// - Poziva metodu <see cref="Cvijet.ProvjeriKrajSezone"/> kako bi se provjerilo je li došao kraj sezone za taj cvijet.
        /// - Provjerava svježinu cvijeća pomoću metode <see cref="Cvijet.OdrediSvježinuCvijeća"/> i postavlja količinu na 0 ako je svježina manja od 2.
        /// </remarks>
        public void PregledajCvijeće()
        {
            foreach (Cvijet cvijet in cvijeće)
            {
                cvijet.ProvjeriKrajSezone();
                if (cvijet.OdrediSvježinuCvijeća() < 2)
                    cvijet.Kolicina = 0;
            }

            cvijeće.RemoveAll(cvijet => cvijet.Kolicina == 0);
        }

        public void NaručiCvijeće(Mušterija m, Buket b, Poklon p, Poklon nagrada = null)
        {
            if (!buketi.Contains(b))
                throw new InvalidOperationException("Traženi buket nije na stanju!");

            m.RegistrujKupovinu(b, p);
            naručeniPokloni.Add(p);

            if (nagrada != null)
                m.NagradnaKupovina(p);
        }

        public void ProvjeriLatinskaImenaCvijeća(ILeksikon leksikon)
        {
            List<Cvijet> zaObrisati = new List<Cvijet>();
            foreach (Cvijet c in cvijeće)
            {
                if (!leksikon.ValidnoLatinskoIme(c.LatinskoIme))
                    zaObrisati.Add(c);
            }
            cvijeće.RemoveAll(cvijet => zaObrisati.Contains(cvijet));
        }

        // Dodana metoda u kojoj koristimo nedovršeni interface. Potrebna za mock test.
        public void ProvjeriEkonomičnostBuketa(IEkonomičnost ekonomicnost)
        {
            List<Buket> zaObrisati = new List<Buket>();
            foreach (Buket b in DajSveBukete())
            {
                if (!ekonomicnost.ProvjeriIsplativost(b))
                    zaObrisati.Add(b);
            }
            DajSveBukete().RemoveAll(buket => zaObrisati.Contains(buket));
        }

        public List<Poklon> DajSveNaručenePoklone(Mušterija m, double popust)
        {
            List<Poklon> pokloni = m.KupljeniPokloni.FindAll(poklon => poklon.PostotakPopusta == popust);
            if (pokloni == null || pokloni.Count == 0)
                throw new FormatException("Došlo je do greške! Pokušajte ponovo sa drugim parametrima zahtjeva.");

            return pokloni;
        }

        /// <summary>
        /// Metoda koja vraća mušteriju koja je izvršila najveći broj kupovina.
        /// Ukoliko cvjećara nema nijednu mušteriju, potrebno je baciti izuzetak.
        /// U suprotnom, potrebno je pronaći mušteriju koja je ukupno kupila najviše cvijeća
        /// u svim buketima koje je naručila.
        /// Ukoliko postoji više takvih mušterija, potrebno je vratiti onu mušteriju
        /// koja je potrošila veći ukupni iznos novca na sve kupljene bukete.
        /// Ukoliko i u tom slučaju postoji više mušterija, potrebno je baciti izuzetak
        /// jer se najbolja mušterija u tom slučaju ne može tačno odrediti.
        /// </summary>
        /// <returns></returns>
        /// 
        public Mušterija DajNajboljuMušteriju()
        {
            //Ovu metodu je implementirao Dzenan Nuhic
            if (mušterije.Count == 0)
            {
                throw new ArgumentException("Cvjecara nema nijednu musteriju");
            }
            List<int> brojCvijecaPoMusteriji = mušterije.Select(musterija =>
            {
                int brojCvijeca = 0;
                musterija.KupljeniBuketi.ForEach(buket => brojCvijeca += buket.Cvijeće.Count);
                return brojCvijeca;
            }).ToList();
            int maxBrojCvijeca = brojCvijecaPoMusteriji.Max();
            List<Mušterija> najboljeMusterije = mušterije
                .Where((musterija, index) => brojCvijecaPoMusteriji[index] == maxBrojCvijeca).ToList();

            //ako ima vise najboljih gleda se broj para koji su najbolji potrosili
            if (najboljeMusterije.Count > 1)
            {
                List<double> brojNovcaList = najboljeMusterije.Select(m =>
                {
                    double brojNovca = 0;
                    m.KupljeniBuketi.ForEach(buket => brojNovca += buket.Cijena);
                    return brojNovca;
                }).ToList();
                double maxNovca = brojNovcaList.Max();
                List<Mušterija> sveMusterijeSaMaxSumom = najboljeMusterije
                    .Where((musterija, index) => brojNovcaList[index] == maxNovca).ToList();
                //da li ima vise njih koji su potrosili isti iznos novca
                //ako ima baci izuzetak
                if (sveMusterijeSaMaxSumom.Count > 1)
                    throw new ArgumentException("Najbolja musterija se ne moze tacno odrediti.");
                return sveMusterijeSaMaxSumom[0];
            }
            return najboljeMusterije[0];
        }

        public List<Buket> PretražiBukete(Vrsta vrsta)
        {
            var trazeni = DajSveBukete()
            .Where(buket =>
                (buket.Cvijeće.Any(cvijet => cvijet.Vrsta == vrsta))).ToList();
            return trazeni;

        }


        #endregion
    }
}