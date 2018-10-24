﻿using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IAdminRepository
    {
        string RegistrerAdmin(int id);
        bool AdminEksisterer(int id);
        bool SjekkInnLogging(Administrator innAdmin);
        List<Film> HentFilmer();
        Film HentFilm(int id);
        List<Skuespiller> HentSkuespillere();
        string LeggSkuespillerIFilm(int filmID, int skuespillerID);
        string SlettSkuespillerFraFilm(int filmID, int skuespillerID);
        bool RedigerFilm(Film innFilm);
        List<Sjanger> HentSjangereForFilm(int id);
        List<Sjanger> HentSjangere();
        string LeggSjangerIFilm(int filmID, int sjangerID);
        string SlettSjangerFraFilm(int filmID, int sjangerID);
        Skuespiller HentSkuespiller(int id);
        List<Film> HentFilmerForAjax();
        string LeggFilmISkuespiller(int skuespillerID, int filmID);
        string SlettFilmFraSkuespiller(int skuespillerID, int filmID);
        bool RedigerSkuespiller(Skuespiller innSkuespiller);
        List<Kunde> HentKunder();
        EndreKunde HentKunde(int id);
        string SlettFilmFraBruker(int brukerID, int filmID);
        bool RedigerKunde(EndreKunde innKunde);
        bool OpprettSkuespiller(Skuespiller innSkuespiller);
        bool SlettSkuespiller(int id);
        bool OpprettFilm(Film innFilm);
        bool SlettFilm(int id);
        bool SlettBruker(int id);
    }
}
