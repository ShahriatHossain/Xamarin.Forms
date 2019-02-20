using System;

namespace MnMLilon.Service.Configuration
{
    public class Config
    {
        public static string ProductionApiUrl = "xxxx";
        public static string ApiUrl = $"{ProductionApiUrl}/api/";
        public static Uri GetApiUri(string action) => new Uri(ApiUrl + action);
        public static string GetToken()
        {
            return string.Empty;
        }
    }
}
