﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model
{
    public class Kunde
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_]{1,}", ErrorMessage = "Fornavn kan ikke inneholde spesialtegn")]
        public string Fornavn { get; set; }

        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_]{1,}", ErrorMessage = "Etternavn kan ikke inneholde spesialtegn")]
        public string Etternavn { get; set; }

        [Required(ErrorMessage = "Brukernavn må oppgis")]
        [RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_]{1,}", ErrorMessage = "Brukernavn kan ikke inneholde spesialtegn")]
        public string Brukernavn { get; set; }

        [Required(ErrorMessage = "Passord må oppgis")]
        [RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_]{8,}", ErrorMessage = "Passord må være minst 8 bokstaver eller tall, og kan ikke inneholde spesialtegn")]
        public string Passord { get; set; }


        [Required(ErrorMessage = "Kortinfo må oppgis")]
        [RegularExpression(@"[0-9]{12,19}", ErrorMessage = "Kredittkort må være mellom 12 og 19 siffer")]
        [Display(Name = "Kredittkort")]
        public long Kort { get; set; }

        public virtual List<Film> Filmer { get; set; }
        public virtual List<Stemme> Stemmer { get; set; }
        public virtual Ønskeliste Ønskeliste { get; set; }
    }

    // Klasse for å endre kundeinfo - gjøres i egen klasse så ikke passordfeltet skal være required
    public class EndreKunde
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_]{1,}", ErrorMessage = "Fornavn kan ikke inneholde spesialtegn")]
        public string Fornavn { get; set; }

        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_]{1,}", ErrorMessage = "Etternavn kan ikke inneholde spesialtegn")]
        public string Etternavn { get; set; }

        [Required(ErrorMessage = "Brukernavn må oppgis")]
        [RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_]{1,}", ErrorMessage = "Brukernavn kan ikke inneholde spesialtegn")]
        public string Brukernavn { get; set; }

        [RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_]{8,}", ErrorMessage = "Passord må være minst 8 bokstaver eller tall, og kan ikke inneholde spesialtegn")]
        public string Passord { get; set; }

        [Required(ErrorMessage = "Kortinfo må oppgis")]
        [RegularExpression(@"[0-9]{12,19}", ErrorMessage = "Kredittkort må være mellom 12 og 19 siffer")]
        [Display(Name = "Kredittkort")]
        public long Kort { get; set; }
        public virtual Ønskeliste Ønskeliste { get; set; }
        public virtual List<Film> Filmer { get; set; }
        public virtual List<Stemme> Stemmer { get; set; }
    }

    public class KundeDB
    {
        public int id { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Brukernavn { get; set; }
        public byte[] Passord { get; set; }
        public string Salt { get; set; }
        public long Kort { get; set; }
        public virtual List<Film> Filmer { get; set; }
        public virtual List<Stemme> Stemmer { get; set; }
        public virtual Ønskeliste Ønskeliste { get; set; }
    }

}