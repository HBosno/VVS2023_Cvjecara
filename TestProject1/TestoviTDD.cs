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


        [TestMethod]
        public void PretražiBukete_PoVrsti_VratiRuze()
        {
            Cvjećara cvjecara = new Cvjećara();
            List<Cvijet> cvijece1 = new List<Cvijet>
            {
            new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 5),
            new Cvijet(Vrsta.Orhideja, "Orchidaceae", "Roza", DateTime.Now, 5),
            };
            cvjecara.DodajBuket(cvijece1, new List<String>(), null, 50);
            List<Cvijet> cvijece2 = new List<Cvijet>
            {
            new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 5)
            };
            cvjecara.DodajBuket(cvijece2, new List<String>(), null, 50);
            var trazeni = cvjecara.PretražiBukete(Vrsta.Ruža);
            CollectionAssert.AreEqual(trazeni, new[] { cvjecara.DajSveBukete()[0] });
        }

        [TestMethod]
        public void PretražiBukete_PoBoji_VratiBijele()
        {

            Cvjećara cvjecara = new Cvjećara();
            List<Cvijet> cvijece1 = new List<Cvijet>
        {
        new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 5),
        new Cvijet(Vrsta.Orhideja, "Orchidaceae", "Roza", DateTime.Now, 5),
        };
            cvjecara.DodajBuket(cvijece1, new List<String>(), null, 50);
            List<Cvijet> cvijece2 = new List<Cvijet>
        {
           new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 5)
            };
            cvjecara.DodajBuket(cvijece2, new List<String>(), null, 50);
            var trazeni = cvjecara.PretražiBukete(null, "Bijela");
            CollectionAssert.AreEqual(trazeni, new[] { cvjecara.DajSveBukete()[1] });
        }


        [TestMethod]
        public void PretražiBukete_PoVrstiIBoji_VratiRužeIŽute()
        {
            Cvjećara cvjecara = new Cvjećara();
            List<Cvijet> cvijece1 = new List<Cvijet>
{
            new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 5),
            new Cvijet(Vrsta.Orhideja, "Orchidaceae", "Roza", DateTime.Now, 5),
};
            cvjecara.DodajBuket(cvijece1, new List<String>(), null, 50);
            List<Cvijet> cvijece2 = new List<Cvijet>
{
            new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 5)
};
            cvjecara.DodajBuket(cvijece2, new List<String>(), null, 50);
            List<Cvijet> cvijece3 = new List<Cvijet>
{
            new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Bijela", DateTime.Now, 5),
            new Cvijet(Vrsta.Ruža, "Rosa rubiginosa", "Crvena", DateTime.Now, 5),
            new Cvijet(Vrsta.Neven, " Calendula officinalis", "Žuta", DateTime.Now,    5)
};
            cvjecara.DodajBuket(cvijece3, new List<String>(), null, 50);
            var trazeni = cvjecara.PretražiBukete(Vrsta.Ruža, "Žuta");
            CollectionAssert.AreEqual(trazeni, new[] { cvjecara.DajSveBukete()[2] });
        }



    }


}
