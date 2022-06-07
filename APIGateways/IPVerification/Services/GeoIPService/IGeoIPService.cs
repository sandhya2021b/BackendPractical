using IPVerification.Services.Models;

namespace IPVerification.Services.GeoIPService
{
    public interface IGeoIPService
    {
        Task<GeoIPModel> GetGeoIP(string ipAddress);
    }
}
