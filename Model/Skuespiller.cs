using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model
{
    public class Skuespiller
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Fornavn må oppgis")]
        //[RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_- ]{1,}", ErrorMessage = "Fornavn kan ikke inneholde spesialtegn")]
        public string Fornavn { get; set; }

        [Required(ErrorMessage = "Etternavn må oppgis")]
        //[RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_- ]{1,}", ErrorMessage = "Etternavn kan ikke inneholde spesialtegn")]
        public string Etternavn { get; set; }

        [Required(ErrorMessage = "Land må oppgis")]
        //[RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_- ]{1,}", ErrorMessage = "Land kan ikke inneholde spesialtegn")]
        public string Land { get; set; }

        [Required(ErrorMessage = "Alder må oppgis")]
        [RegularExpression(@"[0-9]{1,}", ErrorMessage = "Alder må være tall")]
        public int Alder { get; set; }

        public string Bilde { get; set; }

        public virtual List<Film> Filmer { get; set; }
    }
}