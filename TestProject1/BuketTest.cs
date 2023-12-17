using CsvHelper;
using CsvHelper.Configuration;
using Cvjecara;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

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

        [TestMethod]
        public void TestDodajCvijet_WithCsvData()
        {
            // Arrange
            var csvRecords = ReadCsvTestData("CsvCvijetTestData.csv");

            // Act and Assert
            foreach (var record in csvRecords)
            {
                if (!Enum.TryParse<Vrsta>(record.Vrsta, out var vrsta))
                {
                    Console.WriteLine($"Unable to parse Vrsta: {record.Vrsta}");
                    continue;
                }

                buket.DodajCvijet(new Cvijet(vrsta, record.Naziv, record.Boja, record.Datum, (int)record.Cijena));
            }
        }

        [TestMethod]
        public void TestDodajDodatak_WithXmlData()
        {
            // Arrange
            var xmlRecords = ReadXmlTestData("..\\..\\..\\XmlDodatakTestData.xml");

            // Act and Assert
            foreach (var record in xmlRecords)
            {
                buket.DodajDodatak(record.Dodatak);
            }
        }

        [TestMethod]
        public void TestDodajPoklon_WithCsvData()
        {
            // Arrange
            var csvRecords = ReadCsvTestData("..\\..\\..\\CsvPoklonTestData.csv");

            // Act and Assert
            foreach (var record in csvRecords)
            {
                buket.DodajPoklon(new Poklon(record.Opis, record.PostotakPopusta));
            }
        }

        [TestMethod]
        public void TestMoqExample()
        {
            // Arrange
            var mockService = new Mock<IMyService>();
            mockService.Setup(service => service.GetValue()).Returns("Mocked Value");

            // Act
            var result = mockService.Object.GetValue();

            // Assert
            Assert.AreEqual("Mocked Value", result);
        }

        private List<TestData> ReadCsvTestData(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    return csv.GetRecords<TestData>().ToList();
                }
            }
            catch (CsvHelper.HeaderValidationException ex)
            {
                Console.WriteLine($"Header validation failed: {ex.Message}");
                throw;
            }
        }

        private List<TestData> ReadXmlTestData(string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<TestData>));
            using (var reader = new StreamReader(filePath))
            {
                return (List<TestData>)serializer.Deserialize(reader);
            }
        }

        public class TestData
        {
            public string Vrsta { get; set; }
            public string Naziv { get; set; }
            public string Boja { get; set; }
            public DateTime Datum { get; set; }
            public double Cijena { get; set; }

            public string Dodatak { get; set; }
            public string Opis { get; set; }
            public double PostotakPopusta { get; set; }
        }
    }

    public interface IMyService
    {
        string GetValue();
    }
}
