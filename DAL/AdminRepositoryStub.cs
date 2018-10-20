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
                    Navn = "Film01",
                    Bilde = "Bilde01",
                    Kontinent = "USA",
                    Visninger = 2013,
                    Produksjonsår = 2016,
                    Sjanger = new List<Sjanger>(),
                    Stemmer = new List<Stemme>()
                };
                Sjanger sjanger = new Sjanger()
                {
                    sjanger = "Action"
                };
                Stemme stemme = new Stemme()
                {
                    AntallStjerner = 5
                };
                enFilm.Sjanger.Add(sjanger);
                enFilm.Stemmer.Add(stemme);

                return enFilm;
          }
            else
            {
                return null;
            }
        }

        public List<Skuespiller> HentSkuespillere()
        {
            var alleSkuespillere = new List<Skuespiller>();
            var skuespiller = new Skuespiller()
            {
                id = 1,
                Fornavn = "Ole",
                Etternavn = "Olesen",
                Alder = 33,
                Bilde = "Bilde01",
                Land = "Norge",
                Filmer = new List<Film>()
            };
            var film = new Film()
            {
                Navn = "Film01",
                id = 1,
                Bilde = "Bilde02"
            };
            skuespiller.Filmer.Add(film);
            alleSkuespillere.Add(skuespiller);
            alleSkuespillere.Add(skuespiller);
            alleSkuespillere.Add(skuespiller);

            return alleSkuespillere;
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
                return null;
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

        public string LeggSjangerIFilm(int filmID, int skuespillerID)
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

        public Skuespiller HentSkuespiller(int id)
        {
            if (id == 1)
            {
                Skuespiller skuespiller = new Skuespiller()
                {
                    id = 1,
                    Fornavn = "Ole",
                    Etternavn = "Olesen",
                    Alder = 33,
                    Land = "Norge",
                    Bilde = "Bilde01",
                    Filmer = new List<Film>()
                };
                Film film = new Film()
                {
                    id = 1,
                    Navn = "Film01",
                    Visninger = 1234
                };
                skuespiller.Filmer.Add(film);

                return skuespiller;
            }
            else
            {
                return null;
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
            Kunde kunde = new Kunde()
            {
                id = 1,
                Fornavn = "Ole",
                Etternavn = "Olesen",
                Brukernavn = "oleO",
                Kort = 123456789098789,
                Filmer = new List<Film>()
            };
            Film film = new Film()
            {
                id = 1,
                Navn = "Film01",
                Beskrivelse = "Film for testbehov",
                Visninger = 1234
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
                EndreKunde kunde = new EndreKunde()
                {
                    id = 1,
                    Fornavn = "Ole",
                    Etternavn = "Olesen",
                    Brukernavn = "oleO",
                    Kort = 123456789098789,
                    Filmer = new List<Film>()
                };
                Film film = new Film()
                {
                    id = 1,
                    Navn = "Film01",
                    Beskrivelse = "Film for testbehov",
                    Visninger = 1234
                };
                kunde.Filmer.Add(film);

                return kunde;
            }
            else
            {
                return null;
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

    }
}
