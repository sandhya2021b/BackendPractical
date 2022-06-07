using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GeoIPService.API.Services.IPinfo;
using GeoIPService.API.Services.Models;
using System.Net;
using GeoIPService.API;

namespace GeoIPService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoIPController : ControllerBase
    {
        private readonly IIPinfoService _ipInfoService;
        public GeoIPController(IIPinfoService iPinfoService)
        {
            _ipInfoService = iPinfoService ?? throw new ArgumentNullException(nameof(iPinfoService));
        }

        [HttpGet("/{ip}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GeoIPResponse>> GetGeoIP([FromRoute] string ip)
        {
            if (!IPValidator.ValidateIP(ip))
                return BadRequest("Invalid IP Address");

            var response = await _ipInfoService.GetGeoIP(ip);

            return Ok(response);
        }
    }
}
