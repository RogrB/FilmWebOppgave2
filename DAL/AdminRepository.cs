using Model;
using System;
using System.Security.Cryptography;
using System.Linq;
using System.Collections.Generic;

namespace DAL
{
    public class AdminRepository : IAdminRepository
    {
        public string RegistrerAdmin(int id)
        {
            string resultat = "";
            if (AdminEksisterer(1))
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
                }
                catch (Exception e)
                {
                    resultat = "Feil";
                }
            }
            return resultat;
        }

        public bool AdminEksisterer(int id)
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

                    return "OK";
                }
                catch(Exception e)
                {
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

                    return "OK";
                }
                catch (Exception e)
                {
                    return "Feil";
                }
            }
        }

        public bool RedigerFilm(Film innFilm)
        {
            var db = new DBContext();
            bool resultat = true;
            try
            {
                Film endreFilm = db.Filmer.Find(innFilm.id);
                if(endreFilm != null)
                {
                    endreFilm.Beskrivelse = innFilm.Beskrivelse;
                    endreFilm.Navn = innFilm.Navn;
                    endreFilm.Gjennomsnitt = innFilm.Gjennomsnitt;
                    endreFilm.Bilde = innFilm.Bilde;
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
                resultat = false;
            }
            return resultat;
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

                    return "OK";
                }
                catch (Exception e)
                {
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

                    return "OK";
                }
                catch (Exception e)
                {
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

                    return "OK";
                }
                catch (Exception e)
                {
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

                    return "OK";
                }
                catch (Exception e)
                {
                    return "Feil";
                }
            }
        }

        public bool RedigerSkuespiller(Skuespiller innSkuespiller)
        {
            var db = new DBContext();
            bool resultat = true;
            try
            {
                Skuespiller endreSkuespiller = db.Skuespillere.Find(innSkuespiller.id);
                if (endreSkuespiller != null)
                {
                    endreSkuespiller.Fornavn = innSkuespiller.Fornavn;
                    endreSkuespiller.Etternavn = innSkuespiller.Etternavn;
                    endreSkuespiller.Bilde = innSkuespiller.Bilde;
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

                    return "OK";
                }
                catch (Exception e)
                {
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
                resultat = false;
            }
            return resultat;
        }

        public bool OpprettSkuespiller(Skuespiller innSkuespiller)
        {
            using (var db = new DBContext())
            {
                bool resultat = true;
                try
                {
                    db.Skuespillere.Add(innSkuespiller);
                    db.SaveChanges();
                }
                catch(Exception e)
                {
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
                }
                catch (Exception e)
                {
                    resultat = false;
                }
                
                return resultat;
            }
        }

        public bool OpprettFilm(Film innFilm)
        {
            using (var db = new DBContext())
            {
                bool resultat = true;
                try
                {
                    db.Filmer.Add(innFilm);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
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
                }
                catch (Exception e)
                {
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
                    db.Kunder.Remove(bruker);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    resultat = false;
                }

                return resultat;
            }
        }


    }
}
