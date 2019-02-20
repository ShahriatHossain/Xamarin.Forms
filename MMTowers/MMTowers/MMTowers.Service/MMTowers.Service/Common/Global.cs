using System;

namespace MMTowers.Service.Common
{
    public class Global
    {
        public static string ProductionApiUrl = "xxxxx";
        public static string ApiUrl = $"{ProductionApiUrl}/api/";
        public static Uri GetApiUri(string action) => new Uri(ApiUrl + action);
        public static string GetToken()
        {
            return string.Empty;
        }
    }
}
