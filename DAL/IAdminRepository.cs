using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IAdminRepository
    {
        string RegistrerAdmin(int id);
        bool AdminEksisterer(int id);
    }
}
