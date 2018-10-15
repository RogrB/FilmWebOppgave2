using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class Ønskeliste
    {
        public int id { get; set; }
        public virtual List<Film> Filmer { get; set; }
    }
}
