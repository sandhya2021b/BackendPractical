using GeoIPService.API.Services.Models;

namespace GeoIPService.API.Services.IPinfo
{
    public interface IIPinfoService
    {
        Task<GeoIPResponse> GetGeoIP(string ipAddress);
    }
}
