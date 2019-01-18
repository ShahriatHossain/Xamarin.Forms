using Pigeon.Services.Interface;
using Plugin.FilePicker.Abstractions;
using System;
using System.IO;
using Xamarin.Forms;

namespace Pigeon.Helpers
{
    public class FileManageHelper
    {
        public enum FileType
        {
            ImageLarge,
            ImageSmall,
            PDF
        }
        private CommonHelper _commonHelper;
        public FileManageHelper()
        {
            _commonHelper = new CommonHelper();
        }

        public bool SaveFileAsByteToFileServer(Byte[] contents, string keyName)
        {
            Stream stream = new MemoryStream(contents);
            return SaveFileAsStreamToFileServer((MemoryStream)stream, keyName);
        }

        public bool SaveFileAsStreamToFileServer(MemoryStream inputStream, string keyName)
        {
            try
            {
                DependencyService.Get<ISharedService>().UploadStream(inputStream, keyName);
            }
            catch (Exception ex)
            {
                _commonHelper.DisplayAlertMessage(string.Empty, "The system got problem, please contact to app support team.");
                return false;
            }

            return true;
        }


        // Convert File to Bytearray
        public byte[] ConvertMediaFiletoByteArray(Plugin.Media.Abstractions.MediaFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                //file.Dispose();
                return memoryStream.ToArray();
            }
        }

        public string DownloadFile(string url)
        {
            var response = string.Empty;
            try
            {
                response = DependencyService.Get<IHttpFileManageService>().DownloadFile(url);
            }
            catch (Exception ex)
            {
                _commonHelper.DisplayAlertMessage(string.Empty, "The system got problem, please contact to app support team.");
            }
            return response;
        }

        //convert bytearray to image
        public ImageSource ConvertByteArrayToImage(byte[] byteArray)
        {
            return ImageSource.FromStream(() => new MemoryStream(byteArray));
        }

        //convert bytearray to stream
        public MemoryStream ConvertByteArrayToStream(byte[] byteArray)
        {
            var ms = new MemoryStream();
            ms.Write(byteArray, 0, byteArray.Length);
            ms.Position = 0;
            return ms;
        }

        public void DeleteLocalFile(string filePath)
        {
            DependencyService.Get<ILocalFileManageService>().DeleteFile(filePath);
        }

        public byte[] ConvertFileDatatoByteArray(FileData file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                //file.Dispose();
                return memoryStream.ToArray();
            }
        }

        public byte[] GetFileDataArray(string filePath)
        {
            return DependencyService.Get<ILocalFileManageService>().GetFileDataArray(filePath);
        }
    }
}
