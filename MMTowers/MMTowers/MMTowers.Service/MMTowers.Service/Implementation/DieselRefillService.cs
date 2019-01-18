using MMTowers.Service.Interface;
using MMTowers.Service.Model;
using Plugin.Media.Abstractions;
using System.Threading.Tasks;

namespace MMTowers.Service.Implementation
{
    public class DieselRefillService : BaseService, IDieselRefillService
    {
        public async Task<string> RefillDieselAsync(DieselRefill dieselRefill)
        {
            var response = await PostAndReadAsStringAsync("DieselRefill", true
                , CreateInput("BTSId", dieselRefill.BTSId)
                , CreateInput("RefillBy", dieselRefill.RefillBy)
                , CreateInput("InitialQuantity", dieselRefill.InitialQuantity)
                , CreateInput("FilledQuantity", dieselRefill.FilledQuantity)
                , CreateInput("HMRPhoto", dieselRefill.HMRPhoto)
                , CreateInput("EBPhoto", dieselRefill.EBPhoto)
                , CreateInput("GCUPhoto", dieselRefill.GSUPhoto)
                , CreateInput("DieselBillPhoto", dieselRefill.DieselBillPhoto));

            return response;
        }

        public async Task<string> UploadRefillImageAsync(MediaFile media, MediaFile media2, MediaFile media3, MediaFile media4)
        {
            var response = await UploadAndReadAsStringAsync("DieselRefill/Upload", true, media, media2, media3, media4);
            return response;
        }
    }
}
