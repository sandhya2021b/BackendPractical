using Swashbuckle.AspNetCore.Filters;
using IPVerification.Services.Models;

namespace IPVerification.SwaggerFilters.Requests
{
    public class IPVerificationGetDetailsRequestFilter : IExamplesProvider<VerificationInputModel>
    {
        public VerificationInputModel GetExamples()
        {
            return new VerificationInputModel{
                IpAdderss = "some IP address",
                DomainName = "some domain name",
                ServiceNames = new List<string>(new string[] {"GeoIP", "RDAP", "ReverseDNS", "PingStatus" })
            };
        }
    }
}
