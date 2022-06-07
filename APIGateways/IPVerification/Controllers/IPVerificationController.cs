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

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<VerificationResponseModel>> GetDetails([FromBody] VerificationInputModel input)
        {
            if(input == null || (string.IsNullOrEmpty(input.IpAdderss) && string.IsNullOrEmpty(input.DomainName)))
                return BadRequest("Invalid input");

            if (!string.IsNullOrEmpty(input.IpAdderss) && !_ipVerificationService.IsValidIpAddress(input.IpAdderss).Result)
                return BadRequest("Invalid input IP Address");

            if (!string.IsNullOrEmpty(input.DomainName) && !_ipVerificationService.IsValidDomainName(input.DomainName).Result)
                return BadRequest("Invalid input domain name");

            var response = await _ipVerificationService.GetIpDetails(ipAddress: input.IpAdderss, domainName: input.DomainName);

            return Ok(response);
        }
    }
}
