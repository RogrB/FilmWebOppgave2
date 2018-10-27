using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBModels
{
    public class AdminDB
    {
        public int id { get; set; }
        public string Brukernavn { get; set; }
        public byte[] Passord { get; set; }
        public string Salt { get; set; }
    }
}
