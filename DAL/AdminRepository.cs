using Model;
using System;
using System.Security.Cryptography;
using System.Linq;

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
    }
}
