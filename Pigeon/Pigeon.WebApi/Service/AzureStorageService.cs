using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Pigeon.WebApi.Service
{
    public class AzureStorageService : IFileStorageService
    {
        private IConfiguration _config;
        public AzureStorageService(IConfiguration config)
        {
            this._config = config;
        }
        public async Task Upload(Stream stream, string blobName)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(this._config.GetConnectionString("AzureBlogConnection"));


            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("pigeoncontainer");            
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
            await blockBlob.UploadFromStreamAsync(stream);            
        }
    }
}
