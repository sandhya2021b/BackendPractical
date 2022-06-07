using RDAPService.API.Services.Verisign.Models;

namespace RDAPService.API.Services.Verisign
{
    public interface IVerisignService
    {
        Task<VerisignResponseModel> GetRDAPfromVerisign(string domainName);
    }
}
