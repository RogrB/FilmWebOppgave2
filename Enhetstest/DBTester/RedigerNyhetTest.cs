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
    public class RedigerNyhetTest
    {

        [TestMethod]
        public void RedigerNyheterViewOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            List<Nyhet> forventetResultat = new List<Nyhet>();
            Nyhet enNyhet = new Nyhet()
            {
                id = 1,
                Tittel = "Nyhet01",
                Dato = "Dato01",
                Beskjed = "Dette er en nyhet"
            };

            forventetResultat.Add(enNyhet);
            forventetResultat.Add(enNyhet);
            forventetResultat.Add(enNyhet);

            // Act
            var actionResult = (ViewResult)controller.RedigerNyheter();
            var resultat = (List<Nyhet>)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].id, resultat[i].id);
                Assert.AreEqual(forventetResultat[i].Tittel, resultat[i].Tittel);
                Assert.AreEqual(forventetResultat[i].Dato, resultat[i].Dato);
                Assert.AreEqual(forventetResultat[i].Beskjed, resultat[i].Beskjed);
            }
        }

        [TestMethod]
        public void RedigerNyheterViewNullSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = null;

            // Act
            var actionResult = (RedirectToRouteResult)controller.RedigerNyheter();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void RedigerNyheterViewBlankSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.RedigerNyheter();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void RedigerNyhetViewOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            Nyhet forventetResultat = new Nyhet()
            {
                id = 1,
                Tittel = "Nyhet01",
                Dato = "Dato01",
                Beskjed = "Dette er en nyhet"
            };


            // Act
            var actionResult = (ViewResult)controller.RedigerNyhet(forventetResultat.id);
            var resultat = (Nyhet)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");

            Assert.AreEqual(forventetResultat.id, resultat.id);
            Assert.AreEqual(forventetResultat.Tittel, resultat.Tittel);
            Assert.AreEqual(forventetResultat.Dato, resultat.Dato);
            Assert.AreEqual(forventetResultat.Beskjed, resultat.Beskjed);
        }

        [TestMethod]
        public void RedigerNyhetViewNullModel()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            // Act
            var actionResult = (ViewResult)controller.RedigerNyhet(0);
            var NyhetsResultat = (Nyhet)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(NyhetsResultat.id, 0);
        }

        [TestMethod]
        public void RedigerNyhetViewNullSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = null;

            // Act
            var actionResult = (RedirectToRouteResult)controller.RedigerNyhet();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void RedigerNyhetViewBlankSession()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.RedigerNyhet();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void RedigerNyhetIkkeFunnetIPost()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            Nyhet innNyhet = new Nyhet()
            {
                id = 0,
                Tittel = "Nyhet01",
                Dato = "Dato01",
                Beskjed = "Dette er en nyhet"
            };


            // Act
            var actionResult = (ViewResult)controller.RedigerNyhet(innNyhet);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["EndreStatus"], "Klarte ikke å oppdatere informasjon");
        }

        [TestMethod]
        public void RedigerNyhetFeilValideringIPost()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            Nyhet innNyhet = new Nyhet()
            {
                id = 1,
                Tittel = "Nyhet01",
                Dato = "Dato01",
                Beskjed = "Dette er en nyhet"
            };

            controller.ViewData.ModelState.AddModelError("feil", "ID = 0");

            // Act
            var actionResult = (ViewResult)controller.RedigerNyhet(innNyhet);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "ID = 0");
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void RedigerNyhetPostOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            Nyhet innNyhet = new Nyhet()
            {
                id = 1,
                Tittel = "Nyhet01",
                Dato = "Dato01",
                Beskjed = "Dette er en nyhet"
            };

            // Act
            var actionResult = (ViewResult)controller.RedigerNyhet(innNyhet);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["EndreStatus"], "Informasjon oppdatert");
        }

    }

}
