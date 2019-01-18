using MnMFiber.Core.Dtos.PatrollerDailySurveillance;
using MnMFiber.Core.Interfaces;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MnMFiber.Services
{
    public class PatrollerDailySurveillanceService : BaseService, IPatrollerDailySurveillanceService
    {
        public async Task<ObservableCollection<PatrollerLinkDto>> GetPatrollerLinksAsync(string patrollerNumber)
        {
            var action = "HotoFiberSurveillanceApi/GetPatrollerLinks?patrollerNumber=" + patrollerNumber;
            var response = await GetAndReadAsStringAsync(action, true);
            return JsonConvert.DeserializeObject<ObservableCollection<PatrollerLinkDto>>(response);
        }
    }
}
