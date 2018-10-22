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
    public class OpprettTest
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
            var actionResult = (RedirectToRouteResult)controller.RedigerFilmer();

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

            // Act
            var actionResult = (ViewResult)controller.OpprettSkuespiller(innSkuespiller);

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

            // Act
            var actionResult = (RedirectToRouteResult)controller.OpprettSkuespiller(innSkuespiller);

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

            // Act
            var actionResult = (ViewResult)controller.OpprettSkuespiller(innSkuespiller);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["OpprettStatus"], "Klarte ikke å opprette skuespiller");
        }

    }

}
