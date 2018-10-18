using DAL;
using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class AdminLogikk : IAdminLogikk
    {
        private IAdminRepository _repository;
        public AdminLogikk()
        {
            _repository = new AdminRepository();
        }

        public AdminLogikk(IAdminRepository stub)
        {
            _repository = stub;
        }

        public string RegistrerAdmin(int id)
        {
            string resultat = _repository.RegistrerAdmin(id);
            return resultat;
        }

        public bool SjekkInnLogging(Administrator innAdmin)
        {
            bool resultat = _repository.SjekkInnLogging(innAdmin);
            return resultat;
        }

        public List<Film> HentFilmer()
        {
            List<Film> alleFilmer = _repository.HentFilmer();
            return alleFilmer;
        }
    }
}
