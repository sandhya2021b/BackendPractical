using ReverseDnsService.API.Services.Models;

namespace ReverseDnsService.API.Services.HackerTarget
{
    public interface IHackerTargetService
    {
        Task<DNSResponse> GetDnsFromHackerTarget(string domainName);
    }
}
