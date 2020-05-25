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
    [Route("api/trades")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private IConfiguration _configuration;
        private ITradeDAO _tradeDAO;

        public TradeController(ITradeDAO tradeDAO, IConfiguration configuration)
        {
            _configuration = configuration;
            _tradeDAO = tradeDAO;
        }

        // GET: api/Trade/5
        [HttpGet("{id}", Name = "GetTrade")]
        public async Task<IActionResult> GetAsync(int id)
        {
            TradeDTO trade = await _tradeDAO.FindById(id);

            if (trade == null)
            {
                return new NotFoundResult();
            }

            return new ObjectResult(trade);
        }

        // POST: api/Trade
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm(Name = "image")] IFormFile imageFile, [FromForm] PostTradeRequest request)
        {
            System.Diagnostics.Trace.WriteLine("fooo");
            var storageConnectionString = _configuration["ConnectionStrings:AzureStorageConnectionString"];

            if (CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
            {
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("tradeimages");
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

                TradeDTO trade = new TradeDTO
                {
                    OwnerId = request.OwnerId,
                    Name = request.Name,
                    Description = request.Description,
                    ImageUrl = uri,
                    Address = request.Address,
                    Town = request.Town
                };

                TradeDTO insertedTrade = await _tradeDAO.Insert(trade);

                return new OkObjectResult(insertedTrade);
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
