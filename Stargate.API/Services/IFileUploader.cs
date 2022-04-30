using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Stargate.API.Models;

namespace Stargate.API.Services
{
    public interface IFileUploader
    {
        Task<FileReference> UploadAsync(IFormFile file);
    }
}
