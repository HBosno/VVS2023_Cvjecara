using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Xml;
using CsvHelper;
using System.Globalization;
using System.IO;
using Castle.Core.Internal;

namespace TestProject1
{
    [TestClass]
    public class TestCvjećara
    {
        [TestMethod]
        public void RadSaCvijećem_DodajCvijet_DodanCvijet()
        {
            Cvjećara cvjecara = new Cvjećara();
            Cvijet cvijet = new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20);
            cvjecara.RadSaCvijećem(cvijet, 0);
            Assert.IsTrue(cvjecara.Cvijeće.Contains(cvijet));
        }

        [TestMethod]
        public void RadSaCvijećem_IzmijeniCvijet_IzmijenjenCvijet()
        {
            Cvjećara cvjecara = new Cvjećara();
            Cvijet cvijet = new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20);
            cvjecara.RadSaCvijećem(cvijet, 0);
            cvijet.Kolicina = 10;
            cvjecara.RadSaCvijećem(cvijet, 1);
            Assert.AreEqual(cvjecara.Cvijeće[0].Kolicina, 10);
            Assert.AreEqual(cvjecara.Cvijeće.Count, 1);
        }

        [TestMethod]
        public void RadSaCvijećem_ObrisiCvijet_ObrisanCvijet()
        {
            Cvjećara cvjecara = new Cvjećara();
            Cvijet cvijet = new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20);
            cvjecara.RadSaCvijećem(cvijet, 0);
            cvjecara.RadSaCvijećem(cvijet, 2);
            Assert.IsFalse(cvjecara.Cvijeće.Contains(cvijet));
            Assert.AreEqual(cvjecara.Cvijeće.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Unijeli ste nepoznatu opciju!")]
        public void RadSaCvijećem_NepoznataOpcija_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            Cvijet cvijet = new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20);
            cvjecara.RadSaCvijećem(cvijet, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Nemoguće dodati cvijet koji ne postoji!")]
        public void RadSaCvijećem_DodajNull_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            cvjecara.RadSaCvijećem(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Nemoguće izmijeniti cvijet koji ne postoji!")]
        public void RadSaCvijećem_IzmijeniNull_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            cvjecara.RadSaCvijećem(null, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Nemoguće obrisati cvijet koji ne postoji!")]
        public void RadSaCvijećem_ObrisiNull_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            cvjecara.RadSaCvijećem(null, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Nemoguće dodati cvijet koji već postoji!")]
        public void RadSaCvijećem_DodajPostojeci_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            Cvijet cvijet = new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20);
            cvjecara.RadSaCvijećem(cvijet, 0);
            cvjecara.RadSaCvijećem(cvijet, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Nemoguće izmijeniti cvijet koji ne postoji!")]
        public void RadSaCvijećem_IzmijeniNepostojeci_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            Cvijet cvijet = new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20);
            cvjecara.RadSaCvijećem(cvijet, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Nemoguće obrisati cvijet koji ne postoji!")]
        public void RadSaCvijećem_ObrisiNepostojeci_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            Cvijet cvijet = new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20);
            cvjecara.RadSaCvijećem(cvijet, 2);
        }

        public static IEnumerable<object[]> ReadCSV()
        {
            using (var reader = new StreamReader("..\\..\\..\\podaciCvjecaraCSV.txt"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    yield return new object[] { row.vrsta, row.latinskoIme, row.boja, Int32.Parse(row.kolicina) };
                }
            }
        }

        static IEnumerable<object[]> CvjecaraCSV
        {
            get
            {
                return ReadCSV();
            }
        }

        [TestMethod]
        [DynamicData("CvjecaraCSV")]
        public void RadSaCvijećem_DodajIObrisi_DodanIObrisanCvijet(string vrsta, string latinskoIme, string boja, int kolicina)
        {
            Cvjećara cvjecara = new Cvjećara();
            Vrsta vrstaCvijeta = (Vrsta)Enum.Parse(typeof(Vrsta), vrsta);
            Cvijet cvijet = new Cvijet(vrstaCvijeta, latinskoIme, boja, DateTime.Now, kolicina);
            cvjecara.RadSaCvijećem(cvijet, 0);
            Assert.IsTrue(cvjecara.Cvijeće.Contains(cvijet));
            cvjecara.RadSaCvijećem(cvijet, 2);
            Assert.AreEqual(cvjecara.Cvijeće.Count, 0);
        }

        public static IEnumerable<object[]> ReadXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("..\\..\\..\\podaciCvjecaraXML.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                yield return new object[] { elements[0], elements[1], elements[2], Int32.Parse(elements[3]) };
            }
        }

        static IEnumerable<object[]> CvjecaraXML
        {
            get
            {
                return ReadXML();
            }
        }

        [TestMethod]
        [DynamicData("CvjecaraXML")]
        public void RadSaCvijećem_Izmijeni_IzmijenjenCvijet(string vrsta, string latinskoIme, string boja, int kolicina)
        {
            Cvjećara cvjecara = new Cvjećara();
            Vrsta vrstaCvijeta = (Vrsta)Enum.Parse(typeof(Vrsta), vrsta);
            Cvijet cvijet = new Cvijet(vrstaCvijeta, latinskoIme, boja, DateTime.Now, kolicina);
            cvjecara.RadSaCvijećem(cvijet, 0);
            cvijet.Kolicina = 60;
            cvijet.Boja = "Žuta";
            cvjecara.RadSaCvijećem(cvijet, 1);
            Assert.AreEqual(cvjecara.Cvijeće[0].Kolicina, 60);
            Assert.AreEqual(cvjecara.Cvijeće[0].Boja, "Žuta");
        }

        [TestMethod]
        public void DodajBuket_Dodavanje_DodanBuket()
        {
            Cvjećara cvjecara = new Cvjećara();
            List<Cvijet> cvijeće = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20),
                new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 10)
            };
            List<string> dodaci = new List<string>
            {
                "Slama",
                "Lišće"
            };
            Poklon poklon = new Poklon("Popust 10%", 0.1);
            double cijena = 50;
            cvjecara.DodajBuket(cvijeće, dodaci, poklon, cijena);
            List<Buket> buketi = cvjecara.DajSveBukete();
            Assert.AreEqual(1, buketi.Count);
            Buket dodaniBuket = buketi[0];
            Assert.AreEqual(cijena, dodaniBuket.Cijena);
            Assert.AreEqual(poklon, dodaniBuket.Poklon);
            CollectionAssert.AreEqual(cvijeće, dodaniBuket.Cvijeće);
            CollectionAssert.AreEqual(dodaci, dodaniBuket.Dodaci);
        }

        [TestMethod]
        public void ObrišiBuket_Brisanje_ObrisanBuket()
        {
            Cvjećara cvjecara = new Cvjećara();
            List<Cvijet> cvijeće = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20),
                new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 10)
            };
            List<string> dodaci = new List<string>
            {
                "Slama",
                "Lišće"
            };
            Poklon poklon = new Poklon("Popust 10%", 0.1);
            double cijena = 50;
            cvjecara.DodajBuket(cvijeće, dodaci, poklon, cijena);
            Buket b = cvjecara.DajSveBukete()[0];
            cvjecara.ObrišiBuket(b);
            Assert.AreEqual(0, cvjecara.DajSveBukete().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Nabavka nije dozvoljena ljeti ili zimi.")]
        public void IzvršiNabavku_Ljeto_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            cvjecara.IzvršiNabavku("Ljeto", "Mala");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Nabavka nije dozvoljena ljeti ili zimi.")]
        public void IzvršiNabavku_Zima_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            cvjecara.IzvršiNabavku("Zima", "Mala");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Nije dozvoljena nabavka srednje velicine.")]
        public void IzvršiNabavku_SrednjaNabavka_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            cvjecara.IzvršiNabavku("Jesen", "Srednja");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Nabavka je dozvoljena samo u proljeće ili jesen.")]
        public void IzvršiNabavku_NesmislenoGodisnje_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            cvjecara.IzvršiNabavku("BlaBla", "Mala");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Nabavka je dozvoljena samo u proljeće ili jesen.")]
        public void IzvršiNabavku_NesmislenaVelicinaNarudzbe_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            cvjecara.IzvršiNabavku("Proljeće", "BlaBla");
        }

        [TestMethod]
        public void IzvršiNabavku_MalaNarudzba_DodanoCvijece()
        {
            Cvjećara cvjecara = new Cvjećara();
            cvjecara.IzvršiNabavku("Proljeće", "Mala");
            Assert.AreEqual(cvjecara.Cvijeće.Count, 3);
        }

        [TestMethod]
        public void IzvršiNabavku_VelikaNarudzba_DodanoCvijece()
        {
            Cvjećara cvjecara = new Cvjećara();
            cvjecara.IzvršiNabavku("Proljeće", "Velika");
            Assert.AreEqual(cvjecara.Cvijeće.Count, 2);
        }

        [TestMethod]
        public void PregledajCvijeće_SaCvijecemZaIzbaciti_IzbacenoCvijece()
        {
            Cvjećara cvjecara = new Cvjećara();
            List<Cvijet> cvijeće = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now.AddDays(-6), 10),
                new Cvijet(Vrsta.Orhideja, "Orchidaceae", "Roza", DateTime.Now, 10)
            };
            cvjecara.Cvijeće.AddRange(cvijeće);
            cvjecara.PregledajCvijeće();
            Assert.AreEqual(cvjecara.Cvijeće.Count, 1);
            Assert.AreEqual(cvjecara.Cvijeće[0].LatinskoIme, "Orchidaceae");
        }

        [TestMethod]
        public void PregledajCvijeće_PraznaLista_BezPromjena()
        {
            Cvjećara cvjecara = new Cvjećara();
            cvjecara.PregledajCvijeće();
            CollectionAssert.AreEqual(new List<Cvijet>(), cvjecara.Cvijeće);
        }

        [TestMethod]
        public void PregledajCvijeće_BezCvijecaZaIzbaciti_BezPromjene()
        {
            Cvjećara cvjecara = new Cvjećara();
            List<Cvijet> cvijeće = new List<Cvijet>
            {
                new Cvijet(Vrsta.Orhideja, "Orchidaceae", "Roza", DateTime.Now, 10),
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20)
            };
            cvjecara.Cvijeće.AddRange(cvijeće);
            cvjecara.PregledajCvijeće();
            Assert.AreEqual(cvjecara.Cvijeće.Count, 2);
        }

        [ExpectedException(typeof(InvalidOperationException), "Traženi buket nije na stanju!")]
        [TestMethod]
        public void NaručiCvijeće_NemaBuketa_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            Mušterija m = new Mušterija("testna mušterija");
            Buket b = new Buket(50);
            Poklon poklon = new Poklon("Popust 10%", 0.1);
            cvjecara.NaručiCvijeće(m, b, poklon);
        }

        [TestMethod]
        public void NaručiCvijeće_PostojiBuket_RegistrovanaKupovina()
        {
            Cvjećara cvjecara = new Cvjećara();
            Mušterija m = new Mušterija("testna mušterija");
            List<string> dodaci = new List<string>
            {
                "Slama",
                "Lišće"
            };
            List<Cvijet> cvijeće = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20),
                new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 10)
            };
            Poklon poklon = new Poklon("Popust 10%", 0.1);
            double cijena = 50;
            cvjecara.DodajBuket(cvijeće, dodaci, poklon, cijena);
            Buket b = cvjecara.DajSveBukete()[0];
            cvjecara.NaručiCvijeće(m, b, poklon);
            Assert.IsTrue(m.KupljeniBuketi.Contains(b));
            Assert.IsTrue(m.KupljeniPokloni.Contains(poklon));
            Assert.AreEqual(1, m.UkupanBrojKupovina);
            Assert.IsTrue(cvjecara.NaručeniPokloni.Contains(poklon));
        }

        // izuzetak se baca zbog nacina rada metode NagradnaKupovina u Mušterija.cs klasi
        [ExpectedException(typeof(ArgumentException), "Proslijeđeni poklon ne ispunjava parametar")]
        [TestMethod]
        public void NaručiCvijeće_PostojiBuketINagrada_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            Mušterija m = new Mušterija("testna mušterija");
            List<string> dodaci = new List<string>
            {
                "Slama",
                "Lišće"
            };
            List<Cvijet> cvijeće = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20),
                new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 10)
            };
            Poklon poklon = new Poklon("Popust 10%", 0.1);
            double cijena = 50;
            cvjecara.DodajBuket(cvijeće, dodaci, poklon, cijena);
            Buket b = cvjecara.DajSveBukete()[0];
            cvjecara.NaručiCvijeće(m, b, poklon, poklon);
            Assert.IsTrue(m.KupljeniBuketi.Contains(b));
            Assert.IsTrue(m.KupljeniPokloni.Contains(poklon));
            Assert.AreEqual(1, m.UkupanBrojKupovina);
            Assert.IsTrue(cvjecara.NaručeniPokloni.Contains(poklon));
        }

        [TestMethod]
        public void ProvjeriLatinskaImenaCvijeća_ValidnoIme_NeBrisiCvijet()
        {
            ILeksikon leksikon = new Leksikon();
            Cvjećara cvjecara = new Cvjećara();
            Cvijet validniCvijet = new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20);
            cvjecara.RadSaCvijećem(validniCvijet, 0);
            cvjecara.ProvjeriLatinskaImenaCvijeća(leksikon);
            Assert.AreEqual(1, cvjecara.Cvijeće.Count);
        }

        [TestMethod]
        public void ProvjeriLatinskaImenaCvijeća_NevalidnoIme_BrisiCvijet()
        {
            ILeksikon leksikon = new Leksikon();
            Cvjećara cvjecara = new Cvjećara();
            Cvijet nevalidniCvijet = new Cvijet(Vrsta.Ruža, "BlaBla", "Crvena", DateTime.Now, 20);
            cvjecara.RadSaCvijećem(nevalidniCvijet, 0);
            cvjecara.ProvjeriLatinskaImenaCvijeća(leksikon);
            Assert.AreEqual(0, cvjecara.Cvijeće.Count);
        }

        [TestMethod]
        public void DajSveNaručenePoklone_ValidanPopust_VraćaPoklone()
        {
            Mušterija mušterija = new Mušterija("testna mušterija");
            Poklon poklon1 = new Poklon("Popust 10%", 0.1);
            Poklon poklon2 = new Poklon("Popust 15%", 0.15);
            mušterija.KupljeniPokloni.Add(poklon1);
            mušterija.KupljeniPokloni.Add(poklon2);
            double popust = 0.1;
            Cvjećara cvjecara = new Cvjećara();
            List<Poklon> naruceni = cvjecara.DajSveNaručenePoklone(mušterija, popust);
            CollectionAssert.Contains(naruceni, poklon1);
            CollectionAssert.DoesNotContain(naruceni, poklon2);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Došlo je do greške! Pokušajte ponovo sa drugim parametrima zahtjeva.")]
        public void DajSveNaručenePoklone_NevalidanPopust_Izuzetak()
        {
            Mušterija mušterija = new Mušterija("testna mušterija");
            double nevalidan = 0.5;
            Cvjećara cvjecara = new Cvjećara();
            List<Poklon> naruceni = cvjecara.DajSveNaručenePoklone(mušterija, nevalidan);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Cvjecara nema nijednu musteriju")]
        public void DajNajboljuMušteriju_NemaMusterije_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            cvjecara.DajNajboljuMušteriju();
        }

        [TestMethod]
        public void DajNajboljuMušteriju_MusterijaSaViseCvijeca_VracaMusterijuSaViseCvijeca()
        {
            Cvjećara cvjecara = new Cvjećara();
            Mušterija mušterija1 = new Mušterija("musterija 1");
            Mušterija mušterija2 = new Mušterija("musterija 2");
            List<Cvijet> cvijece = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20)
            };
            cvjecara.DodajBuket(cvijece, new List<String>(), new Poklon("Popust 10%", 0.1), 50);
            Buket buket1 = cvjecara.DajSveBukete()[0];
            cvjecara.NaručiCvijeće(mušterija1, buket1, buket1.Poklon);
            List<Cvijet> cvijece2 = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 30),
                new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 50)
            };
            cvjecara.DodajBuket(cvijece2, new List<String>(), new Poklon("Popust 10%", 0.1), 50);
            Buket buket2 = cvjecara.DajSveBukete()[1];
            cvjecara.NaručiCvijeće(mušterija2, buket2, buket2.Poklon);
            cvjecara.Mušterije.Add(mušterija1);
            cvjecara.Mušterije.Add(mušterija2);
            Mušterija najboljaMušterija = cvjecara.DajNajboljuMušteriju();
            Assert.AreEqual(najboljaMušterija, mušterija2);
        }

        [TestMethod]
        public void DajNajboljuMušteriju_MusterijaSaVisePotrosnje_VracaMusterijuSaVisePotrosnje()
        {
            Cvjećara cvjecara = new Cvjećara();
            Mušterija mušterija1 = new Mušterija("musterija 1");
            Mušterija mušterija2 = new Mušterija("musterija 2");
            List<Cvijet> cvijece = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20),
                new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 10)
            };
            cvjecara.DodajBuket(cvijece, new List<String>(), new Poklon("Popust 10%", 0.1), 100);
            Buket buket1 = cvjecara.DajSveBukete()[0];
            cvjecara.NaručiCvijeće(mušterija1, buket1, buket1.Poklon);
            List<Cvijet> cvijece2 = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 30),
                new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 50)
            };
            cvjecara.DodajBuket(cvijece2, new List<String>(), new Poklon("Popust 10%", 0.1), 50);
            Buket buket2 = cvjecara.DajSveBukete()[1];
            cvjecara.NaručiCvijeće(mušterija2, buket2, buket2.Poklon);
            cvjecara.Mušterije.Add(mušterija1);
            cvjecara.Mušterije.Add(mušterija2);
            Mušterija najboljaMušterija = cvjecara.DajNajboljuMušteriju();
            Assert.AreEqual(najboljaMušterija, mušterija1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Najbolja musterija se ne moze tacno odrediti.")]
        public void DajNajboljuMušteriju_ViseNajboljeMusterije_Izuzetak()
        {
            Cvjećara cvjecara = new Cvjećara();
            Mušterija mušterija1 = new Mušterija("musterija 1");
            Mušterija mušterija2 = new Mušterija("musterija 2");
            List<Cvijet> cvijece = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20),
                new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 10)
            };
            cvjecara.DodajBuket(cvijece, new List<String>(), new Poklon("Popust 10%", 0.1), 100);
            Buket buket1 = cvjecara.DajSveBukete()[0];
            cvjecara.NaručiCvijeće(mušterija1, buket1, buket1.Poklon);
            List<Cvijet> cvijece2 = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 30),
                new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 50)
            };
            cvjecara.DodajBuket(cvijece2, new List<String>(), new Poklon("Popust 10%", 0.1), 100);
            Buket buket2 = cvjecara.DajSveBukete()[1];
            cvjecara.NaručiCvijeće(mušterija2, buket2, buket2.Poklon);
            cvjecara.Mušterije.Add(mušterija1);
            cvjecara.Mušterije.Add(mušterija2);
            cvjecara.DajNajboljuMušteriju();
        }

        [TestMethod]
        public void ProvjeriEkonomičnostBuketa_SviEkonomični_BezIzbacivanja()
        {
            var ekonomičnostMock = new Mock<IEkonomičnost>();
            ekonomičnostMock.Setup(e => e.ProvjeriIsplativost(It.IsAny<Buket>())).Returns(true);
            var cvjecara = new Cvjećara();
            List<Cvijet> cvijece1 = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20),
                new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 10)
            };
            List<Cvijet> cvijece2 = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20)
            };
            cvjecara.DodajBuket(cvijece1, new List<String>(), null, 50);
            cvjecara.DodajBuket(cvijece2, new List<String>(), null, 50);
            cvjecara.ProvjeriEkonomičnostBuketa(ekonomičnostMock.Object);
            Assert.AreEqual(2, cvjecara.DajSveBukete().Count);
        }

        [TestMethod]
        public void ProvjeriEkonomičnostBuketa_NemaIsplativih_IzbrisiSve()
        {
            var ekonomičnostMock = new Mock<IEkonomičnost>();
            ekonomičnostMock.Setup(e => e.ProvjeriIsplativost(It.IsAny<Buket>())).Returns(false);
            var cvjecara = new Cvjećara();
            List<Cvijet> cvijece1 = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20),
                new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 10)
            };
            List<Cvijet> cvijece2 = new List<Cvijet>
            {
                new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 20)
            };
            cvjecara.DodajBuket(cvijece1, new List<String>(), null, 50);
            cvjecara.DodajBuket(cvijece2, new List<String>(), null, 50);
            cvjecara.ProvjeriEkonomičnostBuketa(ekonomičnostMock.Object);
            Assert.AreEqual(0, cvjecara.DajSveBukete().Count);
        }
    }
}