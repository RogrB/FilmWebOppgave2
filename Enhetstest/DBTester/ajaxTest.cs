using BLL;
using DAL;
using Model;
using Graubakken_Filmsjappe.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Enhetstest.DBTester
{
    [TestClass]
    public class ajaxTest
    {
        [TestMethod]
        public void TestHentSkuespillere()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var skuespillere = new List<Skuespiller>();
            var Skuespiller = new Skuespiller()
            {
                id = 1,
                Fornavn = "Ole",
                Etternavn = "Olesen",
                Land = "Norge",
                Bilde = "Bilde01",
                Alder = 22,
                Filmer = new List<Film>()
            };
            var Film = new Film()
            {
                id = 1,
                Navn = "Film01",
                Bilde = "Bilde01"
            };
            Skuespiller.Filmer.Add(Film);

            skuespillere.Add(Skuespiller);
            skuespillere.Add(Skuespiller);
            skuespillere.Add(Skuespiller);
            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize(skuespillere);

            // Act
            var resultat = controller.HentSkuespillere();

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void LeggSkuespillerIFilmOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            
            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("OK");

            // Act
            var resultat = controller.LeggSkuespillerIFilm(1, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void LeggSkuespillerIFilmFeilSkuespiller()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.LeggSkuespillerIFilm(1, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void LeggSkuespillerIFilmFeilFilm()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.LeggSkuespillerIFilm(0, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void LeggSkuespillerIFilmFeilSkuespillerOgFilm()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.LeggSkuespillerIFilm(0, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettSkuespillerFraFilmOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("OK");

            // Act
            var resultat = controller.SlettSkuespillerFraFilm(1, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettSkuespillerFraFilmFeilFilm()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettSkuespillerFraFilm(0, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettSkuespillerFraFilmFeiSkuespiller()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettSkuespillerFraFilm(1, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettSkuespillerFraFilmFeilFilmOgSkuespiller()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettSkuespillerFraFilm(0, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void HentSjangereForFilmOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            List<Sjanger> sjangere = new List<Sjanger>();
            Sjanger sjanger = new Sjanger()
            {
                id = 1,
                sjanger = "Action"
            };
            sjangere.Add(sjanger);
            sjangere.Add(sjanger);
            sjangere.Add(sjanger);

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize(sjangere);

            // Act
            var resultat = controller.HentSjangereForFilm(1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void HentSjangereForFilmFeil()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            List<Sjanger> sjangere = new List<Sjanger>();
            Sjanger sjanger = new Sjanger()
            {
                id = 0,
            };
            sjangere.Add(sjanger);
            sjangere.Add(sjanger);
            sjangere.Add(sjanger);

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize(sjangere);

            // Act
            var resultat = controller.HentSjangereForFilm(0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void HentSjangereTest()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            List<Sjanger> sjangere = new List<Sjanger>();
            Sjanger sjanger = new Sjanger()
            {
                id = 1,
                sjanger = "Action"
            };
            sjangere.Add(sjanger);
            sjangere.Add(sjanger);
            sjangere.Add(sjanger);

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize(sjangere);

            // Act
            var resultat = controller.HentSjangere();

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void LeggSjangerIFilmOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("OK");

            // Act
            var resultat = controller.LeggSjangerIFilm(1, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void LeggSjangerIFilmFeilFilm()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.LeggSjangerIFilm(0, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void LeggSjangerIFilmFeilSjanger()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.LeggSjangerIFilm(1, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void LeggSjangerIFilmFeilFilmOgSjanger()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.LeggSjangerIFilm(0, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettSjangerFraFilmOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("OK");

            // Act
            var resultat = controller.SlettSjangerFraFilm(1, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettSjangerFraFilmFeilFilm()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettSjangerFraFilm(0, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettSjangerFraFilmFeilSjanger()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettSjangerFraFilm(1, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettSjangerFraFilmFeilFilmOgSjanger()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettSjangerFraFilm(0, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void TestHentFilmerForAjax()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var filmer = new List<Film>();
            var film = new Film()
            {
                id = 1,
                Navn = "Film01",
                Bilde = "bilde1",
                Beskrivelse = "Dette er en film",
                Gjennomsnitt = 3,
                Kontinent = "USA",
                Pris = 20,
                Studio = "Studio01",
                Produksjonsår = 1999,
                Visninger = 16,
                ReleaseDate = "12/12/2014",
                Sjanger = new List<Sjanger>(),
                Skuespillere = new List<Skuespiller>(),
                Kommentarer = new List<Kommentar>()
            };
            var skuespiller = new Skuespiller()
            {
                id = 1,
                Fornavn = "Per",
                Etternavn = "Persen",
                Bilde = "bilde02",
                Alder = 48,
                Land = "Norge"
            };
            var sjanger = new Sjanger()
            {
                id = 1,
                sjanger = "Action"
            };
            var kommentar = new Kommentar()
            {
                Dato = "12/12/2018",
                id = 1,
                Melding = "Dette er en kommentar"
            };
            film.Skuespillere.Add(skuespiller);
            film.Sjanger.Add(sjanger);
            film.Kommentarer.Add(kommentar);
            filmer.Add(film);
            filmer.Add(film);
            filmer.Add(film);
            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize(filmer);

            // Act
            var resultat = controller.HentFilmerForAjax();

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void LeggFilmISkuespillerOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("OK");

            // Act
            var resultat = controller.LeggFilmISkuespiller(1, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void LeggFilmISkuespillerFeilFilm()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.LeggFilmISkuespiller(1, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void LeggFilmISkuespillerFeilSkuespiller()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.LeggFilmISkuespiller(0, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void LeggFilmISkuespillerFeilFilmOgSkuespiller()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.LeggFilmISkuespiller(0, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettFilmFraSkuespillerOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("OK");

            // Act
            var resultat = controller.SlettFilmFraSkuespiller(1, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettFilmFraSkuespillerFeilSkuespiller()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettFilmFraSkuespiller(0, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettFilmFraSkuespillerFeilFilm()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettFilmFraSkuespiller(1, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettFilmFraSkuespillerFeilSkuespillerOgFilm()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettFilmFraSkuespiller(0, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettFilmFraBrukerOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("OK");

            // Act
            var resultat = controller.SlettFilmFraBruker(1, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettFilmFraBrukerFeilBruker()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettFilmFraBruker(0, 1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettFilmFraBrukerFeilFilm()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettFilmFraBruker(1, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettFilmFraBrukerFeilFilmOgBruker()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettFilmFraBruker(0, 0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettSkuespillerOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("OK");

            // Act
            var resultat = controller.SlettSkuespiller(1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettSkuespillerFeil()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettSkuespiller(0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettFilmOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("OK");

            // Act
            var resultat = controller.SlettFilm(1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettFilmFeil()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettFilm(0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettBrukerOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("OK");

            // Act
            var resultat = controller.SlettBruker(1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettBrukerFeil()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettBruker(0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettNyhetOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("OK");

            // Act
            var resultat = controller.SlettNyhet(1);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

        [TestMethod]
        public void SlettNyhetFeil()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            var jsonSerializer = new JavaScriptSerializer();
            var forventetResultat = jsonSerializer.Serialize("Feil");

            // Act
            var resultat = controller.SlettNyhet(0);

            // Assert
            Assert.AreEqual(forventetResultat, resultat);
        }

    }
}
