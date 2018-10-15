using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class IndexView
    {
        public List<Film> Filmer { get; set; }
        public List<Film> ActionFilmer { get; set; }
        public List<Nyhet> Nyheter { get; set; }
    }
}