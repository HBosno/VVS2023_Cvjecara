using CsvHelper;
using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Moq;

namespace TestProject1
{
    [TestClass]
    public class BuketTest
    {
        Buket buket;
        [TestInitialize]
        public void InicijalizacijaTesta()
        {
            buket = new Buket(10);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Test1Izuzetak()
        {
            Buket b = new Buket(0.001);
        }

        [TestMethod]
        public void Test2DodajCvijet()
        {
            int velicinaPrije = buket.Cvijeće.Count;
            Cvijet cvijet = new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now.AddDays(-1), 10);
            buket.DodajCvijet(cvijet);
            int velicinaPoslije = buket.Cvijeće.Count;
            Assert.IsTrue(velicinaPoslije - velicinaPrije == 1);
        }

        [TestMethod]
        public void Test3DodajDodatak()
        {
            int velicinaPrije = buket.Dodaci.Count;
            string dodatak1 = "Slama", dodatak2 = "Lišće", dodatak3 = "Trava";
            buket.DodajDodatak(dodatak1);
            buket.DodajDodatak(dodatak2);
            buket.DodajDodatak(dodatak3);
            int velicinaPoslije = buket.Dodaci.Count;
            Assert.IsTrue(velicinaPoslije - velicinaPrije == 3);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Test4DodajDodatak()
        {
            string dodatak = "Blato";
            buket.DodajDodatak(dodatak);
        }

        [TestMethod]
        public void Test5DodajPoklon()
        {
            Poklon poklon = new Poklon("biciklo", 0.2);
            buket.DodajPoklon(poklon);
        }

        [TestMethod]
        public void Test6CijenaGetter()
        {
            Assert.AreEqual(buket.Cijena, 10);
        }

    }
}
