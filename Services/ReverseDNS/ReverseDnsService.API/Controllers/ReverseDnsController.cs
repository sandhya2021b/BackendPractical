using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReverseDnsService.API.Services.HackerTarget;
using ReverseDnsService.API.Services.Models;
using System.Net;

namespace ReverseDnsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReverseDnsController : ControllerBase
    {
        private readonly IHackerTargetService _hackertargetService;
        public ReverseDnsController(IHackerTargetService hackertargetService)
        {
            _hackertargetService = hackertargetService ?? throw new ArgumentNullException(nameof(hackertargetService));
        }

        /// <summary>
        /// Gets details of DNS for given IpAddress
        /// </summary>
        /// <param name="IP Address">IP Address as string</param>
        /// <response code="200">Gets DNS details for given IpAddress</response>
        /// <response code="400">Failed to fetch DNS details for the input provided</response>
        [HttpGet("/{ip}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DNSResponse), statusCode:200)]
        [ProducesResponseType(typeof(ErrorResponse), statusCode:400)]
        public async Task<ActionResult<DNSResponse>> GetRevernseDNS([FromRoute] string ip)
        {
            if (!string.IsNullOrEmpty(ip) && !_hackertargetService.IsValidIpAddress(ip).Result)
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel> { new ErrorModel { Message = "Invalid input IP Address" } }
                });

            var response = await _hackertargetService.GetDnsFromHackerTarget(ip);

            return Ok(response);
        }

    }
}
