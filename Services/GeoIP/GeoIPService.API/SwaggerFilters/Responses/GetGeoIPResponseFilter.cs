using Swashbuckle.AspNetCore.Filters;
using GeoIPService.API.Services.Models;

namespace GeoIPService.API.SwaggerFilters.Responses
{
    public class GetGeoIPResponseFilter : IExamplesProvider<GeoIPResponse>
    {
        public GeoIPResponse GetExamples()
        {
            return new GeoIPResponse
            {
                Ip = "Ip address",
                Bogon = true,
                Loc = "Location Name",
                Region = "Region Name",
                City = "City Name",
                Country = "Country Name",
                Postal = "Postal code",
                Org = "Organiztion",
                Hostname = "Hostname",
                Timezone = "Timezone"
            };
        }
    }
}
