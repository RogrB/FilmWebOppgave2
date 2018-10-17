using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using DAL;
using Model;
using Graubakken_Filmsjappe.Controllers;
using System.Web.Mvc;
using MvcContrib.TestHelper;

namespace Enhetstest.DBTester
{
    [TestClass]
    public class RegistrerAdminTest
    {
        [TestMethod]
        public void RegistrerAdminOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            // Act
            var actionResult = (ViewResult)controller.RegistrerAdmin(1);
            
            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void RegistrerAdminFeil()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            // Act
            var actionResult = (ViewResult)controller.RegistrerAdmin(3);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void RegistrerAdminAlleredeRegistrert()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));

            // Act
            var actionResult = (ViewResult)controller.RegistrerAdmin(2);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
    }
}
