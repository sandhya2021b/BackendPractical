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

        [HttpGet("/{ip}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<DNSResponse>> GetRevernseDNS([FromRoute] string ip)
        { 
            var response = await _hackertargetService.GetDnsFromHackerTarget(ip);

            return Ok(response);
        }

    }
}
