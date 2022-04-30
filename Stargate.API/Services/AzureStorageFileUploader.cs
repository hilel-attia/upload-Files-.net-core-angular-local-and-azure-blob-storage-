using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Stargate.API.Models;

namespace Stargate.API.Services
{
    public class AzureStorageFileUploader : IFileUploader
    {
        private readonly string AccountKey = "AccountName";
        private readonly string CONTAINER_NAME = "queneyimages";

        private readonly IConfiguration _configuration;
        public AzureStorageFileUploader(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<FileReference> UploadAsync(IFormFile file)
        {
            var storageAccountKey = _configuration[AccountKey];

            if (storageAccountKey == null)
            {
                throw new ArgumentNullException(nameof(storageAccountKey), $"You must set an environment variable for {AccountKey}");
            }

            //Connect to Azure
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageAccountKey);
            // Create a reference to the file client.
            CloudFileClient cloudFileClient = storageAccount.CreateCloudFileClient();

            // Create a reference to the Azure path
            CloudFileShare cloudFileShare = cloudFileClient.GetShareReference(CONTAINER_NAME);


            //Create a reference to the filename that you will be uploading
            CloudFile cloudFile = cloudFileShare
                .GetRootDirectoryReference()
                .GetDirectoryReference("uploads")
                .GetFileReference(file.FileName);

            //Open a stream from a local file.
            Stream fileStream = file.OpenReadStream();

            //Upload the file to Azure.
            await cloudFile.UploadFromStreamAsync(fileStream);
            fileStream.Dispose();

            var response = new FileReference
            {
                FileName = file.FileName,
                Uri = cloudFile.Uri.AbsoluteUri,
                Success = true
            };
            return response;
        }
    }
}
