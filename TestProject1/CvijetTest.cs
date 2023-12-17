using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestProject1
{
    [TestClass]
    public class CvijetTest
    {
        static Cvijet cvijet;
        [TestInitialize]
        public void InicijalizacijaPrijeSvakogTesta()
        {
            cvijet = new Cvijet(Vrsta.Ljiljan, "Lilium bosniacum", "Crvena", DateTime.Now.AddDays(-2), 10);
        }

        [TestMethod]
        public void Test1ProvjeriKrajSezone()
        {
            cvijet.Sezonsko = false;
            cvijet.ProvjeriKrajSezone();
            Assert.AreEqual(cvijet.Kolicina, 10);

            cvijet.Sezonsko = true;
            cvijet.ProvjeriKrajSezone();
            Assert.AreEqual(cvijet.Kolicina, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Test2LatinskoIme()
        {
            Assert.AreEqual(cvijet.LatinskoIme, "Lilium bosniacum");

            cvijet.LatinskoIme = "";
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Test3LatinskoIme()
        {
            Cvijet c = new Cvijet(Vrsta.Ljiljan, null, "Crvena", DateTime.Now.AddDays(-2), 10);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Test4Kolicina()
        {
            cvijet.Kolicina = -5;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Test5DatumBranja()
        {
            Assert.AreNotEqual(cvijet.DatumBranja, DateTime.Now);

            cvijet.DatumBranja = DateTime.Now.AddDays(2);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Test6Boja()
        {
            Assert.AreEqual(cvijet.Boja, "Crvena");

            cvijet.Boja = "crvena";
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Test7Boja()
        {
            cvijet.Boja = "Narandžasta";
        }

        public static IEnumerable<object[]> UčitajPodatkeXML()
        {
            Vrsta vrsta;
            XmlDocument doc = new XmlDocument();
            doc.Load("Cvijece.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                yield return new object[] { elements[0], elements[1],
                    elements[2], DateTime.Parse(elements[3]), Int32.Parse(elements[4]) };
            }
        }

        static IEnumerable<object[]> CvijeceXML
        {
            get
            {
                return UčitajPodatkeXML();
            }
        }
        [TestMethod]
        [DynamicData("CvijeceXML")]
        [ExpectedException(typeof(FormatException))]
        public void TestKonstruktoraPacijentaXML(string vrsta, string ime, string boja, DateTime datumBiranja, int kolicina)
        {
            Vrsta v = (Vrsta)Enum.Parse(typeof(Vrsta), vrsta);
            Cvijet c = new Cvijet(v, ime, boja, datumBiranja, kolicina);
        }
    }
}
  
    
    



