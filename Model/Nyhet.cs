using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model
{
    public class Nyhet
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Tittel må oppgis")]
        [RegularExpression(@"^[A-Za-z0-9æøåØÆÅ@_\- ]{1,}", ErrorMessage = "Tittel kan ikke inneholde spesialtegn")]
        public string Tittel { get; set; }

        [Required(ErrorMessage = "Beskjed må oppgis")]
        [RegularExpression(@"^[A-Za-z0-9æøåØÆÅ@_\- ]{1,}", ErrorMessage = "Beskjed kan ikke inneholde spesialtegn")]
        public string Beskjed { get; set; }

        public string Dato { get; set; }
    }
}