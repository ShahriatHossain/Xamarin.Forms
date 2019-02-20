using System;

namespace Pigeon.Services.Configuration
{
    public static class GlobalVariables
    {
        public static string ProductionApiUrl = "xxxxxxx";
        
        public static string ApiUrl = $"{ProductionApiUrl}/api/v1/";
        public static Uri GetApiUri(string action) => new Uri(ApiUrl + action);

        public static string AzureBlobUrl = "xxxxxx";
    }
}
