using MnMFiber.Core.Dtos.PatrollerDailySurveillance;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MnMFiber.Core.Interfaces
{
    public interface IPatrollerDailySurveillanceService
    {
        Task<ObservableCollection<PatrollerLinkDto>> GetPatrollerLinksAsync(string patrollerNumber);
    }
}
