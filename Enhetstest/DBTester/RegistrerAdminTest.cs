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
            var actionResult = (ViewResult)controller.RegisterAdmin(1);
            
            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
    }
}
