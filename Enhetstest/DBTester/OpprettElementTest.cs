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
using System.Web;
using Enhetstest.MockFiles;

namespace Enhetstest.DBTester
{
    [TestClass]
    public class OpprettElementTest
    {
        [TestMethod]
        public void OpprettSkuespillerViewOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            // Act
            var actionResult = (ViewResult)controller.OpprettSkuespiller();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void OpprettSkuespillerViewNullSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = null;

            // Act
            var actionResult = (RedirectToRouteResult)controller.OpprettSkuespiller();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void OpprettSkuespillerViewBlankSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.OpprettSkuespiller();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void OpprettSkuespillerFeilValideringIPost()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            var innSkuespiller = new Skuespiller()
            {
                id = 0,
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
            innSkuespiller.Filmer.Add(Film);
            controller.ViewData.ModelState.AddModelError("feil", "ID = 0");

            TestBilde testBilde = new TestBilde();

            // Act
            var actionResult = (ViewResult)controller.OpprettSkuespiller(innSkuespiller, testBilde);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "ID = 0");
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["OpprettStatus"], "Klarte ikke å opprette skuespiller");
        }

        [TestMethod]
        public void OpprettSkuespillerPostOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            var innSkuespiller = new Skuespiller()
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
            innSkuespiller.Filmer.Add(Film);

            TestBilde testBilde = new TestBilde();

            // Act
            var actionResult = (RedirectToRouteResult)controller.OpprettSkuespiller(innSkuespiller, testBilde);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "RedigerSkuespillere");
        }

        [TestMethod]
        public void OpprettSkuespillerFeilIDB()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            var innSkuespiller = new Skuespiller()
            {
                id = 0,
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
            innSkuespiller.Filmer.Add(Film);

            TestBilde testBilde = new TestBilde();

            // Act
            var actionResult = (ViewResult)controller.OpprettSkuespiller(innSkuespiller, testBilde);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["OpprettStatus"], "Klarte ikke å opprette skuespiller");
        }



        [TestMethod]
        public void OpprettFilmViewOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            // Act
            var actionResult = (ViewResult)controller.OpprettFilm();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void OpprettFilmViewNullSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = null;

            // Act
            var actionResult = (RedirectToRouteResult)controller.OpprettFilm();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void OpprettFilmViewBlankSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.OpprettFilm();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void OpprettFilmFeilValideringIPost()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            var innFilm = new Film()
            {
                id = 0,
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
            innFilm.Skuespillere.Add(skuespiller);
            innFilm.Sjanger.Add(sjanger);
            innFilm.Kommentarer.Add(kommentar);
            controller.ViewData.ModelState.AddModelError("feil", "ID = 0");

            // Act
            var actionResult = (ViewResult)controller.OpprettFilm(innFilm);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "ID = 0");
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["OpprettStatus"], "Klarte ikke å opprette film");
        }

        [TestMethod]
        public void OpprettFilmPostOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            var innFilm = new Film()
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
            innFilm.Skuespillere.Add(skuespiller);
            innFilm.Sjanger.Add(sjanger);
            innFilm.Kommentarer.Add(kommentar);

            // Act
            var actionResult = (RedirectToRouteResult)controller.OpprettFilm(innFilm);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "RedigerFilmer");
        }

        [TestMethod]
        public void OpprettFilmFeilIDB()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            var innFilm = new Film()
            {
                id = 0,
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
            innFilm.Skuespillere.Add(skuespiller);
            innFilm.Sjanger.Add(sjanger);
            innFilm.Kommentarer.Add(kommentar);

            // Act
            var actionResult = (ViewResult)controller.OpprettFilm(innFilm);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["OpprettStatus"], "Klarte ikke å opprette film");
        }

        [TestMethod]
        public void OpprettNyhetViewOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            // Act
            var actionResult = (ViewResult)controller.OpprettNyhet();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void OpprettNyhetViewNullSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = null;

            // Act
            var actionResult = (RedirectToRouteResult)controller.OpprettNyhet();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void OpprettNyhetViewBlankSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.OpprettNyhet();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void OpprettNyhetFeilValideringIPost()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            var innNyhet = new Nyhet()
            {
                id = 0,
                Tittel = "Nyhet01",
                Dato = "Dato01",
                Beskjed = "Beskjed01"
            };
            controller.ViewData.ModelState.AddModelError("feil", "ID = 0");

            // Act
            var actionResult = (ViewResult)controller.OpprettNyhet(innNyhet);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "ID = 0");
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["OpprettStatus"], "Klarte ikke å opprette nyhet");
        }

        [TestMethod]
        public void OpprettNyhetPostOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            var innNyhet = new Nyhet()
            {
                id = 1,
                Tittel = "Nyhet01",
                Dato = "Dato01",
                Beskjed = "Beskjed01"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.OpprettNyhet(innNyhet);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "RedigerNyheter");
        }

        [TestMethod]
        public void OpprettNyhetFeilIDB()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            var innNyhet = new Nyhet()
            {
                id = 0,
                Tittel = "Nyhet01",
                Dato = "Dato01",
                Beskjed = "Beskjed01"
            };

            // Act
            var actionResult = (ViewResult)controller.OpprettNyhet(innNyhet);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["OpprettStatus"], "Klarte ikke å opprette nyhet");
        }

    }

}
