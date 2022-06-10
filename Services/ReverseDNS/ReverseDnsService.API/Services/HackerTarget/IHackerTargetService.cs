using ReverseDnsService.API.Services.Models;

namespace ReverseDnsService.API.Services.HackerTarget
{
    public interface IHackerTargetService
    {
        Task<bool> IsValidIpAddress(string ipAddress);
        Task<DNSResponse> GetDnsFromHackerTarget(string domainName);
    }
}
