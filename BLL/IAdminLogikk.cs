using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IAdminLogikk
    {
        string RegistrerAdmin(int id);
        bool SjekkInnLogging(Administrator admin);
    }
}
