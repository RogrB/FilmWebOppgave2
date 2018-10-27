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
        private readonly string filBane = "Logg.txt";

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
            w.Write("\r\n Logg Entry: ");
            w.Write("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.Write(" : ");
            w.Write(" : {0}", melding);
            w.Write("------------");
        }

        public void OpprettFilmLogg(Film innFilm)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "Film opprettet: ";
                melding += innFilm.Navn + "\n";
                melding += innFilm.Bilde + "\n";
                melding += innFilm.Beskrivelse + "\n";
                melding += innFilm.Kontinent + "\n";
                melding += innFilm.Produksjonsår + "\n";
                melding += innFilm.ReleaseDate + "\n";
                melding += innFilm.Studio;

                SkrivLogg(melding, w);
            }
        }

        public void EndreFilmLogg(Film innFilm, Film gammelFilm)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "Endring av Film: \n";
                melding += gammelFilm.Navn + " -> " + innFilm.Navn + "\n";
                melding += gammelFilm.Bilde + " -> " + innFilm.Bilde + "\n";
                melding += gammelFilm.Kontinent + " -> " + innFilm.Kontinent + "\n";
                melding += gammelFilm.Beskrivelse + " -> " + innFilm.Beskrivelse + "\n";
                melding += gammelFilm.Gjennomsnitt + " -> " + innFilm.Gjennomsnitt + "\n";
                melding += gammelFilm.Pris + " -> " + innFilm.Pris + "\n";
                melding += gammelFilm.Produksjonsår + " -> " + innFilm.Produksjonsår + "\n";
                melding += gammelFilm.ReleaseDate + " -> " + innFilm.ReleaseDate + "\n";
                melding += gammelFilm.Studio + " -> " + innFilm.Studio + "\n";
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
                string melding = "Skuespiller opprettet: ";
                melding += innSkuespiller.Fornavn + "\n";
                melding += innSkuespiller.Etternavn + "\n";
                melding += innSkuespiller.Alder + "\n";
                melding += innSkuespiller.Land;

                SkrivLogg(melding, w);
            }
        }

        public void EndreSkuespillerLogg(Skuespiller innSkuespiller, Skuespiller gammelSkuespiller)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "Endring av Skuespiller: \n";
                melding += gammelSkuespiller.Fornavn + " -> " + innSkuespiller.Fornavn + "\n";
                melding += gammelSkuespiller.Etternavn + " -> " + innSkuespiller.Etternavn + "\n";
                melding += gammelSkuespiller.Alder + " -> " + innSkuespiller.Alder + "\n";
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
                string melding = "Endring av Bruker: \n";
                melding += gammelKunde.Fornavn + " -> " + innKunde.Fornavn + "\n";
                melding += gammelKunde.Etternavn + " -> " + innKunde.Etternavn + "\n";
                melding += gammelKunde.Brukernavn+ " -> " + innKunde.Brukernavn + "\n";
                melding += gammelKunde.Kort + " -> " + innKunde.Kort + "\n";

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
                string melding = "Metoden: " + metode + " har gjort endring i databasen med kommentar:";
                melding += kommentar;

                SkrivLogg(melding, w);
            }
        }

        public void FeilmeldingLogg(string metode, Exception feilmelding)
        {
            using (StreamWriter w = File.AppendText(filBane))
            {
                string melding = "En feil oppstod ved metoden ";
                melding += metode + ". \nFeilmelding: " + feilmelding;

                SkrivLogg(melding, w);
            }
        }

    }
}
