using DAL.DBModels;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace DAL
{
    public class AdminRepository : IAdminRepository
    {
        // Registrerer en adminkonto med forhåndsbestemt innloggingsnavn og passord
        public string RegistrerAdmin()
        {
            string resultat = "";
            if (AdminEksisterer())
            {
                return "Admin er allerede registrert";
            }
            using (var db = new DBContext())
            {
                try
                {
                    string salt = LagSalt();
                    AdminDB admin = new AdminDB() 
                    {
                        Brukernavn = "admin",
                        Salt = salt,
                        Passord = LagHash("admin" + salt),
                    };
                    db.Administrator.Add(admin);
                    db.SaveChanges();
                    resultat = "OK";

                    LoggSkriver logg = new LoggSkriver();
                    string kommentar = "Admin registrert med forhåndsdefinert innloggingsnavn og passord";
                    logg.GenerellLogg("RegistrerAdmin", kommentar);
                }
                catch (Exception e)
                {
                    resultat = "Feil";
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("RegistrerAdmin", e);
                }
            }
            return resultat;
        }

        // Metode som sjekker om adminkontoen allerede er registrert.
        public bool AdminEksisterer()
        {
            bool resultat = false;
            using (var db = new DBContext())
            {
                var admin = db.Administrator.FirstOrDefault(a => a.Brukernavn == "admin");
                if (admin != null)
                {
                    resultat = true;
                }
            }

            return resultat;
        }

        /*
         Metode for å Generere random salt
         Skrevet av Faglærer i webapplikasjoner ved Oslomet
         */
        private static string LagSalt()
        {
            byte[] randomArray = new byte[10];
            string randomString;

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomArray);
            randomString = Convert.ToBase64String(randomArray);
            return randomString;
        }

        /*
        Metode for å Generere passordhash
        Skrevet av Faglærer i webapplikasjoner ved Oslomet
        */
        private static byte[] LagHash(string innStreng)
        {
            byte[] innData, utData;
            var algoritme = SHA256.Create();
            innData = System.Text.Encoding.UTF8.GetBytes(innStreng);
            utData = algoritme.ComputeHash(innData);
            return utData;
        }

        public bool SjekkInnLogging(Administrator innAdmin)
        {
            using (var db = new DBContext())
            {
                AdminDB funnetBruker = db.Administrator.FirstOrDefault(a => a.Brukernavn == innAdmin.Brukernavn);
                if (funnetBruker != null)
                {
                    byte[] passordForTest = LagHash(innAdmin.Passord + funnetBruker.Salt);
                    bool riktigBruker = funnetBruker.Passord.SequenceEqual(passordForTest);
                    return riktigBruker;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Film> HentFilmer()
        {
            using (var db = new DBContext())
            {
                List<Film> alleFilmer = new List<Film>();
                alleFilmer = db.Filmer.ToList();

                return alleFilmer;
            }
        }

        public Film HentFilm(int id)
        {
            using (var db = new DBContext())
            {
                var Film = db.Filmer.Find(id);
                if (Film != null)
                {
                    return Film;
                }
                return null;
            }
        }

        public List<Skuespiller> HentSkuespillere()
        {
            var db = new DBContext();
            var skuespillere = db.Skuespillere.ToList();
            List<Skuespiller> utSkuespillere = new List<Skuespiller>();
            foreach(var skuespiller in skuespillere)
            {
                var nySkuespiller = new Skuespiller()
                {
                    Fornavn = skuespiller.Fornavn,
                    Etternavn = skuespiller.Etternavn,
                    Alder = skuespiller.Alder,
                    Bilde = skuespiller.Bilde,
                    Land = skuespiller.Land,
                    id = skuespiller.id
                };
                utSkuespillere.Add(nySkuespiller);
            }

            return utSkuespillere;
        }

        public string LeggSkuespillerIFilm(int filmID, int skuespillerID)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var skuespiller = db.Skuespillere.Find(skuespillerID);
                    var film = db.Filmer.Find(filmID);
                    film.Skuespillere.Add(skuespiller);
                    db.SaveChanges();

                    LoggSkriver logg = new LoggSkriver();
                    string kommentar = skuespiller.Fornavn + " " + skuespiller.Etternavn;
                    kommentar += " ble lagt til i film " + film.Navn;
                    logg.GenerellLogg("LeggSkuespillerIFilm", kommentar);

                    return "OK";
                }
                catch(Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("LeggSkuespillerIFilm", e);
                    return "Feil";
                }
            }
        }

        public string SlettSkuespillerFraFilm(int filmID, int skuespillerID)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var skuespiller = db.Skuespillere.Find(skuespillerID);
                    var film = db.Filmer.Find(filmID);
                    film.Skuespillere.Remove(skuespiller);
                    db.SaveChanges();

                    LoggSkriver logg = new LoggSkriver();
                    string kommentar = skuespiller.Fornavn + " " + skuespiller.Etternavn;
                    kommentar += " ble slettet fra film " + film.Navn;
                    logg.GenerellLogg("SlettSkuespillerFraFilm", kommentar);

                    return "OK";
                }
                catch (Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("SlettSkuespillerFraFilm", e);
                    return "Feil";
                }
            }
        }

        public bool RedigerFilm(Film innFilm, HttpPostedFileBase bilde)
        {
            var db = new DBContext();
            bool resultat = true;
            try
            {
                if (FilErOk(bilde) && FilErBilde(bilde.FileName))
                {
                    var filNavn = Path.GetFileName(bilde.FileName);
                    var filBane = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("/Content/images/posters"), filNavn);
                    bilde.SaveAs(filBane);
                    innFilm.Bilde = Path.Combine("/Content/images/posters", filNavn);
                }
                else
                {
                    innFilm.Bilde = null;
                }
                
                Film endreFilm = db.Filmer.Find(innFilm.id);
                if(endreFilm != null)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.EndreFilmLogg(innFilm, endreFilm);

                    endreFilm.Beskrivelse = innFilm.Beskrivelse;
                    endreFilm.Navn = innFilm.Navn;
                    endreFilm.Gjennomsnitt = innFilm.Gjennomsnitt;
                    if (innFilm.Bilde != null)
                    {
                        endreFilm.Bilde = innFilm.Bilde;
                    }
                    endreFilm.Kontinent = innFilm.Kontinent;
                    endreFilm.Pris = innFilm.Pris;
                    endreFilm.Produksjonsår = innFilm.Produksjonsår;
                    endreFilm.ReleaseDate = innFilm.ReleaseDate;
                    endreFilm.Studio = innFilm.Studio;
                    endreFilm.Visninger = innFilm.Visninger;
                    db.SaveChanges();
                }
                else
                {
                    resultat = false;
                }
            }
            catch (Exception e)
            {
                LoggSkriver logg = new LoggSkriver();
                logg.FeilmeldingLogg("EndreFilmLogg", e);
                resultat = false;
            }
            return resultat;
        }

        // Metode som sjekker om filen som lastes opp er gyldig
        private bool FilErOk(HttpPostedFileBase fil)
        {
            return fil != null && fil.ContentLength > 0;
        }

        // Metode som sjekker om filen som lastes opp er et bilde
        private bool FilErBilde(string filnavn)
        {
            return (filnavn.EndsWith(".png") || filnavn.EndsWith(".jpg") || filnavn.EndsWith(".jpeg"));
        }

        public List<Sjanger> HentSjangereForFilm(int id)
        {
            var db = new DBContext();
            List<Sjanger> sjangere = new List<Sjanger>();
            var film = db.Filmer.Find(id);
            if (film != null)
            {
                for (int i = 0; i < film.Sjanger.Count(); i++)
                {
                    Sjanger sjanger = new Sjanger()
                    {
                        id = film.Sjanger[i].id,
                        sjanger = film.Sjanger[i].sjanger
                    };
                    sjangere.Add(sjanger);
                }

                return sjangere;
            }
            return null;
        }

        public List<Sjanger> HentSjangere()
        {
            using (var db = new DBContext())
            {
                List<Sjanger> alleSjangere = new List<Sjanger>();
                var dbSjangere = db.Sjangere.ToList();
                foreach(var sjanger in dbSjangere)
                {
                    Sjanger nySjanger = new Sjanger()
                    {
                        id = sjanger.id,
                        sjanger = sjanger.sjanger
                    };
                    alleSjangere.Add(nySjanger);
                }

                return alleSjangere;
            }
        }

        public string LeggSjangerIFilm(int filmID, int sjangerID)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var sjanger = db.Sjangere.Find(sjangerID);
                    var film = db.Filmer.Find(filmID);
                    film.Sjanger.Add(sjanger);
                    db.SaveChanges();

                    LoggSkriver logg = new LoggSkriver();
                    string kommentar = sjanger.sjanger;
                    kommentar += " ble lagt til film " + film.Navn;
                    logg.GenerellLogg("LeggSjangerIFilm", kommentar);

                    return "OK";
                }
                catch (Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("LeggSjangerIFilm", e);
                    return "Feil";
                }
            }
        }

        public string SlettSjangerFraFilm(int filmID, int sjangerID)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var sjanger = db.Sjangere.Find(sjangerID);
                    var film = db.Filmer.Find(filmID);
                    film.Sjanger.Remove(sjanger);
                    db.SaveChanges();

                    LoggSkriver logg = new LoggSkriver();
                    string kommentar = sjanger.sjanger;
                    kommentar += " ble slettet fra film " + film.Navn;
                    logg.GenerellLogg("SlettSjangerFraFilm", kommentar);

                    return "OK";
                }
                catch (Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("SlettSjangerFraFilm", e);
                    return "Feil";
                }
            }
        }

        public Skuespiller HentSkuespiller(int id)
        {
            var db = new DBContext();
            var skuespiller = db.Skuespillere.Find(id);
            if (skuespiller != null)
            {
                return skuespiller;
            }
            return null;
        }

        public List<Film> HentFilmerForAjax()
        {
            var db = new DBContext();
            List<Film> alleFilmer = new List<Film>();
            List<Film> dbFilmer = db.Filmer.ToList();

            foreach(var film in dbFilmer)
            {
                var nyFilm = new Film()
                {
                    id = film.id,
                    Bilde = film.Bilde,
                    Navn = film.Navn,
                    Beskrivelse = film.Beskrivelse,
                    Visninger = film.Visninger
                };
                alleFilmer.Add(nyFilm);
            }
            return alleFilmer;
        }

        public string LeggFilmISkuespiller(int skuespillerID, int filmID)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var skuespiller = db.Skuespillere.Find(skuespillerID);
                    var film = db.Filmer.Find(filmID);
                    skuespiller.Filmer.Add(film);
                    db.SaveChanges();

                    LoggSkriver logg = new LoggSkriver();
                    string kommentar = film.Navn;
                    kommentar += " ble lagt til skuespiller " + skuespiller.Fornavn + " " + skuespiller.Etternavn;
                    logg.GenerellLogg("LeggFilmISkuespiller", kommentar);

                    return "OK";
                }
                catch (Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("LeggFilmISkuespiller", e);
                    return "Feil";
                }
            }
        }

        public string SlettFilmFraSkuespiller(int skuespillerID, int filmID)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var skuespiller = db.Skuespillere.Find(skuespillerID);
                    var film = db.Filmer.Find(filmID);
                    skuespiller.Filmer.Remove(film);
                    db.SaveChanges();

                    LoggSkriver logg = new LoggSkriver();
                    string kommentar = film.Navn;
                    kommentar += " ble slettet fra skuespiller " + skuespiller.Fornavn + " " + skuespiller.Etternavn;
                    logg.GenerellLogg("SlettFilmFraSkuespiller", kommentar);

                    return "OK";
                }
                catch (Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("SlettFilmFraSkuespiller", e);
                    return "Feil";
                }
            }
        }

        public bool RedigerSkuespiller(Skuespiller innSkuespiller, HttpPostedFileBase bilde)
        {
            var db = new DBContext();
            bool resultat = true;
            try
            {
                if (FilErOk(bilde) && FilErBilde(bilde.FileName))
                {
                    var filNavn = Path.GetFileName(bilde.FileName);
                    var filBane = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("/Content/images/skuespillere"), filNavn);
                    bilde.SaveAs(filBane);
                    innSkuespiller.Bilde = Path.Combine("/Content/images/skuespillere", filNavn);
                }
                else
                {
                    innSkuespiller.Bilde = null;
                }

                Skuespiller endreSkuespiller = db.Skuespillere.Find(innSkuespiller.id);
                if (endreSkuespiller != null)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.EndreSkuespillerLogg(innSkuespiller, endreSkuespiller);

                    endreSkuespiller.Fornavn = innSkuespiller.Fornavn;
                    endreSkuespiller.Etternavn = innSkuespiller.Etternavn;
                    if (innSkuespiller.Bilde != null)
                    {
                        endreSkuespiller.Bilde = innSkuespiller.Bilde;
                    }
                    endreSkuespiller.Alder = innSkuespiller.Alder;
                    endreSkuespiller.Land = innSkuespiller.Land;
                    db.SaveChanges();
                }
                else
                {
                    resultat = false;
                }
            }
            catch (Exception e)
            {
                LoggSkriver logg = new LoggSkriver();
                logg.FeilmeldingLogg("RedigerSkuespiller", e);
                resultat = false;
            }
            return resultat;
        }

        public List<Kunde> HentKunder()
        {
            var db = new DBContext();
            List<Kunde> alleKunder = new List<Kunde>();
            var dbKunder = db.Kunder.ToList();
            foreach(var kunde in dbKunder)
            {
                Kunde nyKunde = new Kunde()
                {
                    Brukernavn = kunde.Brukernavn,
                    Fornavn = kunde.Fornavn,
                    Etternavn = kunde.Etternavn,
                    id = kunde.id,
                    Kort = kunde.Kort,
                    Filmer = kunde.Filmer,
                    Stemmer = kunde.Stemmer,
                    Ønskeliste = kunde.Ønskeliste
                };
                alleKunder.Add(nyKunde);
            }

            return alleKunder;
        }

        public EndreKunde HentKunde(int id)
        {
            var db = new DBContext();
            var dbKunde = db.Kunder.Find(id);
            if (dbKunde != null)
            {
                EndreKunde utKunde = new EndreKunde()
                {
                    id = dbKunde.id,
                    Fornavn = dbKunde.Fornavn,
                    Etternavn = dbKunde.Etternavn,
                    Brukernavn = dbKunde.Brukernavn,
                    Kort = dbKunde.Kort,
                    Filmer = dbKunde.Filmer,
                    Ønskeliste = dbKunde.Ønskeliste,
                    Stemmer = dbKunde.Stemmer
                };

                return utKunde;
            }
            return null;
        }

        public string SlettFilmFraBruker(int brukerID, int filmID)
        {
            using (var db = new DBContext())
            {
                try
                {
                    var bruker = db.Kunder.Find(brukerID);
                    var film = db.Filmer.Find(filmID);
                    bruker.Filmer.Remove(film);
                    db.SaveChanges();

                    LoggSkriver logg = new LoggSkriver();
                    string kommentar = film.Navn;
                    kommentar += " ble slettet fra bruker " + bruker.Brukernavn;
                    logg.GenerellLogg("SlettFilmFraBruker", kommentar);

                    return "OK";
                }
                catch (Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("SlettFilmFraBruker", e);
                    return "Feil";
                }
            }
        }

        public bool RedigerKunde(EndreKunde innKunde)
        {
            var db = new DBContext();
            bool resultat = true;
            try
            {
                KundeDB endreKunde = db.Kunder.Find(innKunde.id);
                if (endreKunde != null)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.EndreBruker(innKunde, endreKunde);

                    endreKunde.Fornavn = innKunde.Fornavn;
                    endreKunde.Etternavn = innKunde.Etternavn;
                    endreKunde.Brukernavn = innKunde.Brukernavn;
                    endreKunde.Kort = innKunde.Kort;
                    db.SaveChanges();
                }
                else
                {
                    resultat = false;
                }
            }
            catch (Exception e)
            {
                LoggSkriver logg = new LoggSkriver();
                logg.FeilmeldingLogg("RedigerKunde", e);
                resultat = false;
            }
            return resultat;
        }

        public bool OpprettSkuespiller(Skuespiller innSkuespiller, HttpPostedFileBase bilde)
        {
            using (var db = new DBContext())
            {
                bool resultat = true;
                try
                {
                    if (FilErOk(bilde) && FilErBilde(bilde.FileName))
                    {
                        var filNavn = Path.GetFileName(bilde.FileName);
                        var filBane = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("/Content/images/skuespillere"), filNavn);
                        bilde.SaveAs(filBane);
                        innSkuespiller.Bilde = Path.Combine("/Content/images/skuespillere", filNavn);
                    }
                    else
                    {
                        innSkuespiller.Bilde = "/Content/images/skuespillere/defaultActor.jpg";
                    }
                    db.Skuespillere.Add(innSkuespiller);
                    db.SaveChanges();

                    LoggSkriver logg = new LoggSkriver();
                    logg.OpprettSkuespillerLogg(innSkuespiller);
                }
                catch(Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("OpprettSkuespiller", e);
                    resultat = false;
                }

                return resultat;
            }
        }

        public bool SlettSkuespiller(int id)
        {
            using (var db = new DBContext())
            {
                bool resultat = true;
                try
                {
                    var skuespiller = db.Skuespillere.Find(id);
                    db.Skuespillere.Remove(skuespiller);
                    db.SaveChanges();

                    LoggSkriver logg = new LoggSkriver();
                    logg.SlettSkuespillerLogg(skuespiller);
                }
                catch (Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("SlettSkuespiller", e);
                    resultat = false;
                }
                
                return resultat;
            }
        }

        public bool OpprettFilm(Film innFilm, HttpPostedFileBase bilde)
        {
            using (var db = new DBContext())
            {
                bool resultat = true;
                try
                {
                    if (FilErOk(bilde) && FilErBilde(bilde.FileName))
                    {
                        var filNavn = Path.GetFileName(bilde.FileName);
                        var filBane = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("/Content/images/posters"), filNavn);
                        bilde.SaveAs(filBane);
                        innFilm.Bilde = Path.Combine("/Content/images/posters", filNavn);
                    }
                    else
                    {
                        innFilm.Bilde = "/Content/images/posters/defaultPoster.jpg";
                    }
                    innFilm.Gjennomsnitt = 0;
                    innFilm.Visninger = 0;
                    db.Filmer.Add(innFilm);
                    db.SaveChanges();

                    LoggSkriver logg = new LoggSkriver();
                    logg.OpprettFilmLogg(innFilm);
                }
                catch (Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("OpprettFilm", e);
                    resultat = false;
                }

                return resultat;
            }
        }

        public bool SlettFilm(int id)
        {
            using (var db = new DBContext())
            {
                bool resultat = true;
                try
                {
                    var film = db.Filmer.Find(id);
                    db.Filmer.Remove(film);
                    db.SaveChanges();

                    LoggSkriver logg = new LoggSkriver();
                    logg.SlettFilmLogg(film);
                }
                catch (Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("SlettFilm", e);
                    resultat = false;
                }

                return resultat;
            }
        }

        public bool SlettBruker(int id)
        {
            using (var db = new DBContext())
            {
                bool resultat = true;
                try
                {
                    var bruker = db.Kunder.Find(id);
                    if (bruker.Stemmer.Any())
                    {
                        var stemmer = db.Stemmer.Where(s => s.Kunde.id == bruker.id);
                        foreach(var stemme in stemmer)
                        {
                            db.Stemmer.Remove(stemme);
                        }
                    }
                    var kommentarer = db.Kommentarer.Where(k => k.Kunde.id == bruker.id);
                    if (kommentarer.Any() && kommentarer != null)
                    {
                        foreach (var kommentar in kommentarer)
                        {
                            db.Kommentarer.Remove(kommentar);
                        }
                    }
                    db.Kunder.Remove(bruker);
                    db.SaveChanges();

                    LoggSkriver logg = new LoggSkriver();
                    logg.SlettBrukerLogg(bruker);
                }
                catch (Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("SlettBruker", e);
                    resultat = false;
                }

                return resultat;
            }
        }

        public List<Nyhet> HentNyheter()
        {
            using (var db = new DBContext())
            {
                List<Nyhet> alleNyheter = db.Nyheter.ToList();
                return alleNyheter;
            }
        }

        public Nyhet HentNyhet(int id)
        {
            using (var db = new DBContext())
            {
                Nyhet utNyhet = db.Nyheter.Find(id);
                if(utNyhet != null)
                {
                    return utNyhet;
                }
                return null;
            }
        }

        public bool RedigerNyhet(Nyhet innNyhet)
        {
            bool resultat = true;
            var db = new DBContext();

            try
            {
                Nyhet endreNyhet = db.Nyheter.Find(innNyhet.id);
                if (endreNyhet != null)
                {
                    LoggSkriver logg = new LoggSkriver();
                    string kommentar = "Nyhet redigert: ";
                    kommentar += innNyhet.Tittel + " -> " + endreNyhet.Tittel + "\n";
                    kommentar += innNyhet.Beskjed + " -> " + endreNyhet.Beskjed + "\n";
                    logg.GenerellLogg("RedigerNyhet", kommentar);

                    endreNyhet.Dato = innNyhet.Dato;
                    endreNyhet.Tittel = innNyhet.Tittel;
                    endreNyhet.Beskjed = innNyhet.Beskjed;

                    db.SaveChanges();
                }
                else
                {
                    resultat = false;
                }
            }
            catch(Exception e)
            {
                LoggSkriver logg = new LoggSkriver();
                logg.FeilmeldingLogg("RedigerNyhet", e);
                resultat = false;
            }

            return resultat;
        }

        public bool OpprettNyhet(Nyhet innNyhet)
        {
            using (var db = new DBContext())
            {
                bool resultat = true;
                try
                {
                    innNyhet.Dato = DateTime.Now.ToString();
                    db.Nyheter.Add(innNyhet);
                    db.SaveChanges();

                    LoggSkriver logg = new LoggSkriver();
                    string kommentar = "Nyhet opprettet: ";
                    kommentar += innNyhet.Tittel + "\n";
                    kommentar += innNyhet.Beskjed;
                    logg.GenerellLogg("OpprettNyhet", kommentar);
                }
                catch (Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("OpprettNyhet", e);
                    resultat = false;
                }

                return resultat;
            }
        }

        public bool SlettNyhet(int id)
        {
            using (var db = new DBContext())
            {
                bool resultat = true;
                try
                {
                    var slettNyhet = db.Nyheter.Find(id);
                    if(slettNyhet != null)
                    {
                        db.Nyheter.Remove(slettNyhet);
                        db.SaveChanges();

                        LoggSkriver logg = new LoggSkriver();
                        string kommentar = "Nyhet slettet: ";
                        kommentar += slettNyhet.Tittel + "\n";
                        kommentar += slettNyhet.Beskjed;
                        logg.GenerellLogg("SlettNyhet", kommentar);
                    }
                    else
                    {
                        resultat = false;
                    }
                }
                catch (Exception e)
                {
                    LoggSkriver logg = new LoggSkriver();
                    logg.FeilmeldingLogg("SlettNyhet", e);
                    resultat = false;
                }
                return resultat;
            }
        }

        public string[] HentLogg()
        {
            LoggSkriver logg = new LoggSkriver();
            return logg.HentLoggInnhold();
        }


    }
}
