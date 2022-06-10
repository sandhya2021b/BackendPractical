using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GeoIPService.API.Services.IPinfo;
using GeoIPService.API.Services.Models;
using System.Net;
using GeoIPService.API;
using GeoIPService.API.Services;

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

        /// <summary>
        /// Gets details of Geolocation for given IpAddress
        /// </summary>
        /// <param name="ip">IP address as string</param>
        /// <response code="200">Gets Geolocation for given IpAddress</response>
        /// <response code="400">Failed to fetch Geolocation details for the input provided</response>
        [HttpGet("/{ip}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GeoIPResponse), statusCode:200)]
        [ProducesResponseType(typeof(ErrorResponse), statusCode:400)]
        public async Task<ActionResult<GeoIPResponse>> GetGeoIP([FromRoute] string ip)
        {
            if (!IPValidator.ValidateIP(ip))
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel> { new ErrorModel { Message = "Invalid input IP Address" } }
                });

            var response = await _ipInfoService.GetGeoIP(ip);

            return Ok(response);
        }
    }
}
