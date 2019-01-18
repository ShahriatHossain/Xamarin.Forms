using Pigeon.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pigeon.DataAccess.Repository
{
    public interface IUserInstituteRepository : IRepository<UserInstitute>
    {
        bool Save(List<UserInstitute> userInstitutes);
    }
}
