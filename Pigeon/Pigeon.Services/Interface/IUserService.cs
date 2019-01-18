using Pigeon.Services.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pigeon.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUser();
        bool IsVerifiedOtp();
        Task<string> SaveUserAsync(User user, bool isNew = false);
        Task<CustomJResponse> GetOtpCode(string email);
        Task<User> GetToken(string userName, string password);

    }
}
