using System.Net;
using Microsoft.AspNetCore.Mvc;
using RDAPService.API.Services;
using RDAPService.API.Services.Verisign;
using RDAPService.API.Services.Verisign.Models;

namespace RDAPService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RDAPController : ControllerBase
    {
        private readonly IVerisignService _verisignService;
        public RDAPController(IVerisignService verisignService)
        {
            _verisignService = verisignService ?? throw new ArgumentNullException(nameof(verisignService));
        }

        /// <summary>
        /// Gets details of Registration Data Acess Protocal for given IpAddress
        /// </summary>
        /// <param name="domain name">domain name as string</param>
        /// <response code="200">Gets RDAP details for given IpAddress</response>
        /// <response code="400">Failed to fetch RDAP details for the input provided</response>
        [HttpGet("/{domain}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(VerisignResponseModel), statusCode:200)]
        [ProducesResponseType(typeof(ErrorResponse), statusCode:400)]
        public async Task<ActionResult<VerisignResponseModel>> GetRDAPDetails([FromRoute] string domain)
        {
            if (!string.IsNullOrEmpty(domain) && !IsValidDomainName(domain))
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel> { new ErrorModel { Message = "Invalid input domain name" } }
                });

            var response = await _verisignService.GetRDAPfromVerisign(domain);

            return Ok(response);
        }

        private bool IsValidDomainName(string domainName)
        {
            bool isDomainExist = false;
            System.Net.IPHostEntry host;
            try
            {
                host = System.Net.Dns.GetHostEntry(domainName);
                if (host != null)
                {
                    isDomainExist = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Host not exists")
                {
                    isDomainExist = false;
                }
            }

            return isDomainExist;
        }
    }
}
