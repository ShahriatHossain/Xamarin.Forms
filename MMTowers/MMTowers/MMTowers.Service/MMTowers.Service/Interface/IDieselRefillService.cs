using MMTowers.Service.Model;
using Plugin.Media.Abstractions;
using System.Threading.Tasks;

namespace MMTowers.Service.Interface
{
    public interface IDieselRefillService
    {
        Task<string> RefillDieselAsync(DieselRefill dieselRefill);
        Task<string> UploadRefillImageAsync(MediaFile media, MediaFile media2, MediaFile media3, MediaFile media4);
    }
}
