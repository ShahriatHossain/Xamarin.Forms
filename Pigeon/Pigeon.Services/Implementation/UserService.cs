using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pigeon.LocalDataAccess.Model;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pigeon.Services.Implementation
{
    public class UserService : BaseService, IUserService
    {
        public async Task<IEnumerable<Model.User>> GetUser()
        {
            var url = string.Empty;
            HttpClient client = new HttpClient(new NativeMessageHandler());
            var response = await client.GetAsync(url);

            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }

            return data;
        }

        public bool IsVerifiedOtp()
        {
            if (LocalStorageSettings.IsOtpVerified)
                return true;

            return false;
        }
        public bool IsOtpSent()
        {
            if (LocalStorageSettings.IsOtpSent)
                return true;

            return false;
        }
        public async Task<string> SaveUserAsync(Model.User user, bool isNew = false)
        {
            try
            {
                var response = await PostAndReadAsStringAsync("account", false
                , CreateInput("Email", user.Email)
                , CreateInput("CountryCode", user.CountryCode)
                , CreateInput("Password", user.Password)
                , CreateInput("MobileNo", user.MobileNo));

                return response;
            }
            catch (Exception ex)
            {

            }

            return string.Empty;

        }

        public async Task<CustomJResponse> GetOtpCode(string email)
        {
            var content = await PostAndReadAsStringAsync("account/OneTimePassword/" + email, false);
            return JsonConvert.DeserializeObject<CustomJResponse>(content);
        }

        public async Task<Model.User> GetToken(string userName, string password)
        {
            var response = await PostAndReadAsStringAsync("account/token", false
                , CreateInput("userName", userName)
                , CreateInput("password", password));

            if (!"Failed to generate token".Equals(response))
            {
                var jObject = JObject.Parse(response);
                return jObject.ToObject<Model.User>();
            } else
            {
                return new User();
            }
        }
    }
}
