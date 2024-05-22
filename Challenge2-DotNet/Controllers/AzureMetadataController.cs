using Challenge2_DotNet.Functions;
using Challenge2_DotNet.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Challenge2_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureMetadataController : ControllerBase
    {

        private readonly ILogger<AzureMetadataController> _logger;
        private readonly ITokenGeneration _tokenGeneration;
        private readonly IRetrieveMetadata _retrieveMetadata;
        public AzureMetadataController(ILogger<AzureMetadataController> logger, ITokenGeneration tokenGeneration, IRetrieveMetadata retrieveMetadata)
        {
            _logger = logger;
            _tokenGeneration = tokenGeneration;
            _retrieveMetadata = retrieveMetadata;
        }

        [HttpPost]
        public string Post(Request metaDataRequest) {

            if (ModelState.IsValid)
            {
                string token = string.Empty;
                string metadataValue = string.Empty;

                try
                {
                    token = _tokenGeneration.generateTokenAsync(metaDataRequest).Result;

                    _logger.LogInformation("Generated token successfully");

                    if (!string.IsNullOrEmpty(token))
                    {
                        metadataValue = _retrieveMetadata.retrieveMetadata(metaDataRequest, token).Result;

                        _logger.LogInformation("Retrieved metadata successfully");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occured while processing the request");
                }

                return metadataValue;
            }
            else
            {
                throw new Exception("Invalid Payload");
            }
                
        }
    }
}
