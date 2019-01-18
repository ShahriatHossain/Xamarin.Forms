using MMTowers.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMTowers.Service.Model;
using Newtonsoft.Json;

namespace MMTowers.Service.Implementation
{
    public class AccountService : BaseService, IAccountService
    {
        public async Task<CustomResponse> UpdatePasswordAsync(Account account)
        {
            var response = await PostAndReadAsStringAsync("Account/UpdatePassword", false
                , CreateInput("LoginId", account.UserName)
                , CreateInput("Password", account.Password)
                , CreateInput("ConfirmPassword", account.ConfirmPassword)
                , CreateInput("OldPassword", account.OldPassword));

            return JsonConvert.DeserializeObject<CustomResponse>(response);
        }

        public async Task<CustomResponse> VerifyLoginAsync(Account account)
        {
            var response = await PostAndReadAsStringAsync("Account", false
                , CreateInput("UserName", account.UserName)
                , CreateInput("Password", account.Password));

            return JsonConvert.DeserializeObject<CustomResponse>(response);
        }
    }
}
