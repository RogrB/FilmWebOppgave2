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
    public class LoginnTest
    {
        [TestMethod]
        public void AdminLoginnView()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            // Act
            var actionResult = (ViewResult)controller.AdminLoginn();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
        
        [TestMethod]
        public void LoginnOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var innAdmin = new Administrator()
            {
                Brukernavn = "admin",
                Passord = "admin"
            };
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);

            // Act
            var actionResult = (RedirectToRouteResult)controller.AdminLoginn(innAdmin);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Admin");
        }

        [TestMethod]
        public void LoginnFeilNavn()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var innAdmin = new Administrator()
            {
                Brukernavn = "feilNavn",
                Passord = "admin"
            };
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);

            // Act
            var actionResult = (ViewResult)controller.AdminLoginn(innAdmin);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["AdminLogin"], "Feil brukernavn eller passord");
        }

        [TestMethod]
        public void LoginnFeilPassord()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var innAdmin = new Administrator()
            {
                Brukernavn = "admin",
                Passord = "feilPassord"
            };
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);

            // Act
            var actionResult = (ViewResult)controller.AdminLoginn(innAdmin);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["AdminLogin"], "Feil brukernavn eller passord");
        }

        [TestMethod]
        public void LoginnFeilNavnOgPassord()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var innAdmin = new Administrator()
            {
                Brukernavn = "feilNavn",
                Passord = "feilPassord"
            };
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);

            // Act
            var actionResult = (ViewResult)controller.AdminLoginn(innAdmin);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(actionResult.ViewData["AdminLogin"], "Feil brukernavn eller passord");
        }

        [TestMethod]
        public void LoginnFeilModell()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var innAdmin = new Administrator();
            controller.ViewData.ModelState.AddModelError("Brukernavn", "Brukernavn må oppgis");
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);

            // Act
            var actionResult = (ViewResult)controller.AdminLoginn(innAdmin);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void AdminViewLoggetInnOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            // Act
            bool resultat = controller.AdminLoggetInn();

            // Assert
            Assert.AreEqual(resultat, true);
        }

        [TestMethod]
        public void AdminViewLoggetInnNull()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = null;

            // Act
            bool resultat = controller.AdminLoggetInn();

            // Assert
            Assert.AreEqual(resultat, false);
        }


        [TestMethod]
        public void AdminViewLoggetInnFeil()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "";

            // Act
            bool resultat = controller.AdminLoggetInn();

            // Assert
            Assert.AreEqual(resultat, false);
        }

        [TestMethod]
        public void AdminLogoutTest()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            // Act
            var actionResult = (RedirectToRouteResult)controller.Logut();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }
    }
}
