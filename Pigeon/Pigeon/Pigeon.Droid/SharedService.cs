using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Pigeon.Droid;
using Pigeon.Services;
using Pigeon.Services.Interface;
using System;
using System.IO;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(SharedService))]
namespace Pigeon.Droid
{
    public class SharedService : ISharedService
    {
        const string bucketName = "noteongo1";
        public async Task<string> UploadFile(string filepath, string filename)
        {
            try
            {
                AmazonS3Client client = SetAmazonCredential();
                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = filename,
                    FilePath = filepath
                };
                // Put object
                PutObjectResponse response = await client.PutObjectAsync(request);
                return "File uploaded to S3 Bucket";
            }
            catch (AmazonS3Exception s3Exception)
            {
                return "Upload failed. " + s3Exception.Message;
            }
        }

        public string GetAmazonS3Url(string keyName)
        {
            var url = string.Empty;
            try
            {
                AmazonS3Client client = SetAmazonCredential();
                var expiryUrlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = keyName,
                    Expires = DateTime.Now.AddDays(7)
                };
                url = client.GetPreSignedURL(expiryUrlRequest);
            }
            catch (AmazonS3Exception s3Exception)
            {
                //
            }

            return url;
        }

        public async void UploadStream(MemoryStream stream, string keyName)
        {
            try
            {
                AmazonS3Client client = SetAmazonCredential();
                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    InputStream = stream
                };
                // Put object
                PutObjectResponse response = await client.PutObjectAsync(request);
            }
            catch (AmazonS3Exception s3Exception)
            {
            }
        }


        const string accessKey = "AKIAJ27X2K3HMXOUWROA";
        const string secretKey = "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";
        private static AmazonS3Client SetAmazonCredential()
        {
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var client = new AmazonS3Client(credentials, RegionEndpoint.APSouth1);
            return client;
        }
    }

}