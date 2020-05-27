using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Model.Database;
using Model.DTO;
using Protocol;
using SmartCityAPI.DAO;

namespace SmartCityAPI.Controllers
{
    [Route("api/offers")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private IConfiguration _configuration;
        private IOfferDAO _offerDAO;
        private IUserDAO _userDAO;

        public OfferController(IOfferDAO offerDAO, IUserDAO userDAO, IConfiguration configuration)
        {
            _configuration = configuration;
            _offerDAO = offerDAO;
            _userDAO = userDAO;
        }

        // GET: api/Offer/5
        [HttpGet("{tradeId}", Name = "GetByTrade")]
        public async Task<IActionResult> GetByTrade(int tradeId)
        {
            IEnumerable<OfferDTO> offers = await _offerDAO.FindOffersByTrade(tradeId);

            return new OkObjectResult(offers);
        }

        // GET: api/Offer?userId=1
        [HttpGet(Name = "GetByUser")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            IEnumerable<OfferDTO> allOffers = await _offerDAO.FindAll();

            if (userId == 0)
            {
                return new OkObjectResult(allOffers);
            }

            List<OfferDTO> result = new List<OfferDTO>();
            UserDTO user = await _userDAO.FindById(userId);

            foreach (int interest in user.Interests)
            {
                foreach (OfferDTO offer in allOffers)
                {
                    if (offer.Target.Contains(interest))
                    {
                        result.Add(offer);
                    }
                }
            }

            return new OkObjectResult(result);
        }

        // POST: api/Offer
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm(Name = "image")] IFormFile imageFile, [FromForm] PostOfferRequest request)
        {
            var storageConnectionString = _configuration["ConnectionStrings:AzureStorageConnectionString"];

            if (CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
            {
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("offerimages");
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

                OfferDTO offer = new OfferDTO
                {
                    TradeId = request.TradeId,
                    Title = request.Title,
                    Description = request.Description,
                    ImageUrl = uri
                };

                OfferDTO insertedOffer = await _offerDAO.Insert(offer);

                return new OkObjectResult(insertedOffer);
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
