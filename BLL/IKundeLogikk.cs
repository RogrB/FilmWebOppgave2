using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IKundeLogikk
    {

        IndexView HentIndexView();

        List<Film> HentFilmView(string sortering);

        List<Film> HentAlleFilmer();

        Skuespiller HentSkuespiller(int id);

        Film HentFilm(int id);

        bool StemPåFilm(int filmID, string brukernavn, int stemme);

        Stemme HarStemt(Film film, KundeDB bruker);

        bool OppdaterGjennomsnitt(int filmID);

        bool RegistrerBruker(Kunde innKunde);

        bool SjekkInnLogging(Kunde innKunde);

        EndreKunde HentBruker(string brukernavn);

        bool EndreBruker(EndreKunde innKunde, string brukernavn);

        Film HentFilmInfo(int id);

        Skuespiller HentSkuespillerInfo(int id);

        List<Film> HentActionFilmer();

        List<Skuespiller> HentAlleSkuespillere();

        List<Skuespiller> HentSkuespillerView(string sortering);

        bool OppdaterFilmVisningData(int id);

        bool LeggFilmIKundeObjekt(string innBruker, int filmID);

        List<Nyhet> HentAlleNyheter();

        List<Nyhet> HentIndexNyheter();

        List<Sjanger> HentAlleSjangere();

        List<Film> HentFilmerFraSkuespillerID(int id);

        List<Skuespiller> HentSkuespillereIFilm(int id);

        bool SjekkOmBrukernavnErLedig(string brukernavn);

        bool LeggIØnskeliste(int FilmID, string Brukernavn);

        bool SlettFraØnskeliste(int FilmID, string brukernavn);

        List<Søkeresultat> HentSøkeforslag(string input);

        bool SkrivKommentar(int id, string Melding, string Brukernavn);

        List<ForeslåttFilm> ForeslåFilm(string Brukernavn);


        /// Metoder som ikke er en del av løsningen, men har blitt brukt under utviklingen

        bool InsertDBData();

        List<Kunde> HentKunder();


    }
}
