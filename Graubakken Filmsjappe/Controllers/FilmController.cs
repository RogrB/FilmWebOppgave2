using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Graubakken_Filmsjappe.Controllers
{
    public class FilmController : Controller
    {
        private IKundeLogikk _kundeBLL;

        public FilmController()
        {
            _kundeBLL = new KundeLogikk();
        }

        public FilmController(IKundeLogikk stub)
        {
            _kundeBLL = stub;
        }

        /// Metoder for views
        public ActionResult Index()
        {
            IndexView indexView = _kundeBLL.HentIndexView();

            return View(indexView);
        }

        public ActionResult Skuespillere(string sortering)
        {
            if (string.IsNullOrEmpty(sortering))
            {
                sortering = "Fornavn";
                ViewBag.SkuespillerSortering = "Fornavn";
            }
            else
            {
                ViewBag.SkuespillerSortering = sortering;
            }
            List<Skuespiller> alleSkuespillere = _kundeBLL.HentSkuespillerView(sortering);
            return View(alleSkuespillere);
        }

        public ActionResult Nyheter()
        {
            List<Nyhet> alleNyheter = _kundeBLL.HentAlleNyheter();
            return View(alleNyheter);
        }

        public ActionResult Filmer(string sortering)
        {
            if (string.IsNullOrEmpty(sortering))
            {
                sortering = "Alfabetisk";
                ViewBag.Sortering = "Alfabetisk";
            }
            else
            {
                ViewBag.Sortering = sortering;
            }
            List<Film> alleFilmer = _kundeBLL.HentFilmView(sortering);
            return View(alleFilmer);
        }

        public ActionResult Sjangere()
        {
            List<Sjanger> alleSjangere = _kundeBLL.HentAlleSjangere();
            return View(alleSjangere);
        }

        public ActionResult Registrer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrer(Kunde innKunde)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.RegistreringsStatus = "Skjemaet er ikke fylt inn riktig";
                return View();
            }
            if (!_kundeBLL.SjekkOmBrukernavnErLedig(innKunde.Brukernavn))
            {
                ViewBag.RegistreringsStatus = "Brukernavnet er opptatt";
                return View();
            }
            if(_kundeBLL.RegistrerBruker(innKunde))
            {
                Session["LoggetInn"] = true;
                Session["Brukernavn"] = innKunde.Brukernavn;
                ViewBag.Innlogget = true;
                return RedirectToAction("Velkommen");
            }
            else
            {
                ViewBag.RegistreringsStatus = "Feil under registrering";
                return View();
            }
        }

        public ActionResult Velkommen()
        {
            if (Session["LoggetInn"] != null)
            {
                if ((bool)Session["LoggetInn"])
                {
                    ViewBag.Brukernavn = Session["Brukernavn"];
                    return View();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Loginn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Loginn(Kunde innKunde)
        {
            if(_kundeBLL.SjekkInnLogging(innKunde))
            {
                Session["LoggetInn"] = true;
                Session["Brukernavn"] = innKunde.Brukernavn;
                ViewBag.Brukernavn = innKunde.Brukernavn;
                return RedirectToAction("Index");
            }
            else
            {
                Session["LoggetInn"] = false;
                Session["Brukernavn"] = "";
                ViewBag.Brukernavn = "";
                return View();
            }
        }

        public ActionResult Logut()
        {
            Session["LoggetInn"] = false;
            Session["Brukernavn"] = "";
            ViewBag.Brukernavn = "";

            return RedirectToAction("Index");
        }

        public ActionResult Bruker(string brukernavn)
        {
            if (Session["LoggetInn"] != null)
            {
                if ((bool)Session["LoggetInn"])
                {
                    var bruker = _kundeBLL.HentBruker(brukernavn);

                    return View(bruker);
                }
            }
            return RedirectToAction("Loginn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bruker(EndreKunde innKunde)
        {
            if (ModelState.IsValid)
            {
                if (_kundeBLL.EndreBruker(innKunde, (string)Session["Brukernavn"]))
                {
                    ViewBag.EndreStatus = "Informasjon oppdatert";
                }
                else
                {
                    ViewBag.EndreStatus = "Klarte ikke å oppdatere informasjon";
                }
            }
            return View(_kundeBLL.HentBruker((string)Session["Brukernavn"]));
        }

        public ActionResult Film(int id = 0)
        {
            var filmInfo = _kundeBLL.HentFilmInfo(id);

            return View(filmInfo);
        }

        public ActionResult Skuespiller(int id = 0)
        {
            var skuespillerInfo = _kundeBLL.HentSkuespillerInfo(id);

            return View(skuespillerInfo);
        }

        public ActionResult FilmVisning(int id = 0)
        {
            if (Session["LoggetInn"] != null && Session["Brukernavn"] != null)
            {
                if ((bool)Session["LoggetInn"])
                {
                    if (!_kundeBLL.OppdaterFilmVisningData(id) || !_kundeBLL.LeggFilmIKundeObjekt((string)Session["Brukernavn"], id))
                    {
                        ViewBag.VisningsInfo = "En databasefeil har oppstått";
                    }
                    var film = _kundeBLL.HentFilmInfo(id);
                    return View(film);
                }
            }
            return RedirectToAction("Loginn");
        }

        /// Metoder for ajax-kall
        public string HentEnFilm(int id)
        {
            Film enFilm = _kundeBLL.HentFilm(id);
            var utFilm = new Film()
            {
                id = enFilm.id,
                Navn = enFilm.Navn,
                Beskrivelse = enFilm.Beskrivelse,
                Bilde = enFilm.Bilde,
                Produksjonsår = enFilm.Produksjonsår,
                Kontinent = enFilm.Kontinent,
                ReleaseDate = enFilm.ReleaseDate,
                Studio = enFilm.Studio,
                Visninger = enFilm.Visninger,
                Sjanger = new List<Sjanger>(),
                Stemmer = new List<Stemme>(),
                Skuespillere = new List<Skuespiller>()
            };
            
            var jsonSerializer = new JavaScriptSerializer();
            string jsonData = jsonSerializer.Serialize(utFilm);

            return jsonData;
        }

        // Lar en bruker gi en "terningkast" stemme på en angitt film
        public string StemPåFilm(int FilmID, int stemme)
        {
            string resultat = "";
            var jsonSerializer = new JavaScriptSerializer();
            if (Session["Brukernavn"] != null && (string)Session["Brukernavn"] != "")
            {
                if (_kundeBLL.StemPåFilm(FilmID, (string)Session["Brukernavn"], stemme))
                {
                    resultat = "OK";
                }
                else
                {
                    resultat = "Feil";
                }
            }
            else
            {
                resultat = "Feil innlogging";
            }
            return jsonSerializer.Serialize(resultat);
        }

        // Henter alle filmer en angitt skuespiller har vært med i
        public string HentFilmerFraSkuespillerID(int id)
        {
            List<Film> filmer = _kundeBLL.HentFilmerFraSkuespillerID(id);

            var jsonSerializer = new JavaScriptSerializer();
            string jsonData = jsonSerializer.Serialize(filmer);

            return jsonData;
        }

        // Henter alle skuespillere i en angitt film
        public string HentSkuespillereIFilm(int id)
        {
            List<Skuespiller> skuespillere = _kundeBLL.HentSkuespillereIFilm(id);

            var jsonSerializer = new JavaScriptSerializer();
            string jsonData = jsonSerializer.Serialize(skuespillere);

            return jsonData;
        }

        public string SjekkOmBrukernavnErLedig(string brukernavn)
        {
            var jsonSerializer = new JavaScriptSerializer();
            string resultat = "";
            if (_kundeBLL.SjekkOmBrukernavnErLedig(brukernavn))
            {
                resultat = "Brukernavn Ledig";
            }
            else
            {
                resultat = "Brukernavn opptatt";
            }
            return jsonSerializer.Serialize(resultat);
        }

        public string LeggIØnskeliste(int FilmID)
        {
            var jsonSerializer = new JavaScriptSerializer();
            string resultat = "";
            if (Session["Brukernavn"] != null && (string)Session["Brukernavn"] != "")
            {

                if (_kundeBLL.LeggIØnskeliste(FilmID, (string)Session["Brukernavn"]))
                {
                    resultat = "OK";
                }
                else
                {
                    resultat = "Feil";
                }
            }
            else
            {
                resultat = "Feil innlogging";
            }
            return jsonSerializer.Serialize(resultat);
        }

        public string SlettFraØnskeliste(int id)
        {
            string resultat = "";
            var jsonSerializer = new JavaScriptSerializer();
            if (_kundeBLL.SlettFraØnskeliste(id, (string)Session["Brukernavn"]))
            {
                resultat = "OK";
            }
            else
            {
                resultat = "Feil";
            }
            return jsonSerializer.Serialize(resultat);
        }

        // Henter en liste av filmer og skuespillere som søkeforslag når en bruker skriver inn i søkefeltet
        public string HentSøkeforslag(string streng)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var søkeforslag = _kundeBLL.HentSøkeforslag(streng);

            return jsonSerializer.Serialize(søkeforslag);
        }

        // Lar en bruker skrive en kommentar på en angitt film
        public string SkrivKommentar(int id, string Melding)
        {
            string resultat = "";
            var jsonSerializer = new JavaScriptSerializer();
            if (Session["Brukernavn"] != null && (string)Session["Brukernavn"] != "")
            {
                if (_kundeBLL.SkrivKommentar(id, Melding, (string)Session["Brukernavn"]))
                {
                    resultat = "OK";
                }
                else
                {
                    resultat = "Feil";
                }
            }
            else
            {
                resultat = "Feil innlogging";
            }
            return jsonSerializer.Serialize(resultat);
        }

        // Henter 3 filmer fra en tilfeldig sjanger som en kunde har sett tidligere
        public string ForeslåFilm()
        {
            var jsonSerializer = new JavaScriptSerializer();
            if (Session["Brukernavn"] != null && (string)Session["Brukernavn"] != "")
            {
                var Filmer = _kundeBLL.ForeslåFilm((string)Session["Brukernavn"]);
                if (Filmer != null)
                {
                    return jsonSerializer.Serialize(Filmer);
                }
                else
                {
                    return jsonSerializer.Serialize("Feil");
                }
            }
            else
            {
                return jsonSerializer.Serialize("Feil innlogging");
            }
        }



        /// Metoder som ikke er del av løsningen, men har blitt brukt under utvikling

        // Metode som mater data inn i DB fra DBData mappen - brukes som erstatning for DBInit
        public ActionResult Dbinsert()
        {
            if (_kundeBLL.InsertDBData())
            {
                ViewBag.Message = "DB insert OK";
            }
            else
            {
                ViewBag.Message = "DB insert feilet";
            }
            return View();
        }

        // Viser Kundeviewet - alle registrerte brukere
        public ActionResult Kunder()
        {
            var kunder = _kundeBLL.HentKunder();
            return View(kunder);
        }

    }
}