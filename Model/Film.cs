using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model
{
    public class Film
    {
        public int id { get; set; }

        [Required(ErrorMessage = "FilmNavn må oppgis")]
        [RegularExpression(@"^[A-Za-z0-9æøåØÆÅ@_\- ]{1,}", ErrorMessage = "FilmNavn kan ikke inneholde spesialtegn")]
        public string Navn { get; set; }

        [Required(ErrorMessage = "Produksjonsår må oppgis")]
        [RegularExpression(@"[0-9]{1,}", ErrorMessage = "Produksjonsår må være tall")]
        public int Produksjonsår { get; set; }

        [Required(ErrorMessage = "Kontinent må oppgis")]
        [RegularExpression(@"^[A-Za-z0-9æøåØÆÅ@_\- ]{1,}", ErrorMessage = "Kontinent kan ikke inneholde spesialtegn")]
        public string Kontinent { get; set; }

        [Required(ErrorMessage = "Studio må oppgis")]
        [RegularExpression(@"^[A-Za-z0-9æøåØÆÅ@_\- ]{1,}", ErrorMessage = "Studio kan ikke inneholde spesialtegn")]
        public string Studio { get; set; }

        public string Bilde { get; set; }

        public int Visninger { get; set; }

        [Required(ErrorMessage = "Beskrivelse må oppgis")]
        [RegularExpression(@"^[A-Za-z0-9æøåØÆÅ@_\- ]{1,}", ErrorMessage = "Beskrivelse kan ikke inneholde spesialtegn")]
        public string Beskrivelse { get; set; }

        [Required(ErrorMessage = "Releasedate må oppgis")]
        [RegularExpression(@"[0-9.,\- ]{1,}", ErrorMessage = "Releasedate kan ikke inneholde spesialtegn")]
        public string ReleaseDate { get; set; }

        [Required(ErrorMessage = "Pris må oppgis")]
        [RegularExpression(@"[0-9]{1,}", ErrorMessage = "Pris må være tall")]
        public int Pris { get; set; }

        public double Gjennomsnitt { get; set; }

        public virtual List<Skuespiller> Skuespillere { get; set; }

        public virtual List<Sjanger> Sjanger { get; set; }

        public virtual List<Stemme> Stemmer { get; set; }

        public virtual List<Ønskeliste> Ønskeliste { get; set; }

        public virtual List<Kommentar> Kommentarer { get; set; }
    }

    public class ForeslåttFilm
    {
        public int id { get; set; }
        public string Navn { get; set; }
        public string Bilde { get; set; }
        public int Pris { get; set; }
        public string Sjanger { get; set; }
    }
}
