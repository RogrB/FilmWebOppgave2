using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class AdminRepositoryStub : IAdminRepository
    {
        public string RegistrerAdmin(int id)
        {
            if (id == 1)
            {
                return "OK";
            }
            else if (id == 2)
            {
                return "Admin er allerede registrert";
            }
            else
            {
                return "Feil";
            }
        }

        public bool AdminEksisterer(int id)
        {
            if (id == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
