using System.Net;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("/{domain}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<VerisignResponseModel>> GetRDAPDetails([FromRoute] string domain)
        {
            var response = await _verisignService.GetRDAPfromVerisign(domain);

            return Ok(response);
        }
    }
}
