using IPVerification.Services.Models;

namespace IPVerification.Services.RDAPService
{
    public interface IRDAPService
    {
        Task<RdapResponseModel> GetRdapDetails(string domainName);
    }
}
