using Model;
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
    }
}
