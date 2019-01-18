using MnMLilon.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace MnMLilon.Service.Implementation
{
    public class FileUploadService : BaseService, IFileUploadService
    {
        public async Task<string> UploadDocumentToServerAsync(dynamic file, int transactionId)
        {
            var action = "BatteryComission/UploadDocuments?id=" + transactionId;
            var response = await UploadAndReadAsStringAsync(action, true, file);
            return response;
        }

        public async Task<string> UploadFileToServerAsync(dynamic file)
        {
            var response = await UploadAndReadAsStringAsync("BatteryComission/UploadPhoto", true, file);
            return Newtonsoft.Json.JsonConvert.DeserializeObject(response);
        }

        public async Task<string> TroubleTicketUploadDocumentToServerAsync(dynamic file, int ticketId)
        {
            var action = "SiteBatteryTroubleTicketApi/TroubleTicketUploadDocuments?id=" + ticketId;
            var response = await UploadAndReadAsStringAsync(action, true, file);
            return response;
        }

        public async Task<string> TroubleTicketUploadFileToServerAsync(dynamic file)
        {
            var response = await UploadAndReadAsStringAsync("SiteBatteryTroubleTicketApi/TroubleTicketUploadPhoto", true, file);
            return Newtonsoft.Json.JsonConvert.DeserializeObject(response);
        }
    }
}
