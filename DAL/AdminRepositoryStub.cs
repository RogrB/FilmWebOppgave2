using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class AdminRepositoryStub : IAdminRepository
    {
        public bool RegistrerAdmin(int id)
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
