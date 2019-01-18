using MnMFiber.Core.Models;
using MnMFiber.Core.Models.HotoFiber;
using MnMFiber.Core.Repositories;
using MnMFiber.Core.Dtos.HotoFiber;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MnMFiber.Services
{
    public class HotoFiberService : BaseService, IHotoFiberService
    {
        public async Task<ObservableCollection<TicketBasicDto>> GetPendingTicketsAsync(string patrollerNumber)
        {
            var action = "HotoFiberApi/GetPendingTickets?patrollerNumber=" + patrollerNumber;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<ObservableCollection<TicketBasicDto>>(response);
        }

        public async Task<TicketBasicDto> GetTicketBasicInfoAsync(string ticketNo)
        {
            var action = "HotoFiberApi/GetTicketBasicInfo?ticketNo=" + ticketNo;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<TicketBasicDto>(response);
        }

        public async Task<dynamic> GetInputHelp()
        {
            var action = "HotoFiberApi/GetInputHelp";
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<dynamic>(response);
        }

        public async Task<ResponseModel> SaveTicketSpot(TicketSpotDto ticketSpot)
        {
            var response = await PostMasterChildObjAndReadAsStringAsync("HotoFiberApi", true
                , ticketSpot);

            return JsonConvert.DeserializeObject<ResponseModel>(response);
        }

        public async Task<ObservableCollection<TicketSpotDto>> GetTicketSpots(long ticketId)
        {
            var action = "HotoFiberApi/GetTicketSpots?ticketId=" + ticketId;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<ObservableCollection<TicketSpotDto>>(response);
        }

        public async Task<TicketSpotDto> GetTicketSpotDetails(string spotId)
        {
            var action = "HotoFiberApi/GetTicketSpotDetails?spotId=" + spotId;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<TicketSpotDto>(response);
        }

        public async Task<ResponseModel> SubmitTicketAsComplete(TicketSignatureUploadModel ticketDetail)
        {
            var response = await PostMasterChildObjAndReadAsStringAsync("HotoFiberApi/TicketSubmitAsComplete", true
                , ticketDetail);

            return JsonConvert.DeserializeObject<ResponseModel>(response);
        }

        public async Task<string> UploadFileAsync(dynamic file)
        {
            var response = await UploadAndReadAsStringAsync("AzureFileStorageApi/Upload?containerName=fiber", true, file);
            return Newtonsoft.Json.JsonConvert.DeserializeObject(response);
        }
    }
}
