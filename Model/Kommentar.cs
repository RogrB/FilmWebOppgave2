using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model
{
    public class Kommentar
    {
        public int id { get; set; }

        public virtual KundeDB Kunde { get; set; }

        [Required(ErrorMessage = "Ingen melding har blitt skrevet")]
        [RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_- ]{1,}", ErrorMessage = "Feltet kan ikke inneholde spesialtegn")]
        public string Melding { get; set; }

        public string Dato { get; set; }
    }
}