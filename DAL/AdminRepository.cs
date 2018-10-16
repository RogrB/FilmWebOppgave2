using Model;
using System;
using System.Security.Cryptography;

namespace DAL
{
    public class AdminRepository : IAdminRepository
    {
        public bool RegistrerAdmin(int id)
        {
            bool resultat = true;
            using (var db = new DBContext())
            {
                try
                {
                    string salt = LagSalt();
                    AdministratorDB admin = new AdministratorDB() 
                    {
                        Brukernavn = "admin",
                        Salt = salt,
                        Passord = LagHash("admin" + salt),
                    };
                    db.Administrator.Add(admin);
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
    }
}
