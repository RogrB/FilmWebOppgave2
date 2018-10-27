using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Administrator
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Brukernavn må oppgis")]
        [RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_]{1,}", ErrorMessage = "Brukernavn kan ikke inneholde spesialtegn")]
        public string Brukernavn { get; set; }

        [Required(ErrorMessage = "Passord må oppgis")]
        [RegularExpression(@"[A-Za-z0-9æøåØÆÅ@_]{8,}", ErrorMessage = "Passord må være minst 8 bokstaver eller tall, og kan ikke inneholde spesialtegn")]
        public string Passord { get; set; }
    }



}
