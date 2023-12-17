using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class TestoviTDD
    {
        [TestMethod]
        public void PopustZaVelikiBuket_11Cvjetova_PrimijenjenPopust()
        {
            Buket buket = new Buket(50);
            buket.DodajCvijet(new Cvijet(Vrsta.Ruža, "Rosa Rubingosa", "Crvena", DateTime.Now, 11));
            buket.PopustZaVelikiBuket();
            double novaCijena = 45;
            Assert.AreEqual(novaCijena, buket.Cijena);
        }

        [TestMethod]
        public void PopustZaVelikiBuket_30Cvjetova_PrimijenjenPopust()
        {
            Buket buket = new Buket(135);
            buket.DodajCvijet(new Cvijet(Vrsta.Ruža, "Rosa Rubingosa", "Crvena", DateTime.Now, 30));
            buket.PopustZaVelikiBuket();
            double novaCijena = 121.5;
            Assert.AreEqual(novaCijena, buket.Cijena);
        }

    }
}
