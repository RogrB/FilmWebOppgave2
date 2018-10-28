using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Web;

namespace BLL
{
    public class AdminLogikk : IAdminLogikk
    {
        private IAdminRepository _repository;
        public AdminLogikk()
        {
            _repository = new AdminRepository();
        }

        public AdminLogikk(IAdminRepository stub)
        {
            _repository = stub;
        }

        public string RegistrerAdmin(int id)
        {
            string resultat = _repository.RegistrerAdmin(id);
            return resultat;
        }

        public bool SjekkInnLogging(Administrator innAdmin)
        {
            bool resultat = _repository.SjekkInnLogging(innAdmin);
            return resultat;
        }

        public List<Film> HentFilmer()
        {
            List<Film> alleFilmer = _repository.HentFilmer();
            return alleFilmer;
        }

        public Film HentFilm(int id)
        {
            Film film = _repository.HentFilm(id);
            return film;
        }

        public List<Skuespiller> HentSkuespillere()
        {
            List<Skuespiller> alleSkuespillere = _repository.HentSkuespillere();
            return alleSkuespillere;
        }

        public string LeggSkuespillerIFilm(int filmID, int skuespillerID)
        {
            string resultat = _repository.LeggSkuespillerIFilm(filmID, skuespillerID);
            return resultat;
        }

        public string SlettSkuespillerFraFilm(int filmID, int skuespillerID)
        {
            string resultat = _repository.SlettSkuespillerFraFilm(filmID, skuespillerID);
            return resultat;
        }

        public bool RedigerFilm(Film innFilm, HttpPostedFileBase bilde)
        {
            bool resultat = _repository.RedigerFilm(innFilm, bilde);
            return resultat;
        }

        public List<Sjanger> HentSjangereForFilm(int id)
        {
            List<Sjanger> sjangere = _repository.HentSjangereForFilm(id);
            return sjangere;
        }

        public List<Sjanger> HentSjangere()
        {
            List<Sjanger> sjangere = _repository.HentSjangere();
            return sjangere;
        }

        public string LeggSjangerIFilm(int filmID, int sjangerID)
        {
            string resultat = _repository.LeggSjangerIFilm(filmID, sjangerID);
            return resultat;
        }

        public string SlettSjangerFraFilm(int filmID, int sjangerID)
        {
            string resultat = _repository.SlettSjangerFraFilm(filmID, sjangerID);
            return resultat;
        }

        public Skuespiller HentSkuespiller(int id)
        {
            Skuespiller skuespiller = _repository.HentSkuespiller(id);
            return skuespiller;
        }

        public List<Film> HentFilmerForAjax()
        {
            List<Film> ajaxFilmer = _repository.HentFilmerForAjax();
            return ajaxFilmer;
        }

        public string LeggFilmISkuespiller(int skuespillerID, int filmID)
        {
            string resultat = _repository.LeggFilmISkuespiller(skuespillerID, filmID);
            return resultat;
        }

        public string SlettFilmFraSkuespiller(int skuespillerID, int filmID)
        {
            string resultat = _repository.SlettFilmFraSkuespiller(skuespillerID, filmID);
            return resultat;
        }

        public bool RedigerSkuespiller(Skuespiller innSkuespiller, HttpPostedFileBase bilde)
        {
            bool resultat = _repository.RedigerSkuespiller(innSkuespiller, bilde);
            return resultat;
        }

        public List<Kunde> HentKunder()
        {
            List<Kunde> kunder = _repository.HentKunder();
            return kunder;
        }

        public EndreKunde HentKunde(int id)
        {
            EndreKunde kunde = _repository.HentKunde(id);
            return kunde;
        }

        public string SlettFilmFraBruker(int brukerID, int filmID)
        {
            string resultat = _repository.SlettFilmFraBruker(brukerID, filmID);
            return resultat;
        }

        public bool RedigerKunde(EndreKunde innKunde)
        {
            bool resultat = _repository.RedigerKunde(innKunde);
            return resultat;
        }

        public bool OpprettSkuespiller(Skuespiller innSkuespiller, HttpPostedFileBase bilde)
        {
            bool resultat = _repository.OpprettSkuespiller(innSkuespiller, bilde);
            return resultat;
        }

        public bool SlettSkuespiller(int id)
        {
            bool resultat = _repository.SlettSkuespiller(id);
            return resultat;
        }

        public bool OpprettFilm(Film innFilm, HttpPostedFileBase bilde)
        {
            bool resultat = _repository.OpprettFilm(innFilm, bilde);
            return resultat;
        }

        public bool SlettFilm(int id)
        {
            bool resultat = _repository.SlettFilm(id);
            return resultat;
        }

        public bool SlettBruker(int id)
        {
            bool resultat = _repository.SlettBruker(id);
            return resultat;
        }

        public List<Nyhet> HentNyheter()
        {
            List<Nyhet> nyheter = _repository.HentNyheter();
            return nyheter;
        }

        public Nyhet HentNyhet(int id)
        {
            Nyhet nyhet = _repository.HentNyhet(id);
            return nyhet;
        }

        public bool RedigerNyhet(Nyhet innNyhet)
        {
            bool resultat = _repository.RedigerNyhet(innNyhet);
            return resultat;
        }

        public bool OpprettNyhet(Nyhet innNyhet)
        {
            bool resultat = _repository.OpprettNyhet(innNyhet);
            return resultat;
        }

        public bool SlettNyhet(int id)
        {
            bool resultat = _repository.SlettNyhet(id);
            return resultat;
        }

        public string[] HentLogg()
        {
            string[] logg = _repository.HentLogg();
            return logg;
        }
    }
}
