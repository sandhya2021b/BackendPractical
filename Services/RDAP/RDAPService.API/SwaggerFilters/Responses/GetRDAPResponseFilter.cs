using Swashbuckle.AspNetCore.Filters;
using RDAPService.API.Services.Verisign.Models;

namespace RDAPService.API.SwaggerFilters.Responses
{
    public class GetRDAPResponseFilter : IExamplesProvider<VerisignResponseModel>
    {
        public VerisignResponseModel GetExamples()
        {
            return new VerisignResponseModel();
        }
    }
}
