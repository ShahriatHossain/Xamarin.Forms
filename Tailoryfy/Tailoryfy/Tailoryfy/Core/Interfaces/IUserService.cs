using System.Threading.Tasks;
using Tailoryfy.Core.Models;

namespace Tailoryfy.Core.Repositories
{
    public interface IUserService
    {
        Task<CustomResponse> VerifyLoginAsync(UserAccount user);
        Task<CustomResponse> UpdatePasswordAsync(UserAccount user);
    }
}
