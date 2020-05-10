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
    [Route("api/networks")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        private IConfiguration _configuration;
        private INetworkDAO _networkDAO;
        private IPublicationDAO _publicationDAO;
        private IUserDAO _userDAO;
        private ISubscriptionDAO _subscriptionDAO;

        public NetworkController(INetworkDAO networkDAO, IPublicationDAO publicationDAO, IUserDAO userDAO, ISubscriptionDAO subscriptionDAO, IConfiguration configuration)
        {
            _configuration = configuration;
            _networkDAO = networkDAO;
            _publicationDAO = publicationDAO;
            _userDAO = userDAO;
            _subscriptionDAO = subscriptionDAO;
        }

        // GET: api/Network
        [HttpGet]
        public async Task<IActionResult> GetNetworksAsync()
        {
            IEnumerable<NetworkDTO> networks = await _networkDAO.FindAll();

            foreach (NetworkDTO network in networks)
            {
                IEnumerable<SubscriptionDTO> subscriptions = await _subscriptionDAO.findByNetworkIdAsync(network.Id);
                
                foreach (SubscriptionDTO subscription in subscriptions)
                {
                    UserDTO user = await _userDAO.FindById(subscription.UserId);
                    subscription.UserName = user.FirstName + " " + user.LastName;
                }

                network.subscriptions = subscriptions;
            }

            return new OkObjectResult(networks);
        }

        // GET: api/Network/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetNetworkAsync(int id)
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
        public async Task<IActionResult> InsertNetwork([FromForm(Name = "image")] IFormFile imageFile, [FromForm] PostNetworkRequest request)
        {
            var storageConnectionString = _configuration["ConnectionStrings:AzureStorageConnectionString"];

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
                    Id = (await _networkDAO.FindAll()).Count(),
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

        // POST: api/Network/5/publications
        [HttpPost("{id}/publications")]
        public async Task<IActionResult> InsertPublication([FromBody] PublicationDTO publication, int id)
        {
            publication.NetworkId = id;

            PublicationDTO insertedPublication = await _publicationDAO.Insert(publication);

            return new OkObjectResult(insertedPublication);
        }

        // GET: api/Network/5/publications
        [HttpGet("{id}/publications", Name ="GetNetworkPublications")]
        public async Task<IActionResult> GetPublicationsAsync(int id)
        {
            IEnumerable<PublicationDTO> publications = await _publicationDAO.FindByNetworkId(id);

            foreach (PublicationDTO publication in publications)
            {
                UserDTO user = await _userDAO.FindById(publication.AuthorId);
                publication.AuthorName = user.FirstName + " " + user.LastName;
            }

            return new ObjectResult(publications);
        }

        // POST: api/Network/5/subscribe
        [HttpPost("{id}/subscriptions")]
        public async Task<IActionResult> Subscribe([FromBody] SubscriptionDTO subscription, int id)
        {
            subscription.NetworkId = id;

            SubscriptionDTO insertedPublication = await _subscriptionDAO.insertAsync(subscription);

            return new OkObjectResult(insertedPublication);
        }

                // POST: api/Network/5/subscribe
        [HttpPut("{id}/subscriptions/{subscriptionId}")]
        public async Task<IActionResult> UpdateSubscription([FromBody] SubscriptionDTO subscription, int id, int subscriptionId)
        {
            subscription.Id = subscriptionId;
            subscription.NetworkId = id;

            // TODO : Throw error if id is different of user's id
            bool result = await _subscriptionDAO.Update(subscription);

            if (! result)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return new OkObjectResult(subscription);
        }
    }
}
