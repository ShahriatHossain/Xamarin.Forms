using MnMLilon.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.Interface
{
    public interface IUserService
    {
        Task<CustomResponse> VerifyLoginAsync(UserAccount user);
        Task<CustomResponse> UpdatePasswordAsync(UserAccount user);
    }
}
