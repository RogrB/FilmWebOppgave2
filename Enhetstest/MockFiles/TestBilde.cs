using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


/* 
 Klasse som implementer HttpPostedFileBase
 Brukes som mock fil for å teste bildeopplasting
 * */
namespace Enhetstest.MockFiles
{
    class TestBilde : HttpPostedFileBase
    {
        Stream stream;
        string ContentType;
        string fileName;

        public override string FileName
        {
            get { return fileName; }
        }
    }
}
