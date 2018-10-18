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
    public class RedigerFilmTest
    {
        [TestMethod]
        public void RedigerFilmerView()
        {
            // Arrange
            var controller = new AdminController(new AdminLogikk(new AdminRepositoryStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["Admin"] = "admin";
            var forventetResultat = new List<Film>();
            var film = new Film()
            {
                id = 1,
                Navn = "Film01",
                Bilde = "bilde1",
                Beskrivelse = "Dette er en film",
                Gjennomsnitt = 3,
                Kontinent = "USA",
                Pris = 20,
                Studio = "Studio01",
                Produksjonsår = 1999,
                Visninger = 16,
                ReleaseDate = "12/12/2014",
                Sjanger = new List<Sjanger>(),
                Skuespillere = new List<Skuespiller>(),
                Kommentarer = new List<Kommentar>()
            };
            var skuespiller = new Skuespiller()
            {
                id = 1,
                Fornavn = "Per",
                Etternavn = "Persen",
                Bilde = "bilde02",
                Alder = 48,
                Land = "Norge"
            };
            var sjanger = new Sjanger()
            {
                id = 1,
                sjanger = "Action"
            };
            var kommentar = new Kommentar()
            {
                Dato = "12/12/2018",
                id = 1,
                Melding = "Dette er en kommentar"
            };
            film.Skuespillere.Add(skuespiller);
            film.Sjanger.Add(sjanger);
            film.Kommentarer.Add(kommentar);

            forventetResultat.Add(film);
            forventetResultat.Add(film);
            forventetResultat.Add(film);

            // Act
            var actionResult = (ViewResult)controller.RedigerFilmer();
            var resultat = (List<Film>)actionResult.Model;
            // Assert

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].id, resultat[i].id);
                Assert.AreEqual(forventetResultat[i].Navn, resultat[i].Navn);
                Assert.AreEqual(forventetResultat[i].Gjennomsnitt, resultat[i].Gjennomsnitt);
                Assert.AreEqual(forventetResultat[i].Bilde, resultat[i].Bilde);
                Assert.AreEqual(forventetResultat[i].Studio, resultat[i].Studio);
                Assert.AreEqual(forventetResultat[i].Pris, resultat[i].Pris);
                Assert.AreEqual(forventetResultat[i].Produksjonsår, resultat[i].Produksjonsår);
                Assert.AreEqual(forventetResultat[i].Beskrivelse, resultat[i].Beskrivelse);
                Assert.AreEqual(forventetResultat[i].Kontinent, resultat[i].Kontinent);
                Assert.AreEqual(forventetResultat[i].Visninger, resultat[i].Visninger);
                Assert.AreEqual(forventetResultat[i].ReleaseDate, resultat[i].ReleaseDate);
                Assert.AreEqual(forventetResultat[i].Skuespillere[0].Fornavn, resultat[i].Skuespillere[0].Fornavn);
                Assert.AreEqual(forventetResultat[i].Sjanger[0].sjanger, resultat[i].Sjanger[0].sjanger);
                Assert.AreEqual(forventetResultat[i].Kommentarer[0].Melding, resultat[i].Kommentarer[0].Melding);
            }
        }
    }
}
