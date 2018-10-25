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
using Enhetstest.MockFiles;

namespace Enhetstest.DBTester
{
    [TestClass]
    public class RedigerSkuespillerTest
    {
        [TestMethod]
        public void RedigerSkuespillereViewOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";
            var forventetResultat = new List<Skuespiller>();
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

            forventetResultat.Add(Skuespiller);
            forventetResultat.Add(Skuespiller);
            forventetResultat.Add(Skuespiller);

            // Act
            var actionResult = (ViewResult)controller.RedigerSkuespillere();
            var resultat = (List<Skuespiller>)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].id, resultat[i].id);
                Assert.AreEqual(forventetResultat[i].Fornavn, resultat[i].Fornavn);
                Assert.AreEqual(forventetResultat[i].Etternavn, resultat[i].Etternavn);
                Assert.AreEqual(forventetResultat[i].Land, resultat[i].Land);
                Assert.AreEqual(forventetResultat[i].Alder, resultat[i].Alder);
                Assert.AreEqual(forventetResultat[i].Bilde, resultat[i].Bilde);
                Assert.AreEqual(forventetResultat[i].Filmer[0].id, resultat[i].Filmer[0].id);
                Assert.AreEqual(forventetResultat[i].Filmer[0].Navn, resultat[i].Filmer[0].Navn);
                Assert.AreEqual(forventetResultat[i].Filmer[0].Bilde, resultat[i].Filmer[0].Bilde);
            }
        }

        [TestMethod]
        public void RedigerSkuespillereViewNullSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = null;

            // Act
            var actionResult = (RedirectToRouteResult)controller.RedigerSkuespillere();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void RedigerSkuespillereViewBlankSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.RedigerSkuespillere();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void RedigerSkuespillerViewOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";
            var forventetResultat = new Skuespiller()
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
            forventetResultat.Filmer.Add(Film);

            // Act
            var actionResult = (ViewResult)controller.RedigerSkuespiller(forventetResultat.id);
            var resultat = (Skuespiller)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            Assert.AreEqual(forventetResultat.id, resultat.id);
            Assert.AreEqual(forventetResultat.Fornavn, resultat.Fornavn);
            Assert.AreEqual(forventetResultat.Etternavn, resultat.Etternavn);
            Assert.AreEqual(forventetResultat.Land, resultat.Land);
            Assert.AreEqual(forventetResultat.Alder, resultat.Alder);
            Assert.AreEqual(forventetResultat.Bilde, resultat.Bilde);
            Assert.AreEqual(forventetResultat.Filmer[0].id, resultat.Filmer[0].id);
            Assert.AreEqual(forventetResultat.Filmer[0].Navn, resultat.Filmer[0].Navn);
            Assert.AreEqual(forventetResultat.Filmer[0].Bilde, resultat.Filmer[0].Bilde);
        }

        [TestMethod]
        public void RedigerSkuespillerViewNullModel()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            // Act
            var actionResult = (ViewResult)controller.RedigerSkuespiller(0);
            var SkuespillerResultat = (Skuespiller)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(SkuespillerResultat.id, 0);
        }

        public void RedigerSkuespillerViewNullSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = null;

            // Act
            var actionResult = (RedirectToRouteResult)controller.RedigerSkuespiller();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void RedigerSkuespillerViewBlankSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.RedigerSkuespiller();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void RedigerSkuespillerIkkeFunnetIPost()
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
            var actionResult = (ViewResult)controller.RedigerSkuespiller(innSkuespiller, testBilde);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["EndreStatus"], "Klarte ikke å oppdatere informasjon");
        }

        [TestMethod]
        public void RedigerSkuespillerFeilValideringIPost()
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
            var actionResult = (ViewResult)controller.RedigerSkuespiller(innSkuespiller, testBilde);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "ID = 0");
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void RedigerSkuespillerPostOK()
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
            var actionResult = (ViewResult)controller.RedigerSkuespiller(innSkuespiller, testBilde);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["EndreStatus"], "Informasjon oppdatert");
        }


    }
}
