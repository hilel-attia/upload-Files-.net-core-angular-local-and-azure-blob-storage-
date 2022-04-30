using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stargate.API.Data;
using Stargate.API.ViewModels;
using AutoMapper;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Stargate.API.Data.Repository;
using Stargate.API.Models;
using Stargate.API.Services;
using System.Net;

namespace Stargate.API.Controllers
{
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private readonly IStargateRepository _repo;
        private readonly ILogger<FilesController> _logger;
        private readonly IMapper _mapper;
   //   private readonly IFileUploader _fileUploader;
        private readonly IBlobService service;

        public FilesController(IStargateRepository repo, 
            ILogger<FilesController> logger,
            IMapper mapper,
            IBlobService blobService

            //StargateContext context, 
           //FileUploader fileUploader
            )
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
          //_fileUploader = fileUploader ?? throw new ArgumentNullException(nameof(fileUploader));
            service = blobService;
        }
        // GET api/files
        /*HttpGet]
          public IActionResult Get()
          {
              try
              {
                  return Ok(
                      _mapper.Map<IEnumerable<Data.Entities.File>, IEnumerable<FileViewModel>>(
                          (IEnumerable<Data.Entities.File>)_repo.GetFiles()));
              }
              catch (Exception ex)
              {
                  _logger.LogError($"Failed to get files: {ex}");
                  return StatusCode(500, "Failed to get files");
              }
          }*/

    
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Data.Entities.File>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Data.Entities.File>>> GetProducts()
        {
            var products = await _repo.GetFiles();
            return Ok(products);
        }



        // GET api/files/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest($"{nameof(id)} should be greater than 0");
                }
                var dbFile = await _repo.GetFileByIdAsync(id);

                if (dbFile == null)
                {
                    return NotFound($"Unable to locate a file given id: '{id}'");
                }

                return Ok(_mapper.Map<Data.Entities.File, FileViewModel>(dbFile));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get file '{id}': {ex}");
                return StatusCode(500, $"Failed to get file '{id}'");
            }
        }

        // POST api/files
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
                if (file == null || file.Length == 0)
                    return BadRequest("No file was provided");

                var fileUploadResult = await service.UploadFileBlobAsync("images",
                    file.OpenReadStream(),
                    file.ContentType,
                    file.FileName);

                var fileEntity = CreateFile(file, fileUploadResult);

                var response = new UploadResponseViewModel
                {
                    Success = true,
                    FileName = fileEntity.FileName,
                    Uri = fileEntity.ExternalUri

                };


        

         await _repo.AddFileAsync(fileEntity);

                return Ok(response);
            
           
        }

        private Data.Entities.File CreateFile(IFormFile file, FileReference reference)
        {
            var fileEntity = new Data.Entities.File
            {
                FileName = file.FileName,
                FileExtension = Path.GetExtension(file.FileName),
                ContentType = file.ContentType,
                ExternalUri = reference.Uri,
                FileSizeBytes = file.Length
            };

            return fileEntity;
        }
    }
}