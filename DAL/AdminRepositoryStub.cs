using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class AdminRepositoryStub : IAdminRepository
    {
        public string RegistrerAdmin(int id)
        {
            if (id == 1)
            {
                return "OK";
            }
            else if (id == 2)
            {
                return "Admin er allerede registrert";
            }
            else
            {
                return "Feil";
            }
        }

        public bool AdminEksisterer(int id)
        {
            if (id == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SjekkInnLogging(Administrator innAdmin)
        {
            if (innAdmin.Brukernavn == "admin" && innAdmin.Passord == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Film> HentFilmer()
        {
            var alleFilmer = new List<Film>();
            var film = new Film()
            {
                id = 1,
                Navn = "Film01",
                Bilde = "bilde1",
                Beskrivelse = "Dette er en film",
                Gjennomsnitt = 3,
                Kontinent = "USA",
                Pris = 20,
                Studio = "Studio01",
                Produksjonsår = 1999,
                Visninger = 16,
                ReleaseDate = "12/12/2014",
                Sjanger = new List<Sjanger>(),
                Skuespillere = new List<Skuespiller>(),
                Kommentarer = new List<Kommentar>()
            };
            var skuespiller = new Skuespiller()
            {
                id = 1,
                Fornavn = "Per",
                Etternavn = "Persen",
                Bilde = "bilde02",
                Alder = 48,
                Land = "Norge"
            };
            var sjanger = new Sjanger()
            {
                id = 1,
                sjanger = "Action"
            };
            var kommentar = new Kommentar()
            {
                Dato = "12/12/2018",
                id = 1,
                Melding = "Dette er en kommentar"
            };
            film.Skuespillere.Add(skuespiller);
            film.Sjanger.Add(sjanger);
            film.Kommentarer.Add(kommentar);
            alleFilmer.Add(film);
            alleFilmer.Add(film);
            alleFilmer.Add(film);

            return alleFilmer;
        }

        public Film HentFilm(int id)
        {
            if (id == 1)
            {
                Film enFilm = new Film()
                {
                    id = 1,
                    Navn = "Film01",
                    Bilde = "bilde1",
                    Beskrivelse = "Dette er en film",
                    Gjennomsnitt = 3,
                    Kontinent = "USA",
                    Pris = 20,
                    Studio = "Studio01",
                    Produksjonsår = 1999,
                    Visninger = 16,
                    ReleaseDate = "12/12/2014",
                    Sjanger = new List<Sjanger>(),
                    Skuespillere = new List<Skuespiller>(),
                    Kommentarer = new List<Kommentar>()
                };
                var skuespiller = new Skuespiller()
                {
                    id = 1,
                    Fornavn = "Per",
                    Etternavn = "Persen",
                    Bilde = "bilde02",
                    Alder = 48,
                    Land = "Norge"
                };
                var sjanger = new Sjanger()
                {
                    id = 1,
                    sjanger = "Action"
                };
                var kommentar = new Kommentar()
                {
                    Dato = "12/12/2018",
                    id = 1,
                    Melding = "Dette er en kommentar"
                };
                enFilm.Sjanger.Add(sjanger);
                enFilm.Skuespillere.Add(skuespiller);
                enFilm.Kommentarer.Add(kommentar);

                return enFilm;
          }
            else
            {
                Film utFilm = new Film();
                utFilm.id = 0;
                return utFilm;
            }
        }

        public List<Skuespiller> HentSkuespillere()
        {
            var alleSkuespillere = new List<Skuespiller>();
            var Skuespiller = new Skuespiller()
            {
                id = 1,
                Fornavn = "Ole",
                Etternavn = "Olesen",
                Land = "Norge",
                Bilde = "Bilde01",
                Alder = 22,
                Filmer = new List<Film>()
            };
            var Film = new Film()
            {
                id = 1,
                Navn = "Film01",
                Bilde = "Bilde01"
            };
            Skuespiller.Filmer.Add(Film);
            alleSkuespillere.Add(Skuespiller);
            alleSkuespillere.Add(Skuespiller);
            alleSkuespillere.Add(Skuespiller);

            return alleSkuespillere;
        }

        public Skuespiller HentSkuespiller(int id)
        {
            if (id == 1)
            {
                var skuespiller = new Skuespiller()
                {
                    id = 1,
                    Fornavn = "Ole",
                    Etternavn = "Olesen",
                    Land = "Norge",
                    Bilde = "Bilde01",
                    Alder = 22,
                    Filmer = new List<Film>()
                };
                var Film = new Film()
                {
                    id = 1,
                    Navn = "Film01",
                    Bilde = "Bilde01"
                };
                skuespiller.Filmer.Add(Film);

                return skuespiller;
            }
            else
            {
                Skuespiller skuespiller = new Skuespiller();
                skuespiller.id = 0;
                return skuespiller;
            }
        }

        public string LeggSkuespillerIFilm(int filmID, int skuespillerID)
        {
            if (filmID == 1 && skuespillerID == 1)
            {
                return "OK";
            }
            else
            {
                return "Feil";
            }
        }

        public string SlettSkuespillerFraFilm(int filmID, int skuespillerID)
        {
            if (filmID == 1 && skuespillerID == 1)
            {
                return "OK";
            }
            else
            {
                return "Feil";
            }
        }

        public bool RedigerFilm(Film innFilm)
        {
            if (innFilm.id == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Sjanger> HentSjangereForFilm(int id)
        {
            if (id == 1)
            {
                List<Sjanger> sjangere = new List<Sjanger>();
                Sjanger sjanger = new Sjanger()
                {
                    id = 1,
                    sjanger = "Action"
                };
                sjangere.Add(sjanger);
                sjangere.Add(sjanger);
                sjangere.Add(sjanger);

                return sjangere;
            }
            else
            {
                List<Sjanger> sjangere = new List<Sjanger>();
                var sjanger = new Sjanger();
                sjanger.id = 0;
                sjangere.Add(sjanger);
                sjangere.Add(sjanger);
                sjangere.Add(sjanger);

                return sjangere;
            }
        }

        public List<Sjanger> HentSjangere()
        {
            List<Sjanger> sjangere = new List<Sjanger>();
            Sjanger sjanger = new Sjanger()
            {
                id = 1,
                sjanger = "Action"
            };
            sjangere.Add(sjanger);
            sjangere.Add(sjanger);
            sjangere.Add(sjanger);

            return sjangere;
        }

        public string LeggSjangerIFilm(int filmID, int sjangerID)
        {
            if (filmID == 1 && sjangerID == 1)
            {
                return "OK";
            }
            else
            {
                return "Feil";
            }
        }

        public string SlettSjangerFraFilm(int filmID, int sjangerID)
        {
            if (filmID == 1 && sjangerID == 1)
            {
                return "OK";
            }
            else
            {
                return "Feil";
            }
        }

        public List<Film> HentFilmerForAjax()
        {
            var alleFilmer = new List<Film>();
            var film = new Film()
            {
                id = 1,
                Navn = "Film01",
                Bilde = "bilde1",
                Beskrivelse = "Dette er en film",
                Gjennomsnitt = 3,
                Kontinent = "USA",
                Pris = 20,
                Studio = "Studio01",
                Produksjonsår = 1999,
                Visninger = 16,
                ReleaseDate = "12/12/2014",
                Sjanger = new List<Sjanger>(),
                Skuespillere = new List<Skuespiller>(),
                Kommentarer = new List<Kommentar>()
            };
            var skuespiller = new Skuespiller()
            {
                id = 1,
                Fornavn = "Per",
                Etternavn = "Persen",
                Bilde = "bilde02",
                Alder = 48,
                Land = "Norge"
            };
            var sjanger = new Sjanger()
            {
                id = 1,
                sjanger = "Action"
            };
            var kommentar = new Kommentar()
            {
                Dato = "12/12/2018",
                id = 1,
                Melding = "Dette er en kommentar"
            };
            film.Skuespillere.Add(skuespiller);
            film.Sjanger.Add(sjanger);
            film.Kommentarer.Add(kommentar);
            alleFilmer.Add(film);
            alleFilmer.Add(film);
            alleFilmer.Add(film);

            return alleFilmer;
        }

        public string LeggFilmISkuespiller(int skuespillerID, int filmID)
        {
            if (filmID == 1 && skuespillerID == 1)
            {
                return "OK";
            }
            else
            {
                return "Feil";
            }
        }

        public string SlettFilmFraSkuespiller(int skuespillerID, int filmID)
        {
            if (filmID == 1 && skuespillerID == 1)
            {
                return "OK";
            }
            else
            {
                return "Feil";
            }
        }

        public bool RedigerSkuespiller(Skuespiller innSkuespiller)
        {
            if(innSkuespiller.id == 1)
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
            var alleKunder = new List<Kunde>();
            var kunde = new Kunde()
            {
                id = 1,
                Fornavn = "Ole",
                Etternavn = "Olesen",
                Kort = 1234567890987654,
                Brukernavn = "ole0",
                Filmer = new List<Film>()
            };
            var film = new Film()
            {
                id = 1,
                Navn = "Film01",
                Bilde = "Bilde01",
                Beskrivelse = "Beskrivelse01",
                Kontinent = "USA"
            };
            kunde.Filmer.Add(film);

            alleKunder.Add(kunde);
            alleKunder.Add(kunde);
            alleKunder.Add(kunde);

            return alleKunder;
        }

        public EndreKunde HentKunde(int id)
        {
            if (id == 1)
            {
                var kunde = new EndreKunde()
                {
                    id = 1,
                    Fornavn = "Ole",
                    Etternavn = "Olesen",
                    Kort = 1234567890987654,
                    Brukernavn = "ole0",
                    Filmer = new List<Film>()
                };
                var film = new Film()
                {
                    id = 1,
                    Navn = "Film01",
                    Bilde = "Bilde01",
                    Beskrivelse = "Beskrivelse01",
                    Kontinent = "USA"
                };
                kunde.Filmer.Add(film);

                return kunde;
            }
            else
            {
                var kunde = new EndreKunde();
                kunde.id = 0;
                return kunde;
            }
        }

        public string SlettFilmFraBruker(int brukerID, int filmID)
        {
            if (filmID == 1 && brukerID == 1)
            {
                return "OK";
            }
            else
            {
                return "Feil";
            }
        }

        public bool RedigerKunde(EndreKunde innKunde)
        {
            if (innKunde.id == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool OpprettSkuespiller(Skuespiller innSkuespiller)
        {
            if (innSkuespiller.id == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SlettSkuespiller(int id)
        {
            if (id == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool OpprettFilm(Film innFilm)
        {
            if (innFilm.id == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SlettFilm(int id)
        {
            if (id == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SlettBruker(int id)
        {
            if (id == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Nyhet> HentNyheter()
        {
            List<Nyhet> alleNyheter = new List<Nyhet>();
            Nyhet enNyhet = new Nyhet()
            {
                id = 1,
                Tittel = "Nyhet01",
                Dato = "Dato01",
                Beskjed = "Dette er en nyhet"
            };

            alleNyheter.Add(enNyhet);
            alleNyheter.Add(enNyhet);
            alleNyheter.Add(enNyhet);

            return alleNyheter;
        }

        public Nyhet HentNyhet(int id)
        {
            if (id == 1)
            {
                Nyhet utNyhet = new Nyhet()
                {
                    id = 1,
                    Tittel = "Nyhet01",
                    Dato = "Dato01",
                    Beskjed = "Dette er en nyhet"
                };

                return utNyhet;
            }
            else
            {
                Nyhet utNyhet = new Nyhet()
                {
                    id = 0
                };
                return utNyhet;
            }
        }

        public bool RedigerNyhet(Nyhet innNyhet)
        {
            if (innNyhet.id == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool OpprettNyhet(Nyhet innNyhet)
        {
            if(innNyhet.id == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SlettNyhet(int id)
        {
            if (id == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
