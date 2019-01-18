using System;

namespace MnMLilon.Service.Configuration
{
    public class Config
    {
        public static string ProductionApiUrl = "http://fundrequest.xprocockpit.com";
        public static string DeveloperApiUrl = "http://192.168.1.216:44314";
        public static string RealIpApiUrl = "http://221.120.103.66:5656";
        public static string StagingApiUrl = "http://xprocockpitstaging.azurewebsites.net";

        public static string ApiUrl = $"{ProductionApiUrl}/api/";
        public static Uri GetApiUri(string action) => new Uri(ApiUrl + action);
        public static string GetToken()
        {
            return string.Empty;
        }
    }
}
