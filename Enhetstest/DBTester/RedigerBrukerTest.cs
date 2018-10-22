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

namespace Enhetstest.DBTester
{
    [TestClass]
    public class RedigerBrukerTest
    {
        
        [TestMethod]
        public void RedigerBrukereViewOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";
            var forventetResultat = new List<Kunde>();
            var kunde = new Kunde()
            {
                id = 1,
                Fornavn = "Ole",
                Etternavn = "Olesen",
                Kort = 1234567890987654,
                Brukernavn = "ole0",
                Filmer = new List<Film>()
            };
            var film = new Film()
            {
                id = 1,
                Navn = "Film01",
                Bilde = "Bilde01",
                Beskrivelse = "Beskrivelse01",
                Kontinent = "USA"
            };
            kunde.Filmer.Add(film);

            forventetResultat.Add(kunde);
            forventetResultat.Add(kunde);
            forventetResultat.Add(kunde);

            // Act
            var actionResult = (ViewResult)controller.RedigerBrukere();
            var resultat = (List<Kunde>)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].id, resultat[i].id);
                Assert.AreEqual(forventetResultat[i].Fornavn, resultat[i].Fornavn);
                Assert.AreEqual(forventetResultat[i].Etternavn, resultat[i].Etternavn);
                Assert.AreEqual(forventetResultat[i].Kort, resultat[i].Kort);
                Assert.AreEqual(forventetResultat[i].Brukernavn, resultat[i].Brukernavn);
                Assert.AreEqual(forventetResultat[i].Filmer[0].id, resultat[i].Filmer[0].id);
                Assert.AreEqual(forventetResultat[i].Filmer[0].Navn, resultat[i].Filmer[0].Navn);
                Assert.AreEqual(forventetResultat[i].Filmer[0].Bilde, resultat[i].Filmer[0].Bilde);
            }
        }

        [TestMethod]
        public void RedigerBrukereViewNullSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = null;

            // Act
            var actionResult = (RedirectToRouteResult)controller.RedigerBrukere();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void RedigerBrukereViewBlankSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.RedigerBrukere();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void RedigerBrukerViewOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";
            var forventetResultat = new EndreKunde()
            {
                id = 1,
                Fornavn = "Ole",
                Etternavn = "Olesen",
                Kort = 1234567890987654,
                Brukernavn = "ole0",
                Filmer = new List<Film>()
            };
            var film = new Film()
            {
                id = 1,
                Navn = "Film01",
                Bilde = "Bilde01",
                Beskrivelse = "Beskrivelse01",
                Kontinent = "USA"
            };
            forventetResultat.Filmer.Add(film);


            // Act
            var actionResult = (ViewResult)controller.RedigerBruker(forventetResultat.id);
            var resultat = (EndreKunde)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            Assert.AreEqual(forventetResultat.id, resultat.id);
            Assert.AreEqual(forventetResultat.Fornavn, resultat.Fornavn);
            Assert.AreEqual(forventetResultat.Etternavn, resultat.Etternavn);
            Assert.AreEqual(forventetResultat.Kort, resultat.Kort);
            Assert.AreEqual(forventetResultat.Brukernavn, resultat.Brukernavn);
            Assert.AreEqual(forventetResultat.Filmer[0].id, resultat.Filmer[0].id);
            Assert.AreEqual(forventetResultat.Filmer[0].Navn, resultat.Filmer[0].Navn);
            Assert.AreEqual(forventetResultat.Filmer[0].Bilde, resultat.Filmer[0].Bilde);
        }

        [TestMethod]
        public void RedigerBrukerViewNullModel()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            // Act
            var actionResult = (ViewResult)controller.RedigerBruker(0);
            var KundeResultat = (EndreKunde)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(KundeResultat.id, 0);
        }

        [TestMethod]
        public void RedigerBrukerViewNullSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = null;

            // Act
            var actionResult = (RedirectToRouteResult)controller.RedigerBruker();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void RedigerBrukerViewBlankSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.RedigerBruker();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void RedigerBrukerIkkeFunnetIPost()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";
            var innKunde = new EndreKunde()
            {
                id = 1,
                Fornavn = "Ole",
                Etternavn = "Olesen",
                Kort = 1234567890987654,
                Brukernavn = "ole0",
                Filmer = new List<Film>()
            };
            var film = new Film()
            {
                id = 1,
                Navn = "Film01",
                Bilde = "Bilde01",
                Beskrivelse = "Beskrivelse01",
                Kontinent = "USA"
            };
            innKunde.Filmer.Add(film);

            // Act
            var actionResult = (ViewResult)controller.RedigerBruker(innKunde);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void RedigerSkuespillerFeilValideringIPost()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";
            var innKunde = new EndreKunde()
            {
                id = 1,
                Fornavn = "Ole",
                Etternavn = "Olesen",
                Kort = 1234567890987654,
                Brukernavn = "ole0",
                Filmer = new List<Film>()
            };
            var film = new Film()
            {
                id = 1,
                Navn = "Film01",
                Bilde = "Bilde01",
                Beskrivelse = "Beskrivelse01",
                Kontinent = "USA"
            };
            innKunde.Filmer.Add(film);
            controller.ViewData.ModelState.AddModelError("feil", "ID = 0");

            // Act
            var actionResult = (ViewResult)controller.RedigerBruker(innKunde);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "ID = 0");
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void RedigerBrukerPostOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";
            var innKunde = new EndreKunde()
            {
                id = 1,
                Fornavn = "Ole",
                Etternavn = "Olesen",
                Kort = 1234567890987654,
                Brukernavn = "ole0",
                Filmer = new List<Film>()
            };
            var film = new Film()
            {
                id = 1,
                Navn = "Film01",
                Bilde = "Bilde01",
                Beskrivelse = "Beskrivelse01",
                Kontinent = "USA"
            };
            innKunde.Filmer.Add(film);

            // Act
            var actionResult = (ViewResult)controller.RedigerBruker(innKunde);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

    }

}
