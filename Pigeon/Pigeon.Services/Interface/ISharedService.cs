using System.IO;
using System.Threading.Tasks;

namespace Pigeon.Services.Interface
{
    public interface ISharedService
    {
        Task<string> UploadFile(string filepath, string filename);

        string GetAmazonS3Url(string keyName);

        void UploadStream(MemoryStream stream, string keyName);

    }

}
