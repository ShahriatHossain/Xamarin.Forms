using System;

namespace Pigeon.Services.Configuration
{
    public static class GlobalVariables
    {
        public static string ProductionApiUrl = "http://pigeonapi.azurewebsites.net";
        public static string DeveloperApiUrl = "http://192.168.1.216:44310";
        public static string RealIpApiUrl = "http://221.120.103.66:5858";
        public static string DeveloperApiUrl2 = "http://192.168.1.215:44310";
        public static string DeveloperApiUrl3 = "http://192.168.1.217/Pigeon.WebApi";

        public static string ApiUrl = $"{DeveloperApiUrl3}/api/v1/";
        public static Uri GetApiUri(string action) => new Uri(ApiUrl + action);

        public static string AzureBlobUrl = "http://implevistabd.blob.core.windows.net/pigeoncontainer/";
    }
}
