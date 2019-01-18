using Pigeon.iOS;
using Pigeon.Services.Interface;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalFileManageService))]
namespace Pigeon.iOS
{
    public class LocalFileManageService : ILocalFileManageService
    {
        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public byte[] GetFileDataArray(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
