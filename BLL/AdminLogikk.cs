using DAL;
using System;

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
    }
}
