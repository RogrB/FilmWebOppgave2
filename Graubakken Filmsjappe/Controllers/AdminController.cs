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
    public class AdminController : Controller
    {
        private IAdminLogikk _adminBLL;

        public AdminController()
        {
            _adminBLL = new AdminLogikk();
        }

        public AdminController(IAdminLogikk stub)
        {
            _adminBLL = stub;
        }

        public ActionResult Index()
        {
            if (AdminLoggetInn())
            {
                return RedirectToAction("Admin");
            }
            return RedirectToAction("AdminLoginn");
        }

        public ActionResult Admin()
        {
            if (AdminLoggetInn())
            {
                return View();
            }
            return RedirectToAction("AdminLoginn");
        }

        public bool AdminLoggetInn()
        {
            return Session["Admin"] != null && (string)Session["Admin"] != "";
        }

        public ActionResult AdminLoginn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLoginn(Administrator innAdmin)
        {
            if (_adminBLL.SjekkInnLogging(innAdmin))
            {
                Session["Admin"] = innAdmin.Brukernavn;
                return RedirectToAction("Admin");
            }
            else
            {
                Session["Admin"] = "";
                ViewBag.AdminLogin = "Feil brukernavn eller passord";
                return View();
            }
        }

        public ActionResult RegistrerAdmin()
        {
            var resultat = _adminBLL.RegistrerAdmin();
            if (resultat == "OK")
            {
                ViewBag.AdminResultat = "Admin opprettet";
            }
            else if(resultat == "Admin er allerede registrert")
            {
                ViewBag.AdminResultat = "Admin er allerede registrert";
            }
            else
            {
                ViewBag.AdminResultat = "Kunne ikke registrere admin";
            }
            return View();
        }

        public ActionResult Logut()
        {
            Session["Admin"] = "";

            return RedirectToAction("AdminLoginn");
        }

        public ActionResult RedigerFilmer()
        {
            if(AdminLoggetInn())
            {
                List<Film> alleFilmer = _adminBLL.HentFilmer();
                return View(alleFilmer);
            }
            return RedirectToAction("AdminLoginn");
        }

        public ActionResult RedigerFilm(int id = 0)
        {
            if(AdminLoggetInn())
            {
                Film film = _adminBLL.HentFilm(id);
                return View(film);
            }
            return RedirectToAction("AdminLoginn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedigerFilm(Film innFilm, HttpPostedFileBase bildeOpplasting)
        {
            if (ModelState.IsValid)
            {
                if (_adminBLL.RedigerFilm(innFilm, bildeOpplasting))
                {
                    ViewBag.EndreStatus = "Informasjon oppdatert";
                }
                else
                {
                    ViewBag.EndreStatus = "Klarte ikke å oppdatere informasjon";
                }
            }
            return View(_adminBLL.HentFilm(innFilm.id));
        }

        public ActionResult RedigerSkuespillere()
        {
            if(AdminLoggetInn())
            {
                var skuespillere = _adminBLL.HentSkuespillere();
                return View(skuespillere);
            }
            return RedirectToAction("AdminLoginn");
        }

        public ActionResult RedigerSkuespiller(int id = 0)
        {
            if (AdminLoggetInn())
            {
                var skuespiller = _adminBLL.HentSkuespiller(id);
                return View(skuespiller);
            }
            return RedirectToAction("AdminLoginn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedigerSkuespiller(Skuespiller innSkuespiller, HttpPostedFileBase bildeOpplasting)
        {
            if (ModelState.IsValid)
            {
                if (_adminBLL.RedigerSkuespiller(innSkuespiller, bildeOpplasting))
                {
                    ViewBag.EndreStatus = "Informasjon oppdatert";
                }
                else
                {
                    ViewBag.EndreStatus = "Klarte ikke å oppdatere informasjon";
                }
            }
            return View(_adminBLL.HentSkuespiller(innSkuespiller.id));
        }

        public ActionResult RedigerBrukere()
        {
            if(AdminLoggetInn())
            {
                var brukere = _adminBLL.HentKunder();
                return View(brukere);
            }
            return RedirectToAction("AdminLoginn");
        }

        public ActionResult RedigerBruker(int id = 0)
        {
            if(AdminLoggetInn())
            {
                var kunde = _adminBLL.HentKunde(id);
                return View(kunde);
            }
            return RedirectToAction("AdminLoginn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedigerBruker(EndreKunde innKunde)
        {
            if (ModelState.IsValid)
            {
                if (_adminBLL.RedigerKunde(innKunde))
                {
                    ViewBag.EndreStatus = "Informasjon oppdatert";
                }
                else
                {
                    ViewBag.EndreStatus = "Klarte ikke å oppdatere informasjon";
                }
            }
            return View(_adminBLL.HentKunde(innKunde.id));
        }

        public ActionResult OpprettSkuespiller()
        {
            if(AdminLoggetInn())
            {
                return View();
            }
            return RedirectToAction("AdminLoginn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OpprettSkuespiller(Skuespiller innSkuespiller, HttpPostedFileBase bildeOpplasting)
        {
            if (ModelState.IsValid)
            {
                if (_adminBLL.OpprettSkuespiller(innSkuespiller, bildeOpplasting))
                {
                    return RedirectToAction("RedigerSkuespillere");
                }
            }
            ViewBag.OpprettStatus = "Klarte ikke å opprette skuespiller";
            return View();
        }

        public ActionResult OpprettFilm()
        {
            if (AdminLoggetInn())
            {
                return View();
            }
            return RedirectToAction("AdminLoginn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OpprettFilm(Film innFilm, HttpPostedFileBase bildeOpplasting)
        {
            if (ModelState.IsValid)
            {
                if (_adminBLL.OpprettFilm(innFilm, bildeOpplasting))
                {
                    return RedirectToAction("RedigerFilmer");
                }
            }
            ViewBag.OpprettStatus = "Klarte ikke å opprette film";
            return View();
        }

        public ActionResult RedigerNyheter()
        {
            if(AdminLoggetInn())
            {
                var nyheter = _adminBLL.HentNyheter();
                return View(nyheter);
            }
            return RedirectToAction("AdminLoginn");
        }

        public ActionResult RedigerNyhet(int id = 0)
        {
            if (AdminLoggetInn())
            {
                var kunde = _adminBLL.HentNyhet(id);
                return View(kunde);
            }
            return RedirectToAction("AdminLoginn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedigerNyhet(Nyhet innNyhet)
        {
            if (ModelState.IsValid)
            {
                if (_adminBLL.RedigerNyhet(innNyhet))
                {
                    ViewBag.EndreStatus = "Informasjon oppdatert";
                }
                else
                {
                    ViewBag.EndreStatus = "Klarte ikke å oppdatere informasjon";
                }
            }
            return View(_adminBLL.HentNyhet(innNyhet.id));
        }

        public ActionResult OpprettNyhet()
        {
            if(AdminLoggetInn())
            {
                return View();
            }
            return RedirectToAction("AdminLoginn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OpprettNyhet(Nyhet innNyhet)
        {
            if (ModelState.IsValid)
            {
                if (_adminBLL.OpprettNyhet(innNyhet))
                {
                    return RedirectToAction("RedigerNyheter");
                }
            }
            ViewBag.OpprettStatus = "Klarte ikke å opprette nyhet";
            return View();
        }

        public ActionResult VisLogg()
        {
            if (AdminLoggetInn())
            {
                string[] logg = _adminBLL.HentLogg();
                return View(logg);
            }
            return RedirectToAction("AdminLoginn");
        }



        // AJAX 

        public string HentSkuespillere()
        {
            var jsonSerializer = new JavaScriptSerializer();
            var skuespillere = _adminBLL.HentSkuespillere();

            return jsonSerializer.Serialize(skuespillere);
        }

        public string LeggSkuespillerIFilm(int filmID, int skuespillerID)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var resultat = _adminBLL.LeggSkuespillerIFilm(filmID, skuespillerID);

            return jsonSerializer.Serialize(resultat);
        }

        public string SlettSkuespillerFraFilm(int filmID, int skuespillerID)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var resultat = _adminBLL.SlettSkuespillerFraFilm(filmID, skuespillerID);

            return jsonSerializer.Serialize(resultat);
        }

        public string HentSjangereForFilm(int id)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var resultat = _adminBLL.HentSjangereForFilm(id);

            return jsonSerializer.Serialize(resultat);
        }

        public string HentSjangere()
        {
            var jsonSerializer = new JavaScriptSerializer();
            var sjangere = _adminBLL.HentSjangere();

            return jsonSerializer.Serialize(sjangere);
        }

        public string LeggSjangerIFilm(int filmID, int sjangerID)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var resultat = _adminBLL.LeggSjangerIFilm(filmID, sjangerID);

            return jsonSerializer.Serialize(resultat);
        }

        public string SlettSjangerFraFilm(int filmID, int sjangerID)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var resultat = _adminBLL.SlettSjangerFraFilm(filmID, sjangerID);

            return jsonSerializer.Serialize(resultat);
        }

        public string HentFilmerForAjax()
        {
            var jsonSerializer = new JavaScriptSerializer();
            var filmer = _adminBLL.HentFilmerForAjax();

            return jsonSerializer.Serialize(filmer);
        }

        public string LeggFilmISkuespiller(int skuespillerID, int filmID)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var resultat = _adminBLL.LeggFilmISkuespiller(skuespillerID, filmID);

            return jsonSerializer.Serialize(resultat);
        }

        public string SlettFilmFraSkuespiller(int skuespillerID, int filmID)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var resultat = _adminBLL.SlettFilmFraSkuespiller(skuespillerID, filmID);

            return jsonSerializer.Serialize(resultat);
        }

        public string SlettFilmFraBruker(int brukerID, int filmID)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var resultat = _adminBLL.SlettFilmFraBruker(brukerID, filmID);

            return jsonSerializer.Serialize(resultat);
        }

        public string SlettSkuespiller(int id)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var resultat = _adminBLL.SlettSkuespiller(id);
            string output = "";
            if (resultat)
            {
                output = "OK";
            }
            else
            {
                output = "Feil";
            }

            return jsonSerializer.Serialize(output);
        }

        public string SlettFilm(int id)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var resultat = _adminBLL.SlettFilm(id);
            string output = "";
            if (resultat)
            {
                output = "OK";
            }
            else
            {
                output = "Feil";
            }

            return jsonSerializer.Serialize(output);
        }

        public string SlettBruker(int id)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var resultat = _adminBLL.SlettBruker(id);
            string output = "";
            if (resultat)
            {
                output = "OK";
            }
            else
            {
                output = "Feil";
            }

            return jsonSerializer.Serialize(output);
        }

        public string SlettNyhet(int id)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var resultat = _adminBLL.SlettNyhet(id);
            string output = "";
            if (resultat)
            {
                output = "OK";
            }
            else
            {
                output = "Feil";
            }

            return jsonSerializer.Serialize(output);
        }

    }
}
