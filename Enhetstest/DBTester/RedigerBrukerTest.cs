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
                Kontinent = "USa"
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
    }
}
