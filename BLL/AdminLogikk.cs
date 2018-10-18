﻿using DAL;
using Model;
using System;
using System.Collections.Generic;

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
    }
}
