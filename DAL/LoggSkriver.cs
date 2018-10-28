using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    class LoggSkriver
    {
        private readonly string filBane = System.Web.HttpContext.Current.Server.MapPath("/Logg.txt");

        public LoggSkriver()
        {
            
            if (!LoggEksisterer())
            {
                File.Create(filBane).Dispose();
            }
        }

        private bool LoggEksisterer()
        {
            return File.Exists(filBane);
        }

        private void SkrivLogg(string melding, TextWriter w)
        {
            w.Write("\r\n Logg Innlegg: \r\n");
            w.Write("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.Write(" :\r\n {0}", melding);
            w.Write("\r\n------------");
        }

        public void OpprettFilmLogg(Film innFilm)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "Film opprettet: \r\n";
                melding += innFilm.Navn + "\r\n";
                melding += innFilm.Bilde + "\r\n";
                melding += innFilm.Beskrivelse + "\r\n";
                melding += innFilm.Kontinent + "\r\n";
                melding += innFilm.Produksjonsår + "\r\n";
                melding += innFilm.ReleaseDate + "\r\n";
                melding += innFilm.Studio;

                SkrivLogg(melding, w);
            }
        }

        public void EndreFilmLogg(Film innFilm, Film gammelFilm)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "Endring av Film: \r\n";
                melding += gammelFilm.Navn + " -> " + innFilm.Navn + "\r\n";
                melding += gammelFilm.Bilde + " -> " + innFilm.Bilde + "\r\n";
                melding += gammelFilm.Kontinent + " -> " + innFilm.Kontinent + "\r\n";
                melding += gammelFilm.Beskrivelse + " -> " + innFilm.Beskrivelse + "\r\n";
                melding += gammelFilm.Gjennomsnitt + " -> " + innFilm.Gjennomsnitt + "\r\n";
                melding += gammelFilm.Pris + " -> " + innFilm.Pris + "\r\n";
                melding += gammelFilm.Produksjonsår + " -> " + innFilm.Produksjonsår + "\r\n";
                melding += gammelFilm.ReleaseDate + " -> " + innFilm.ReleaseDate + "\r\n";
                melding += gammelFilm.Studio + " -> " + innFilm.Studio + "\r\n";
                melding += gammelFilm.Visninger + " -> " + innFilm.Visninger;

                SkrivLogg(melding, w);
            }
        }

        public void SlettFilmLogg(Film innFilm)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "Film Slettet: ";
                melding += innFilm.Navn;

                SkrivLogg(melding, w);
            }
        }

        public void OpprettSkuespillerLogg(Skuespiller innSkuespiller)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "Skuespiller opprettet: \r\n";
                melding += innSkuespiller.Fornavn + "\r\n";
                melding += innSkuespiller.Etternavn + "\r\n";
                melding += innSkuespiller.Alder + "\r\n";
                melding += innSkuespiller.Land;

                SkrivLogg(melding, w);
            }
        }

        public void EndreSkuespillerLogg(Skuespiller innSkuespiller, Skuespiller gammelSkuespiller)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "Endring av Skuespiller: \r\n";
                melding += gammelSkuespiller.Fornavn + " -> " + innSkuespiller.Fornavn + "\r\n";
                melding += gammelSkuespiller.Etternavn + " -> " + innSkuespiller.Etternavn + "\r\n";
                melding += gammelSkuespiller.Alder + " -> " + innSkuespiller.Alder + "\r\n";
                melding += gammelSkuespiller.Land + " -> " + innSkuespiller.Land;

                SkrivLogg(melding, w);
            }
        }

        public void SlettSkuespillerLogg(Skuespiller innSkuespiller)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "Skuespiller Slettet: ";
                melding += innSkuespiller.Fornavn + " " + innSkuespiller.Etternavn;

                SkrivLogg(melding, w);
            }
        }

        public void EndreBruker(EndreKunde innKunde, KundeDB gammelKunde)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "Endring av Bruker: \r\n";
                melding += gammelKunde.Fornavn + " -> " + innKunde.Fornavn + "\r\n";
                melding += gammelKunde.Etternavn + " -> " + innKunde.Etternavn + "\r\n";
                melding += gammelKunde.Brukernavn+ " -> " + innKunde.Brukernavn + "\r\n";
                melding += gammelKunde.Kort + " -> " + innKunde.Kort + "\r\n";

                SkrivLogg(melding, w);
            }
        }

        public void SlettBrukerLogg(KundeDB innKunde)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "Bruker Slettet: ";
                melding += innKunde.Brukernavn;

                SkrivLogg(melding, w);
            }
        }

        public void GenerellLogg(string metode, string kommentar)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "Metoden: " + metode + " har gjort endring i databasen med kommentar: \r\n";
                melding += kommentar;

                SkrivLogg(melding, w);
            }
        }

        public void FeilmeldingLogg(string metode, Exception feilmelding)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "En feil oppstod ved metoden ";
                melding += metode + ". \r\nFeilmelding: " + feilmelding;

                SkrivLogg(melding, w);
            }
        }

        public string[] HentLoggInnhold()
        {
            return File.ReadAllLines(filBane);
        }

    }
}
