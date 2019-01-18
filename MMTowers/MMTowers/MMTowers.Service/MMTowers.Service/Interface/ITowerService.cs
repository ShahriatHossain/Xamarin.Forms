using MMTowers.Service.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMTowers.Service.Interface
{
    public interface ITowerService
    {
        Task<ObservableCollection<Tower>> GetAllByTechnicianAsync(string technicianNumber);
        Task<Tower> GetDetailAsync(int id);
    }
}
