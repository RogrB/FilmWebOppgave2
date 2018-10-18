using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IAdminLogikk
    {
        string RegistrerAdmin(int id);
        bool SjekkInnLogging(Administrator admin);
        List<Film> HentFilmer();
        Film HentFilm(int id);
        List<Skuespiller> HentSkuespillere();
        string LeggSkuespillerIFilm(int filmID, int skuespillerID);
        string SlettSkuespillerFraFilm(int filmID, int skuespillerID);
        bool RedigerFilm(Film innFilm);
    }
}
