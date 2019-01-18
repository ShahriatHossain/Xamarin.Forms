using MnMFiber.Core.Models;
using MnMFiber.Core.Models.HotoFiber;
using MnMFiber.Core.Dtos.HotoFiber;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MnMFiber.Core.Repositories
{
    public interface IHotoFiberService
    {
        Task<ObservableCollection<TicketBasicDto>> GetPendingTicketsAsync(string patrollerNumber);
        Task<TicketBasicDto> GetTicketBasicInfoAsync(string ticketNo);
        Task<dynamic> GetInputHelp();
        Task<ResponseModel> SaveTicketSpot(TicketSpotDto ticketSpot);
        Task<ObservableCollection<TicketSpotDto>> GetTicketSpots(long ticketId);
        Task<TicketSpotDto> GetTicketSpotDetails(string spotId);
        Task<ResponseModel> SubmitTicketAsComplete(TicketSignatureUploadModel ticketDetail);
        Task<string> UploadFileAsync(dynamic file);
    }
}
