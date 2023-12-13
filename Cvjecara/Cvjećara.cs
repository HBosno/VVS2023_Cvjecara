
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
        /* Simulacija CodeStream komentara: 
         * Metoda sadrži logiku za rad sa cvijećem ali nigdje nisu obrazložene moguće opcije (parametar opcija).
         * Potrebno dodati komentar s objašnjenjem rada metode.
        */
        public void RadSaCvijećem(Cvijet c, int opcija)
        {
            if (opcija == 0)
            {
                /* Simulacija CodeStream komentara:
                 * Odvojiti logiku validaciju u zasebnu metodu, smanjiti dupliranje koda i poboljšati čitljivost.
                */
                if (c == null)
                    throw new NullReferenceException("Nemoguće dodati cvijet koji ne postoji!");
                else if (cvijeće.Contains(c))
                    throw new InvalidOperationException("Nemoguće dodati cvijet koji već postoji!");
                else
                    cvijeće.Add(c);
            }
            else if (opcija == 1)
            {
                if (c == null)
                    throw new NullReferenceException("Nemoguće izmijeniti cvijet koji ne postoji!");
                else if (cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme) == null)
                    throw new InvalidOperationException("Nemoguće izmijeniti cvijet koji ne postoji!");
                else
                {
                    cvijeće.Remove(cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme));
                    cvijeće.Add(c);
                }
            }
            else if (opcija == 2)
            {
                if (c == null)
                    throw new NullReferenceException("Nemoguće obrisati cvijet koji ne postoji!");
                else if (cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme) == null)
                    throw new InvalidOperationException("Nemoguće obrisati cvijet koji ne postoji!");
                else
                {
                    cvijeće.Remove(cvijeće.Find(cvijet => cvijet.LatinskoIme == c.LatinskoIme));
                }
            }
            else
                throw new InvalidOperationException("Unijeli ste nepoznatu opciju!");
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
        /* Simulacija CodeStream komentara:
             * Poželjno objašnjenje rada metode.
        */
        public void IzvršiNabavku(string godišnjeDoba, string veličinaNarudžbe)
        {
            if (godišnjeDoba.Equals("Ljeto") || godišnjeDoba.Equals("Zima"))
                throw new ArgumentException("Nabavka nije dozvoljena ljeti ili zimi.");
            if (veličinaNarudžbe.Equals("Srednja"))
                throw new ArgumentException("Nije dozvoljena nabavka srednje velicine.");
            /* Simulacija CodeStream komentara:
             * U naredna dva if-a se podrazumijeva godišnje doba "Proljeće" ili "Jesen", a nigdje nije urađena validacija.
             * Također potrebna validacija za nesmislen parametar velicinaNarudzbe.
             */
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
                Cvijet ljiljan = new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now.AddDays(-1), 10);
                RadSaCvijećem(ljiljan, 2);
                ObrišiBuket(buketi[buketi.Count()-1]);
                Cvijet orhideja = new Cvijet(Vrsta.Orhideja, "Orchidaceae", "Roza", DateTime.Now.AddDays(-1), 100);
                RadSaCvijećem(orhideja, 0);
                Cvijet ruza = new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Narandžasta", DateTime.Now.AddDays(-1), 100);
                RadSaCvijećem(ruza, 0);
            }
        }
        /* Simulacija CodeStream komentara:
             * Poželjno objašnjenje rada metode.
        */
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
        /*
          * Simulacija CodeStream komentara:
          * Potreban manji refaktoring metode. Ukloniti dupliranje koda, koristiti gotove metode za nalaženje maksimuma i slično.
        */
        public Mušterija DajNajboljuMušteriju()
        {
            //Ovu metodu je implementirao Dzenan Nuhic
            if (mušterije.Count == 0)
            {
                throw new ArgumentException("Cvjecara nema nijednu musteriju"); 
            }
            int maxCvijeca = -1;
            //trazenje musterije koji je kupio najvise cvijeca
            mušterije.ForEach(musterija => 
                {
                    int brojCvijeca = 0;
                    musterija.KupljeniBuketi.ForEach(buket => brojCvijeca += buket.Cvijeće.Count);
                    if (brojCvijeca > maxCvijeca)
                        maxCvijeca = brojCvijeca;
                }
            );
            List<Mušterija> najboljeMusterije = new List<Mušterija>();
            //provjera da li ima musterija koji su kupili isti broj cvijeca ko najbolji, te ako ima dodati ih u listu
            najboljeMusterije.AddRange(mušterije.FindAll(musterija =>
            {
                int brojCvijeca = 0;
                musterija.KupljeniBuketi.ForEach(buket => brojCvijeca += buket.Cvijeće.Count);
                /*
                 * Simulacija CodeStream komentara:
                 * Ovo je drugi put da se računaju isti podaci - suma kupljenog cvijeca svakog buketa za svakog od mušterije.
                */
                if (brojCvijeca == maxCvijeca)
                    return true;
                return false;
            }));
            double maxNovca = -1;
            //ako ima vise najboljih gleda se broj para koji su najbolji potrosili
            if(najboljeMusterije.Count > 1)
            {
                
                Mušterija najbolji = null;
                najboljeMusterije.ForEach(m =>
                {
                    double brojNovca = 0;
                    m.KupljeniBuketi.ForEach(buket => brojNovca += buket.Cijena);
                    if(brojNovca > maxNovca)
                    {
                        najbolji = m;
                        maxNovca = brojNovca;
                    }
                });
                //da li ima vise njih koji su potrosili isti iznos novca
                List<Mušterija> temp = najboljeMusterije.FindAll(m =>
                {
                    double brojNovca = 0;
                    m.KupljeniBuketi.ForEach(buket => brojNovca += buket.Cijena);
                    /*
                     * Simulacija CodeStream komentara:
                     * Ovo je drugi put da se računaju isti podaci - suma potrošenog novca za svakog od mušterije.
                    */
                    if (brojNovca == maxNovca)
                        return true;
                    return false;
                });
                //ako ima baci izuzetak
                if (temp.Count > 1)
                    throw new ArgumentException("Najbolja musterija se ne moze tacno odrediti.");
                return najbolji;
            }
            return najboljeMusterije[0];
        }
        #endregion
    }
}
