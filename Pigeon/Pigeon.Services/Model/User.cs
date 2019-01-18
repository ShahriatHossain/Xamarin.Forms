using Newtonsoft.Json;

namespace Pigeon.Services.Model
{
    public class User
    {
        [JsonProperty(PropertyName = "ProfileGUID")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "email_id")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "country_code")]
        public long CountryCode { get; set; }

        [JsonProperty(PropertyName = "mobile_no")]
        public long MobileNo { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty(PropertyName = "expiration")]
        public System.DateTime TokenExpiryDate { get; set; }
    }
}
