using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;

namespace PensionManagement.Server.Controllers
{
    public class OidcConfigurationController : Controller
    {
        private readonly ILogger<OidcConfigurationController> _logger;

        public OidcConfigurationController(IClientRequestParametersProvider clientRequestParametersProvider, ILogger<OidcConfigurationController> logger)
        {
            ClientRequestParametersProvider = clientRequestParametersProvider;
            _logger = logger;
        }

        public IClientRequestParametersProvider ClientRequestParametersProvider { get; }

        [HttpGet("_configuration/{clientId}")]
        public IActionResult GetClientRequestParameters([FromRoute] string clientId)
        {
            try
            {
                var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
                return Ok(parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while retrieving client parameters for client Id '{clientId}[");
                return StatusCode(StatusCodes.Status500InternalServerError, "n error occured while retrieving client parameters");
            }

        }
    }
}