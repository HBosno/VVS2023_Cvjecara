using CsvHelper;
using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using Moq;

namespace UnitTestovi
{
    [TestClass]
    public class PoklonTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test1Konstruktor()
        {
            Poklon poklon = new Poklon("Slusalice", 0);
        }

        [TestMethod]
        public void Test2Sifra()
        {
            Poklon poklon = new Poklon("Mobitel", 0.2);
            Assert.AreEqual(poklon.Šifra, "10000");
        }

        public static IEnumerable<object[]> UcitajPodatkeCSV()
        {
            var path = @"C:\Users\Korisnik\Desktop\VVS\Cvjecara\TestProject1\Poklon.csv";
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], Double.Parse(elements[1]) };
                }
            }
        }

        public static IEnumerable<object[]> UcitajPodatkeXML()
        {
            var path = @"C:\Users\Korisnik\Desktop\VVS\Cvjecara\TestProject1\Poklon.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlNodeList nodeList = xmlDoc.SelectNodes("/Root/Poklon");
            foreach (XmlNode node in nodeList)
            {
                yield return new object[]
                {
                    node.SelectSingleNode("Opis").InnerText,
                    Double.Parse(node.SelectSingleNode("Postotak").InnerText)
                };
            }
        }

        static IEnumerable<object[]> PodaciIzCSV
        {
            get
            {
                return UcitajPodatkeCSV();
            }
        }

        static IEnumerable<object[]> PodaciIzXML
        {
            get
            {
                return UcitajPodatkeXML();
            }
        }

        [TestMethod]
        [DynamicData("PodaciIzCSV")]
        public void TestKonstruktoraPoklonCSV(string opis, double postotak)
        {
            Poklon p = new Poklon(opis, postotak);
            Assert.AreEqual(p.PostotakPopusta, postotak);
        }

        [TestMethod]
        [DynamicData("PodaciIzXML")]
        public void TestKonstruktoraPoklonXML(string opis, double postotak)
        {
            Poklon p = new Poklon(opis, postotak);
            Assert.AreEqual(p.PostotakPopusta, postotak);
        }

        [TestMethod]
        public void TestOdrediCijenuPoklona()
        {
            var mockCijenaPoklona = new Mock<ICijenaPoklona>();
            mockCijenaPoklona.Setup(c => c.OdrediCijenuPoklona(It.IsAny<Poklon>())).Returns(50.0);
            Poklon poklon = new Poklon("Poklon", 0.2);
            double rezultat = poklon.OdrediCijenuPoklona(mockCijenaPoklona.Object);
            Assert.AreEqual(50.0, rezultat);
        }

    }
}
