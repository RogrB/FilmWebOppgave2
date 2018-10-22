using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using DAL;
using Model;
using Graubakken_Filmsjappe.Controllers;
using System.Web.Mvc;
using MvcContrib.TestHelper;

namespace Enhetstest
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void AdminLoggetInnOK()
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
        public void AdminLoggetInnNull()
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
        public void AdminLoggetInnFeil()
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
        public void AdminViewOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            // Act
            var actionResult = (ViewResult)controller.Admin();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void AdminViewNull()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = null;

            // Act
            var actionResult = (RedirectToRouteResult)controller.Admin();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            //Assert.AreEqual(actionResult.RouteValues.Values, "AdminLoginn");
        }

        [TestMethod]
        public void AdminViewBlank()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.Admin();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            //Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void AdminIndexLoggetInn()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            // Act
            var actionResult = (RedirectToRouteResult)controller.Index();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            //Assert.AreEqual(actionResult.RouteValues.Values.First(), "Admin");
        }

        [TestMethod]
        public void AdminIndexBlank()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.Admin();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            //Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void AdminIndexNull()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = null;

            // Act
            var actionResult = (RedirectToRouteResult)controller.Admin();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            //Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        [TestMethod]
        public void AdminIndexViewOk()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            // Act
            var actionResult = (RedirectToRouteResult)controller.Index();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            //Assert.AreEqual(actionResult.RouteValues.Values.First(), "Admin");
        }

        [TestMethod]
        public void AdminIndexViewNull()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = null;

            // Act
            var actionResult = (RedirectToRouteResult)controller.Index();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            //Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }

        /*
        [TestMethod]
        public void AdminLogoutOK()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";

            // Act
            var actionResult = (RedirectToRouteResult)controller.Index();

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(controller.Session["Admin"], "");
            //Assert.AreEqual(actionResult.RouteValues.Values.First(), "AdminLoginn");
        }*/

    }
}
