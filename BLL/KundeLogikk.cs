using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class KundeLogikk : IKundeLogikk
    {
        private IKundeRepository _repository;

        public KundeLogikk()
        {
            _repository = new KundeRepository();
        }

        public KundeLogikk (IKundeRepository stub)
        {
            _repository = stub;
        }

        public IndexView HentIndexView()
        {
            IndexView view = _repository.HentIndexView();
            return view;
        }

        public List<Film> HentFilmView(string sortering)
        {
            List<Film> filmView = _repository.HentFilmView(sortering);
            return filmView;
        }

        public List<Film> HentAlleFilmer()
        {
            List<Film> alleFilmer = _repository.HentAlleFilmer();
            return alleFilmer;
        }

        public Skuespiller HentSkuespiller(int id)
        {
            Skuespiller skuespiller = _repository.HentSkuespiller(id);
            return skuespiller;
        }

        public Film HentFilm(int id)
        {
            Film film = _repository.HentFilm(id);
            return film;
        }

        public bool StemPåFilm(int filmID, string brukernavn, int stemme)
        {
            bool resultat = _repository.StemPåFilm(filmID, brukernavn, stemme);
            return resultat;
        }

        public Stemme HarStemt(Film film, KundeDB bruker)
        {
            Stemme stemme = _repository.HarStemt(film, bruker);
            return stemme;
        }

        public bool OppdaterGjennomsnitt(int filmID)
        {
            bool resultat = _repository.OppdaterGjennomsnitt(filmID);
            return resultat;
        }

        public bool RegistrerBruker(Kunde innKunde)
        {
            bool resultat = _repository.RegistrerBruker(innKunde);
            return resultat;
        }

        public bool SjekkInnLogging(Kunde innKunde)
        {
            bool innlogging = _repository.SjekkInnLogging(innKunde);
            return innlogging;
        }

        public EndreKunde HentBruker(string brukernavn)
        {
            EndreKunde kunde = _repository.HentBruker(brukernavn);
            return kunde;
        }

        public bool EndreBruker(EndreKunde innKunde, string brukernavn)
        {
            bool resultat = _repository.EndreBruker(innKunde, brukernavn);
            return resultat;
        }

        public Film HentFilmInfo(int id)
        {
            Film film = _repository.HentFilmInfo(id);
            return film;
        }

        public Skuespiller HentSkuespillerInfo(int id)
        {
            Skuespiller skuespiller = _repository.HentSkuespillerInfo(id);
            return skuespiller;
        }

        public List<Film> HentActionFilmer()
        {
            List<Film> actionFilmer = _repository.HentActionFilmer();
            return actionFilmer;
        }

        public List<Skuespiller> HentAlleSkuespillere()
        {
            List<Skuespiller> alleSkuespillere = _repository.HentAlleSkuespillere();
            return alleSkuespillere;
        }

        public List<Skuespiller> HentSkuespillerView(string sortering)
        {
            List<Skuespiller> view = _repository.HentSkuespillerView(sortering);
            return view;
        }

        public bool OppdaterFilmVisningData(int id)
        {
            bool resultat = _repository.OppdaterFilmVisningData(id);
            return resultat;
        }

        public bool LeggFilmIKundeObjekt(string innBruker, int filmID)
        {
            bool resultat = _repository.LeggFilmIKundeObjekt(innBruker, filmID);
            return resultat;
        }

        public List<Nyhet> HentAlleNyheter()
        {
            List<Nyhet> alleNyheter = _repository.HentAlleNyheter();
            return alleNyheter;
        }

        public List<Nyhet> HentIndexNyheter()
        {
            List<Nyhet> nyheter = _repository.HentIndexNyheter();
            return nyheter;
        }

        public List<Sjanger> HentAlleSjangere()
        {
            List<Sjanger> alleSjangere = _repository.HentAlleSjangere();
            return alleSjangere;
        }

        public List<Film> HentFilmerFraSkuespillerID(int id)
        {
            List<Film> alleFilmer = _repository.HentFilmerFraSkuespillerID(id);
            return alleFilmer;
        }

        public List<Skuespiller> HentSkuespillereIFilm(int id)
        {
            List<Skuespiller> skuespillere = _repository.HentSkuespillereIFilm(id);
            return skuespillere;
        }

        public bool SjekkOmBrukernavnErLedig(string brukernavn)
        {
            bool resultat = _repository.SjekkOmBrukernavnErLedig(brukernavn);
            return resultat;
        }

        public bool LeggIØnskeliste(int FilmID, string Brukernavn)
        {
            bool resultat = _repository.LeggIØnskeliste(FilmID, Brukernavn);
            return resultat;
        }

        public bool SlettFraØnskeliste(int FilmID, string brukernavn)
        {
            bool resultat = _repository.SlettFraØnskeliste(FilmID, brukernavn);
            return resultat;
        }

        public List<Søkeresultat> HentSøkeforslag(string input)
        {
            List<Søkeresultat> forslag = _repository.HentSøkeforslag(input);
            return forslag;
        }

        public bool SkrivKommentar(int id, string Melding, string Brukernavn)
        {
            bool resultat = _repository.SkrivKommentar(id, Melding, Brukernavn);
            return resultat;
        }

        public List<ForeslåttFilm> ForeslåFilm(string Brukernavn)
        {
            List<ForeslåttFilm> film = _repository.ForeslåFilm(Brukernavn);
            return film;
        }

        public bool InsertDBData()
        {
            bool resultat = _repository.InsertDBData();
            return resultat;
        }

        public List<Kunde> HentKunder()
        {
            List<Kunde> kunder = _repository.HentKunder();
            return kunder;
        }

    }
}


//