using Android.Graphics;
using Pigeon.Droid;
using Pigeon.Services;
using Pigeon.Services.Interface;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(ImageResizeService))]
namespace Pigeon.Droid
{
    public class ImageResizeService : IImageResizeService
    {
        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }
    }
}