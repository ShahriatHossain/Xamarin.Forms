using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.Interface
{
    public interface IFileUploadService
    {
        Task<string> UploadFileToServerAsync(dynamic file);

        Task<string> UploadDocumentToServerAsync(dynamic file, int transactionId);

        Task<string> TroubleTicketUploadDocumentToServerAsync(dynamic file, int ticketId);

        Task<string> TroubleTicketUploadFileToServerAsync(dynamic file);
    }
}
