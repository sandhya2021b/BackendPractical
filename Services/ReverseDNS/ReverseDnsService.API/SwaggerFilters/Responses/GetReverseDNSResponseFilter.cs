using Swashbuckle.AspNetCore.Filters;
using ReverseDnsService.API.Services.Models;

namespace ReverseDnsService.API.SwaggerFilters.Responses
{
    public class GetReverseDNSResponseFilter : IExamplesProvider<DNSResponse>
    {
        public DNSResponse GetExamples()
        {
            return new DNSResponse { 
                HostName = "some host name"
            };
        }
    }
}
