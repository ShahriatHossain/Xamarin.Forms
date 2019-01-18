using MnMFiber.Core.Models;
using System.Threading.Tasks;

namespace MnMFiber.Core.Repositories
{
    public interface IUserService
    {
        Task<CustomResponse> VerifyLoginAsync(UserAccount user);
        Task<CustomResponse> UpdatePasswordAsync(UserAccount user);
    }
}
