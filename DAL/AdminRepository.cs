﻿using Model;
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

                return Film;
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
    }
}
