using Pigeon.Services.Interface;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using Xamarin.Forms;

namespace Pigeon.Helpers
{
    public class ImageResizeHelper
    {
        private CommonHelper _commonHelper;
        public ImageResizeHelper()
        {
            _commonHelper = new CommonHelper();
        }
        public byte[] ResizeImage(MediaFile file, float width, float height)
        {
            return DependencyService.Get<IImageResizeService>().ResizeImage(this.ConvertMediaFiletoByteArray(file), width, height);
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

        public string GetUrlFromAmazon(string keyName)
        {
            var response = string.Empty;
            try
            {
                response = DependencyService.Get<ISharedService>().GetAmazonS3Url(keyName);
            }
            catch (Exception ex)
            {
                _commonHelper.DisplayAlertMessage(string.Empty, "The system got problem, please contact to app support team.");
            }
            return response;
        }
    }
}
