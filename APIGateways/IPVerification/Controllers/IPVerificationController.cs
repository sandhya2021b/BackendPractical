using IPVerification.Services;
using IPVerification.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IPVerification.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IPVerificationController : ControllerBase
    {
        private readonly IIPVerificationService _ipVerificationService;

        public IPVerificationController(IIPVerificationService iPVerificationService)
        {
            _ipVerificationService = iPVerificationService ?? throw new ArgumentNullException(nameof(iPVerificationService));
        }

        /// <summary>
        /// Gets details of Geolocation, PingStatus, ReverseDNS, RDAP for given IpAddress
        /// </summary>
        /// <response code="200">Gets Geolocation, PingStatus, ReverseDNS, RDAP for given IpAddress</response>
        /// <response code="400">Failed to fetch details for the input provided</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(VerificationResponseModel), statusCode:200)]
        [ProducesResponseType((typeof(ErrorResponse)), statusCode:400)]
        public async Task<ActionResult<VerificationResponseModel>> GetDetails([FromBody] VerificationInputModel input)
        {
            if(input == null || (string.IsNullOrEmpty(input.IpAdderss) && string.IsNullOrEmpty(input.DomainName)))
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel> { new ErrorModel {Message = "Invalid input" } }
                });

            if (!string.IsNullOrEmpty(input.IpAdderss) && !_ipVerificationService.IsValidIpAddress(input.IpAdderss).Result)
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel> { new ErrorModel { Message = "Invalid input IP Address" } }
                });            

            if (!string.IsNullOrEmpty(input.DomainName) && !_ipVerificationService.IsValidDomainName(input.DomainName).Result)
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel> { new ErrorModel { Message = "Invalid input domain name" } }
                }); 

            var response = await _ipVerificationService.GetIpDetails(ipAddress: input.IpAdderss, domainName: input.DomainName);

            return Ok(response);
        }
    }
}
