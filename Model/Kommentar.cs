using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class Kommentar
    {
        public int id { get; set; }
        public virtual KundeDB Kunde { get; set; }
        public string Melding { get; set; }
        public string Dato { get; set; }
    }
}