using System;

namespace MnMFiber.Services
{
    public class ApiSettings
    {
        public static string ProductionApiUrl = "http://fundrequest.xprocockpit.com";
        public static string DeveloperApiUrl = "http://192.168.1.216:44314";
        public static string RealIpBasedApiUrl = "http://221.120.103.66:5656";

        public static string ApiUrl = $"{DeveloperApiUrl}/api/";
        public static Uri GetApiUri(string action) => new Uri(ApiUrl + action);
    }
}
