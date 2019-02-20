using System;

namespace Pigeon.Services.Configuration
{
    public static class GlobalVariables
    {
        public static string ProductionApiUrl = "http://pigeonapi.azurewebsites.net";
        
        public static string ApiUrl = $"{ProductionApiUrl}/api/v1/";
        public static Uri GetApiUri(string action) => new Uri(ApiUrl + action);

        public static string AzureBlobUrl = "http://implevistabd.blob.core.windows.net/pigeoncontainer/";
    }
}
