using System;

namespace MnMFiber.Services
{
    public class ApiSettings
    {
        public static string ProductionApiUrl = "xxxx";

        public static string ApiUrl = $"{ProductionApiUrl}/api/";
        public static Uri GetApiUri(string action) => new Uri(ApiUrl + action);
    }
}
