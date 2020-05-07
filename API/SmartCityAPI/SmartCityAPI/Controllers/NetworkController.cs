using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Model.DTO;
using Protocol;
using SmartCityAPI.DAO;

namespace SmartCityAPI.Controllers
{
    [Route("api/networks")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        private IConfiguration _configuration;
        private INetworkDAO _networkDAO;

        public NetworkController(INetworkDAO networkDAO, IConfiguration configuration)
        {
            _configuration = configuration;
            _networkDAO = networkDAO;
        }

        // GET: api/Network
        [HttpGet]
        public async Task<IActionResult> GetNetworksAsync()
        {
            return new ObjectResult(await _networkDAO.FindAll());
        }

        // GET: api/Network/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetAsync(int id)
        {
            NetworkDTO user = await _networkDAO.FindById(id);

            if (user == null)
            {
                return new NotFoundResult();
            }

            return new ObjectResult(user);
        }

        // POST: api/Network
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm(Name = "image")] IFormFile imageFile, [FromForm] PostNetworkRequest request)
        {
            System.Diagnostics.Trace.WriteLine("FOOOO");
            var storageConnectionString = _configuration["ConnectionStrings:AzureStorageConnectionString"];
            System.Diagnostics.Trace.WriteLine("request : ");
            System.Diagnostics.Trace.WriteLine(imageFile == null);
            if (CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
            {
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("networkimages");
                await container.CreateIfNotExistsAsync();

                var permissions = container.GetPermissions();
                if (permissions.PublicAccess == BlobContainerPublicAccessType.Off)
                {
                    permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                    container.SetPermissions(permissions);
                }

                var blob = container.GetBlockBlobReference(imageFile.FileName);

                await blob.UploadFromStreamAsync(imageFile.OpenReadStream());

                string uri = blob.Uri.ToString();


                NetworkDTO network = new NetworkDTO
                {
                    Name = request.Name,
                    Description = request.Description,
                    AuthorId = request.AuthorId,
                    ImageUrl = uri
                };

                await _networkDAO.Insert(network);

                return new OkObjectResult(network);
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
