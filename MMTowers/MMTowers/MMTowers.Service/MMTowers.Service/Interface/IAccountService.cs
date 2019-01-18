using MMTowers.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMTowers.Service.Interface
{
    public interface IAccountService
    {
        Task<CustomResponse> VerifyLoginAsync(Account account);
        Task<CustomResponse> UpdatePasswordAsync(Account account);
    }
}
