
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Stargate.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Stargate.API.Services
{
    /// <summary>
    /// Azure Blob Storage Service
    /// </summary>
    public class BlobService : IBlobService
    {
        #region| Fields |

        private readonly BlobServiceClient _blobServiceClient;
     
        #endregion

        #region| Constructor |

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="blobServiceClient">BlobServiceClient</param>
        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
           
        }

        #endregion

        #region| Methods |

        /// <summary>
        /// Upload a file to the Azure Storage Account into the image container
        /// </summary>
        /// <param name="blobContainerName">blobContainerName</param>
        /// <param name="content">file stream content</param>
        /// <param name="contentType">contentType</param>
        /// <param name="fileName">fileName</param>
        /// <returns>Uri</returns>
        ///  public async Task<FileReference> UploadAsync(IFormFile file)
        public async Task<FileReference> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName)
        {
            var containerClient = GetContainerClient(blobContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });
            Uri uri = blobClient.Uri;
           
            var response = new FileReference
            {
               Uri=uri.AbsoluteUri
            };
            return response;

        }

        /// <summary>
        /// Delete a file to the Azure Storage Account into the image container
        /// </summary>
        /// <param name="blobContainerName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task DeleteFileBlobAsync(string blobContainerName, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);

            await containerClient.DeleteBlobIfExistsAsync(fileName);
        }

        /// <summary>
        /// Get the container client
        /// </summary>
        /// <param name="blobContainerName"></param>
        /// <returns>BlobContainerClient</returns>
        private BlobContainerClient GetContainerClient(string blobContainerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);

            containerClient.CreateIfNotExists(PublicAccessType.Blob);

            return containerClient;
        }

        #endregion
    }
}