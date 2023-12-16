using CsvHelper;
using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Moq;

namespace TestProject1
{
    [TestClass]
    public class MušterijaTest
    {
        Mušterija musterija;
        Mock<Mušterija> mockMusterija;

        [TestInitialize]
        public void InicijalizacijaTesta()
        {
            musterija = new Mušterija("Aldin Islamagić");
            mockMusterija = new Mock<Mušterija>();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void RegistrujKupovinu_NullVr_Izuzetak()
        {
            musterija.RegistrujKupovinu(null, null);
        }

        [TestMethod]
        public void GetImeIPrezime_Getter_ImeIprezime()
        {
            Assert.AreEqual("Aldin Islamagić", musterija.ImeIPrezime);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void SetImeIPrezime_NullVr_Izuzetak()
        {
            musterija.ImeIPrezime = null;
        }

        [TestMethod]
        public void GetIndentifikacijskiBroj_Getter_IdentifikacijskiBroj()
        {
            var id = musterija.IdentifikacijskiBroj;
        }

        [TestMethod]
        public void RegistrujKupovinu_DodanaKupovina_BrojKupovina()
        {
            Buket b = new Buket(3);
            Poklon p = new Poklon("test", 10);
            musterija.RegistrujKupovinu(b, p);
            Assert.AreEqual(1, musterija.UkupanBrojKupovina);
        }

        [TestMethod]
        public void GetRegistrujKupovinu_DodaneKupovine_BrojKupovina()
        {
            Buket b = new Buket(3);
            Poklon p = new Poklon("test", 10);
            musterija.RegistrujKupovinu(b, p);
            Assert.AreEqual(b, musterija.KupljeniBuketi[0]);
            Assert.AreEqual(p, musterija.KupljeniPokloni[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NajcesciCvijet_BezBuketa_Izuzetak()
        {
            var najcesciCvijet = musterija.NajčešćiCvijet();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NajcesciCvijet_PogresanPoklon_Izuzetak()
        {
            var b = new Buket(3);
            var p = new Poklon("test", 10);
            musterija.RegistrujKupovinu(b, p);
            var najcesciCvijet = musterija.NajčešćiCvijet();
        }

        [TestMethod]
        public void NajcesciCvijet_ValidniPodaci_NajcesciCvijet()
        {
            var buket = new Buket(3);
            Vrsta vrstaCvijeta1 = Vrsta.Ljiljan;
            Vrsta vrstaCvijeta2 = Vrsta.Margareta;
            Vrsta vrstaCvijeta3 = Vrsta.Ljiljan;
            buket.DodajCvijet(new Cvijet(vrstaCvijeta1, "ljiljan1", "Žuta", new DateTime(2023, 12, 15), 1));
            buket.DodajCvijet(new Cvijet(vrstaCvijeta2, "margareta", "Žuta", new DateTime(2023, 12, 15), 1));
            buket.DodajCvijet(new Cvijet(vrstaCvijeta3, "ljiljan2", "Žuta", new DateTime(2023, 12, 13), 1));
            Poklon poklon = new Poklon("test", 10);
            musterija.RegistrujKupovinu(buket, poklon);
            var najcesciCvijet = musterija.NajčešćiCvijet();
            Assert.AreEqual(Vrsta.Ljiljan, najcesciCvijet);
        }

        private void registrujKupovine()
        {
            for (int i = 1; i <= 100; i++)
            {
                musterija.RegistrujKupovinu(new Buket(i), new Poklon("", 0.1));
            }
        }

        [TestMethod]
        public void NagradnaKupovina_ValidniPod_True()
        {
            registrujKupovine();
            Poklon poklon = new Poklon("test", 0.1);
            var rezultat = musterija.NagradnaKupovina(poklon);
            Assert.IsTrue(rezultat);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NagradnaKupovina_ManjakKupovina_Izuzetak()
        {
            registrujKupovine();
            Poklon poklon = new Poklon("test", 0.5);
            var rezultat = musterija.NagradnaKupovina(poklon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NagradnaKupovina_PogresanPopust_Izuzetak()
        {
            Poklon poklon = new Poklon("test", 0.1);
            var rezultat = musterija.NagradnaKupovina(poklon);
        }

        public static IEnumerable<object[]> UčitajPodatkeCSV()
        {
            using (var reader = new StreamReader("..\\..\\..\\MusterijaPodaci.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], elements[1] };
                }
            }
        }

        static IEnumerable<object[]> MusterijaCSV
        {
            get
            {
                return UčitajPodatkeCSV();
            }
        }

        [TestMethod]
        [DynamicData("MusterijaCSV")]
        public void GetImeIPrezime_CSVPodaci_ImeIprezime(string imeIprezime, string ocekivanaVrijednost)
        {
            Mušterija musterijaTest = new Mušterija(imeIprezime);
            var rez = musterijaTest.ImeIPrezime;
            Assert.AreEqual(ocekivanaVrijednost, rez);
        }

        public static IEnumerable<object[]> UčitajPodatkeXML()
        {
            Vrsta vrsta;
            XmlDocument doc = new XmlDocument();
            doc.Load("..\\..\\..\\MusterijaKupovine.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                yield return new object[] { Double.Parse(elements[0]), elements[1],
                    Double.Parse(elements[2])};
            }
        }

        static IEnumerable<object[]> MusterijaKupovineXML
        {
            get
            {
                return UčitajPodatkeXML();
            }
        }
        [TestMethod]
        [DynamicData("MusterijaKupovineXML")]
        public void RegistrujKupovinu_XMLPodaci_BrojKupovina(double cijena, String opis, double popust)
        {
            Buket b = new Buket(cijena);
            Poklon p = new Poklon(opis, popust);
            musterija.RegistrujKupovinu(b, p);
            Assert.AreEqual(1, musterija.UkupanBrojKupovina);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void RegistrujKupovinu_NullVrSaMock_Izuzetak()
        {
            mockMusterija.Setup(m => m.RegistrujKupovinu(null, null))
                         .Throws(new NotSupportedException());

            mockMusterija.Object.RegistrujKupovinu(null, null);
        }
    }
}
