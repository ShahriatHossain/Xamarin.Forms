using Pigeon.iOS;
using Pigeon.Services.Interface;
using System;
using System.IO;
using System.Net;

[assembly: Xamarin.Forms.Dependency(typeof(HttpFileManageService))]
namespace Pigeon.iOS
{
    public class HttpFileManageService : IHttpFileManageService
    {
        public string DownloadFile(string fileUrl)
        {
            string downloadPath = string.Empty;
            var webClient = new WebClient();
            var url = new Uri(fileUrl); //new Uri("https://www.xamarin.com/content/images/pages/branding/assets/xamagon.png");
            var bytes = webClient.DownloadData(url); // get the downloaded data
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //string documentsPath = (string)Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads).AbsolutePath;
            string localFilename = "uno.downloaded.pdf";
            downloadPath = Path.Combine(documentsPath, localFilename);
            File.Delete(downloadPath);
            File.WriteAllBytes(downloadPath, bytes);
            var isFileExists = File.Exists(downloadPath);
            return downloadPath;
        }

        //public static async Task<int> CreateDownloadTask(string urlToDownload, IProgress<DownloadBytesProgress> progessReporter)
        //{
        //    int receivedBytes = 0;
        //    int totalBytes = 0;
        //    WebClient client = new WebClient();

        //    using (var stream = await client.OpenReadTaskAsync(urlToDownload))
        //    {
        //        byte[] buffer = new byte[4096];
        //        totalBytes = Int32.Parse(client.ResponseHeaders[HttpResponseHeader.ContentLength]);

        //        for (;;)
        //        {
        //            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
        //            if (bytesRead == 0)
        //            {
        //                await Task.Yield();
        //                break;
        //            }

        //            receivedBytes += bytesRead;
        //            if (progessReporter != null)
        //            {
        //                DownloadBytesProgress args = new DownloadBytesProgress(urlToDownload, receivedBytes, totalBytes);
        //                progessReporter.Report(args);
        //            }
        //        }
        //    }
        //    return receivedBytes;
        //}
    }
}