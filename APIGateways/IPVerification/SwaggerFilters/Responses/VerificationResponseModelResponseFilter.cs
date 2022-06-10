using Swashbuckle.AspNetCore.Filters;
using IPVerification.Services.Models;

namespace IPVerification.SwaggerFilters.Responses
{
    public class VerificationResponseModelResponseFilter : IExamplesProvider<VerificationResponseModel>
    {
        public VerificationResponseModel GetExamples() 
        {
            return new VerificationResponseModel
            {
                GeoIpResponse = new GeoIPModel(),
                Hostname = "some host name",
                PingStatus = "success/failure",
                RdapResponse = new RdapResponseModel ()
            };
        }
    }
}
