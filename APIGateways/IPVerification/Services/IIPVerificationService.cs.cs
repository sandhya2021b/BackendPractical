using IPVerification.Services.Models;

namespace IPVerification.Services
{
    public interface IIPVerificationService
    {
        Task<bool> IsValidIpAddress(string ipAddress);
        bool IsValidDomainName(string domainName);
        Task<VerificationResponseModel> GetIpDetails(string ipAddress, string domainName);
    }
}
