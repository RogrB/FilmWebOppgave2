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
    class LoginnTest
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

            // Act
            var actionResult = (RedirectToRouteResult)controller.Loginn(innAdmin);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
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

            // Act
            var actionResult = (ViewResult)controller.Loginn(innAdmin);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
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

            // Act
            var actionResult = (ViewResult)controller.Loginn(innAdmin);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
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

            // Act
            var actionResult = (ViewResult)controller.Loginn(innAdmin);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
    }
}
