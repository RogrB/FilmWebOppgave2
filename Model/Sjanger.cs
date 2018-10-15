using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class Sjanger
    {
        public int id { get; set; }
        public string sjanger { get; set; }
        public int antall { get; set; }
        public virtual List<Film> Filmer { get; set; }
    }
}
