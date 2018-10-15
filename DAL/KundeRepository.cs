﻿using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace DAL
{
    public class KundeRepository : IKundeRepository
    {
        // Produserer visning av IndexView - forsiden hvor det vises informasjon fra flere klasser
        public IndexView HentIndexView()
        {
            IndexView indexView = new IndexView();
            indexView.Filmer = HentAlleFilmer();
            indexView.ActionFilmer = HentActionFilmer();
            indexView.Nyheter = HentIndexNyheter();

            return indexView;
        }

        // Henter filmliste etter angitt sortering
        public List<Film> HentFilmView(string sortering)
        {
            var db = new DBContext();
            List<Film> alleFilmer = new List<Film>();
            switch(sortering)
            {
                case "Dato":
                    alleFilmer = db.Filmer.OrderBy(f => f.ReleaseDate).ToList();
                    break;
                case "Visninger":
                    alleFilmer = db.Filmer.OrderByDescending(f => f.Visninger).ToList();
                    break;
                case "Stjerner":
                    alleFilmer = db.Filmer.OrderByDescending(f => f.Gjennomsnitt).ToList();
                    break;
                case "Kontinent":
                    alleFilmer = db.Filmer.OrderBy(f => f.Kontinent).ToList();
                    break;
                case "Produksjonsår":
                    alleFilmer = db.Filmer.OrderBy(f => f.Produksjonsår).ToList();
                    break;
                case "Alfabetisk":
                    alleFilmer = db.Filmer.OrderBy(f => f.Navn).ToList();
                    break;
                case "Pris":
                    alleFilmer = db.Filmer.OrderBy(f => f.Pris).ToList();
                    break;
                default:
                    alleFilmer = db.Filmer.ToList();
                    break;
            }
            return alleFilmer;
        }

        public List<Film> HentAlleFilmer()
        {
            using (var db = new DBContext())
            {
                List<Film> alleFilmer = new List<Film>();
                alleFilmer = db.Filmer.ToList();

                return alleFilmer;
            }
        }

        public Skuespiller HentSkuespiller(int id)
        {
            using (var db = new DBContext())
            {
                var Skuespiller = db.Skuespillere.Find(id);

                return Skuespiller;
            }
        }

        public Film HentFilm(int id)
        {
            using (var db = new DBContext())
            {
                var Film = db.Filmer.Find(id);

                return Film;
            }
        }

        // Metode for å stemme på film.
        // Hvis brukeren allerede har stemt på den aktuelle filmen blir den gamle stemmen slettet
        public bool StemPåFilm(int filmID, string brukernavn, int stemme)
        {
            var db = new DBContext();
            bool resultat = true;
            KundeDB Kunde = db.Kunder.FirstOrDefault(k => k.Brukernavn == brukernavn);
            Film film = db.Filmer.Find(filmID);
            Stemme vurdering = new Stemme()
            {
                AntallStjerner = stemme,
                Kunde = Kunde
            };

            try
            {
                if (film.Stemmer == null)
                {
                    film.Stemmer = new List<Stemme>();
                }
                var sjekkStemme = HarStemt(film, Kunde);
                if (sjekkStemme != null)
                {
                    db.Stemmer.Remove(sjekkStemme);
                }
                    film.Stemmer.Add(vurdering);

                db.SaveChanges();
            }
            catch (Exception e)
            {
                resultat = false;
            }

            if (!OppdaterGjennomsnitt(filmID))
            {
                resultat = false;
            }

            return resultat;
        }

        // Sjekker om den angitte kunden har stemt på den angitte filmen
        public Stemme HarStemt(Film film, KundeDB bruker)
        {
            foreach(var stemme in film.Stemmer)
            {
                if(stemme.Kunde != null)
                {
                    if (stemme.Kunde.id == bruker.id)
                    {
                        return stemme;
                    }
                }
            }
            return null;
        }

        // Metode som oppdaterer "terningkast" gjennomsnittet når en film får en ny stemme/vurdering
        public bool OppdaterGjennomsnitt(int filmID)
        {
            bool resultat = true;
            var db = new DBContext();
            try
            {
                Film film = db.Filmer.Find(filmID);
                int antallStemmer = film.Stemmer.Count();
                int total = 0;

                for (int i = 0; i < antallStemmer; i++)
                {
                    total += film.Stemmer[i].AntallStjerner;
                }

                double Gjennomsnitt = total / antallStemmer;
                film.Gjennomsnitt = Gjennomsnitt;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                resultat = false;
            }
            return resultat;
        }

        public bool RegistrerBruker(Kunde innKunde)
        {
            bool resultat = true;
            using (var db = new DBContext())
            {
                try
                {
                    string salt = LagSalt();
                    KundeDB nyKunde = new KundeDB()
                    {
                        Brukernavn = innKunde.Brukernavn,
                        Fornavn = innKunde.Fornavn,
                        Etternavn = innKunde.Etternavn,
                        Kort = innKunde.Kort,
                        Salt = salt,
                        Passord = LagHash(innKunde.Passord + salt),
                        Filmer = new List<Film>(),
                        Stemmer = new List<Stemme>()
                    };
                    db.Kunder.Add(nyKunde);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    resultat = false;
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

        /*
        Metode for å sjekke innloggingsinfo
        Skrevet av Faglærer i webapplikasjoner ved Oslomet, og refaktorert for å passe modellene i dette prosjektet
        */
        public bool SjekkInnLogging(Kunde innKunde)
        {
            using (var db = new DBContext())
            {
                KundeDB funnetBruker = db.Kunder.FirstOrDefault(b => b.Brukernavn == innKunde.Brukernavn);
                if (funnetBruker != null)
                {
                    byte[] passordForTest = LagHash(innKunde.Passord + funnetBruker.Salt);
                    bool riktigBruker = funnetBruker.Passord.SequenceEqual(passordForTest);
                    return riktigBruker;
                }
                else
                {
                    return false;
                }
            }
        }

        // Henter kunde for "Bruker" viewet - hvor en innlogget bruker kan endre sin egen informasjon
        public EndreKunde HentBruker(string brukernavn)
        {
            var db = new DBContext();
            KundeDB funnetKunde = db.Kunder.FirstOrDefault(k => k.Brukernavn == brukernavn);
            if (funnetKunde != null)
            {
                EndreKunde utKunde = new EndreKunde()
                {
                    Fornavn = funnetKunde.Fornavn,
                    Etternavn = funnetKunde.Etternavn,
                    id = funnetKunde.id,
                    Kort = funnetKunde.Kort,
                    Filmer = funnetKunde.Filmer,
                    Stemmer = funnetKunde.Stemmer,
                    Ønskeliste = funnetKunde.Ønskeliste
                };
                return utKunde;
            }
            else
            {
                return null;
            }
        }

        // Metode som lagrer kundeendring i databasen
        public bool EndreBruker(EndreKunde innKunde, string brukernavn)
        {
            var db = new DBContext();
            bool resultat = true;
            try
            {
                KundeDB KundeSomSkalEndres = db.Kunder.FirstOrDefault(k => k.Brukernavn == brukernavn);
                if(KundeSomSkalEndres != null)
                {
                    KundeSomSkalEndres.Fornavn = innKunde.Fornavn;
                    KundeSomSkalEndres.Etternavn = innKunde.Etternavn;
                    KundeSomSkalEndres.Kort = innKunde.Kort;
                    if (innKunde.Passord != null && innKunde.Passord != "")
                    {
                        KundeSomSkalEndres.Passord = LagHash(innKunde.Passord + KundeSomSkalEndres.Salt);
                    }
                    db.SaveChanges();
                }
                else
                {
                    resultat = false;
                }
            }
            catch(Exception e)
            {
                resultat = false;
            }
            return resultat;
        }

        public Film HentFilmInfo(int id)
        {
            var db = new DBContext();
            Film utFilm = db.Filmer.FirstOrDefault(f => f.id == id);

            return utFilm;
        }

        public Skuespiller HentSkuespillerInfo(int id)
        {
            var db = new DBContext();
            Skuespiller utSkuespiller = db.Skuespillere.FirstOrDefault(s => s.id == id);

            return utSkuespiller;
        }

        // Henter alle filmer fra kategorien "action" - vises på forsiden under egen kategori
        public List<Film> HentActionFilmer()
        {
            using (var db = new DBContext())
            {
                List<Film> actionFilmer = new List<Film>();
                List<Sjanger> sjangere = db.Sjangere.Where(k => k.sjanger == "Action").ToList();
                for (int i = 0; i < sjangere.Count(); i++)
                {
                    for (int j = 0; j < sjangere[i].Filmer.Count(); j++)
                    {
                        actionFilmer.Add(sjangere[i].Filmer[j]);
                    }
                }
           
                return actionFilmer;
            }
        }

        public List<Skuespiller> HentAlleSkuespillere()
        {
            using (var db = new DBContext())
            {
                return db.Skuespillere.ToList();
            }
        }

        // Henter liste av skuespillere etter angitt sortering - for å vises i Skuespillerviewet
        public List<Skuespiller> HentSkuespillerView(string sortering)
        {
            using (var db = new DBContext())
            {
                List<Skuespiller> alleSkuespillere = new List<Skuespiller>();
                switch (sortering)
                {
                    case "Fornavn":
                        alleSkuespillere = db.Skuespillere.OrderBy(s => s.Fornavn).ToList();
                        break;
                    case "Etternavn":
                        alleSkuespillere = db.Skuespillere.OrderBy(s => s.Etternavn).ToList();
                        break;
                    case "Alder":
                        alleSkuespillere = db.Skuespillere.OrderBy(s => s.Alder).ToList();
                        break;
                    case "Land":
                        alleSkuespillere = db.Skuespillere.OrderBy(s => s.Land).ToList();
                        break;
                }
                return alleSkuespillere;
            }
        }

        // Oppdaterer antall visninger en film har - når en bruker "kjøper" en film
        public bool OppdaterFilmVisningData(int id)
        {
            using (var db= new DBContext())
            {
                bool resultat = true;
                try
                {
                    var oppdaterFilm = db.Filmer.Find(id);
                    oppdaterFilm.Visninger++;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    resultat = false;
                }
                return resultat;
            }
        }

        // Legger til film i kundeobjektet for å registrere hvilke filmer den aktuelle kunden har kjøpt
        public bool LeggFilmIKundeObjekt(string innBruker, int filmID)
        {
            using (var db = new DBContext())
            {
                bool resultat = true;
                try
                {
                    var filmKunde = db.Kunder.FirstOrDefault(k => k.Brukernavn == innBruker);
                    var vistFilm = db.Filmer.Find(filmID);
                    if(filmKunde != null && vistFilm != null)
                    {
                        filmKunde.Filmer.Add(vistFilm);
                        db.SaveChanges();
                    }
                }
                catch(Exception e)
                {
                    resultat = false;
                }
                return resultat;
            }
        }

        public List<Nyhet> HentAlleNyheter()
        {
            using (var db = new DBContext())
            {
                return db.Nyheter.ToList();
            }
        }

        // Henter nyheter til indexviewet
        public List<Nyhet> HentIndexNyheter()
        {
            using (var db = new DBContext())
            {
                List<Nyhet> alleNyheter = db.Nyheter.OrderByDescending(n => n.id).ToList();

                return alleNyheter;
            }
        }

        public List<Sjanger> HentAlleSjangere()
        {
            var db = new DBContext();
            List<Sjanger> alleSjangere = db.Sjangere.ToList();
            for(int i = 0; i < alleSjangere.Count(); i++)
            {
                alleSjangere[i].antall = alleSjangere[i].Filmer.Count();
            }

            return alleSjangere;
        }

        // Henter alle filmer en angitt skuespiller har vært med i
        public List<Film> HentFilmerFraSkuespillerID(int id)
        {
            var db = new DBContext();

            List<Film> filmer = db.Skuespillere.Find(id).Filmer.Select(f => new Film()
            {
                id = f.id,
                Navn = f.Navn,
                Bilde = f.Bilde,
                Studio = f.Studio,
                Visninger = f.Visninger,
                Beskrivelse = f.Kontinent,
                ReleaseDate = f.ReleaseDate,
                Gjennomsnitt = f.Gjennomsnitt
            }).ToList();
            return filmer;
        }

        // Henter alle skuespillere i en angitt film
        public List<Skuespiller> HentSkuespillereIFilm(int id)
        {
            var db = new DBContext();

            List<Skuespiller> skuespillere = db.Filmer.Find(id).Skuespillere.Select(s => new Skuespiller()
            {
                id = s.id,
                Fornavn = s.Fornavn,
                Etternavn = s.Etternavn,
                Alder = s.Alder,
                Bilde = s.Bilde,
                Land = s.Land
            }).ToList();
            return skuespillere;
        }

        public bool SjekkOmBrukernavnErLedig(string brukernavn)
        {
            bool resultat = true;
            using (var db = new DBContext())
            {
                var sjekkBrukernavn = db.Kunder.FirstOrDefault(k => k.Brukernavn == brukernavn);
                if (sjekkBrukernavn != null)
                {
                    resultat = false;
                }
            }

            return resultat;
        }

        public bool LeggIØnskeliste(int FilmID, string Brukernavn)
        {
            bool resultat = true;
            using (var db = new DBContext())
            {
                KundeDB Kunde = db.Kunder.FirstOrDefault(k => k.Brukernavn == Brukernavn);
                if (Kunde != null)
                {
                    if (Kunde.Ønskeliste == null)
                    {
                        Kunde.Ønskeliste = new Ønskeliste();
                    }
                    if (Kunde.Ønskeliste.Filmer == null)
                    {
                        Kunde.Ønskeliste.Filmer = new List<Film>();
                    }
                    try
                    {
                        var film = db.Filmer.Find(FilmID);
                        Kunde.Ønskeliste.Filmer.Add(film);
                        db.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        resultat = false;
                    }
                }
                else
                {
                    resultat = false;
                }
            }

            return resultat;
        }

        public bool SlettFraØnskeliste(int FilmID, string brukernavn)
        {
            bool resultat = true;
            using(var db = new DBContext())
            {
                try
                {
                    var kunde = db.Kunder.FirstOrDefault(k => k.Brukernavn == brukernavn);
                    var film = db.Filmer.Find(FilmID);
                    kunde.Ønskeliste.Filmer.Remove(film);
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    resultat = false;
                }
            }
            return resultat;
        }

        // Henter ut en liste av filmer og skuespillere som søkeforslag når en bruker skriver inn et søk i søkefeltet
        public List<Søkeresultat> HentSøkeforslag(string input)
        {
            var db = new DBContext();
            List<Søkeresultat> søkeresultater = new List<Søkeresultat>();
            var filmer = db.Filmer.Where(f => f.Navn.Contains(input));
            var skuespillere = db.Skuespillere.Where(s => (s.Fornavn + s.Etternavn).Contains(input));
            foreach(var film in filmer)
            {
                var resultat = new Søkeresultat()
                {
                    Navn = film.Navn,
                    Bilde = film.Bilde,
                    id = film.id,
                    Type = "Film"
                };
                søkeresultater.Add(resultat);
            }
            foreach(var skuespiller in skuespillere)
            {
                var resultat = new Søkeresultat()
                {
                    Navn = skuespiller.Fornavn + " " + skuespiller.Etternavn,
                    Bilde = skuespiller.Bilde,
                    id = skuespiller.id,
                    Type = "Skuespiller"
                };
                søkeresultater.Add(resultat);
            }
            
            return søkeresultater;
        }

        // Metode som lar en bruker skrive en kommentar under en angitt film
        public bool SkrivKommentar (int id, string Melding, string Brukernavn)
        {
            bool resultat = true;
            using (var db = new DBContext())
            {
                try
                {
                    KundeDB bruker = db.Kunder.FirstOrDefault(k => k.Brukernavn == Brukernavn);
                    Kommentar kommentar = new Kommentar()
                    {
                        Kunde = bruker,
                        Melding = Melding,
                        Dato = DateTime.Now.ToString()
                    };
                    var film = db.Filmer.Find(id);
                    film.Kommentarer.Add(kommentar);
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    resultat = false;
                }
            }

            return resultat;
        }

        // Henter ut 3 filmer fra en tilfeldig sjanger som brukeren har sett før - vises som filmforslag på brukersiden
        public List<ForeslåttFilm> ForeslåFilm(string Brukernavn)
        {
            using (var db = new DBContext())
            {
                var Kunde = db.Kunder.FirstOrDefault(k => k.Brukernavn == Brukernavn);
                if(Kunde.Filmer.Any())
                {
                    var tilfeldigTall = new Random();
                    int antallSjangere = Kunde.Filmer.Count();
                    int tilfeldigSjanger = tilfeldigTall.Next(0, antallSjangere);
                    Sjanger sjanger = Kunde.Filmer[tilfeldigSjanger].Sjanger[0];
                    List<ForeslåttFilm> foreslåtteFilmer = new List<ForeslåttFilm>();

                    int teller = 0;
                    foreach(var film in sjanger.Filmer)
                    {
                        ForeslåttFilm nyFilm = new ForeslåttFilm()
                        {
                            Bilde = film.Bilde,
                            Navn = film.Navn,
                            Pris = film.Pris,
                            id = film.id,
                            Sjanger = sjanger.sjanger
                        };
                        foreslåtteFilmer.Add(nyFilm);
                        teller++;
                        if(teller == 3)
                        {
                            break;
                        }
                    }
                    return foreslåtteFilmer;
                }
                else
                {
                    return null;
                }
            }
        }



        /// Metoder som ikke er en del av løsningen, men har blitt brukt under utviklingen
        

        public bool InsertDBData()
        {
            var dbInsert = new DBinsert();
            bool ok = dbInsert.settInnIDB();
            if (ok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Kunde> HentKunder()
        {
            var db = new DBContext();
            var alleKunder = db.Kunder.Select(k => new Kunde()
            {
                Fornavn = k.Fornavn,
                Etternavn = k.Etternavn,
                id = k.id,
                Brukernavn = k.Brukernavn,
                Filmer = k.Filmer
            }).ToList();

            return alleKunder;
        }

    }
}