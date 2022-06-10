using ReverseDnsService.API.Configuration;
using ReverseDnsService.API.Services.Models;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace ReverseDnsService.API.Services.HackerTarget
{
    public class HackerTargetService: HttpService, IHackerTargetService
    {
        private readonly IConfiguration _configuration;
        private readonly HackerTargetServiceConfiguration _hackerTargetServiceServiceConfiguration;
        private readonly ILogger<HackerTargetService> _logger;
        private static readonly Regex validIpV4AddressRegex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]).){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", RegexOptions.IgnoreCase);
        public HackerTargetService(IConfiguration configuration, HttpClient client, ILogger<HackerTargetService> logger) : base(client, logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _hackerTargetServiceServiceConfiguration = _configuration.GetSection("ServicesConfiguration:HackerTargetServiceConfiguration").Get<HackerTargetServiceConfiguration>();
        }
        public async Task<bool> IsValidIpAddress(string ipAddress)
        {
            try
            {
                if (validIpV4AddressRegex.IsMatch(ipAddress.Trim()))
                    return true;

                IPAddress ip;
                if (IPAddress.TryParse(ipAddress, out ip))
                {
                    if (ip.AddressFamily == AddressFamily.InterNetworkV6)
                        return true;
                }
            }
            catch (Exception) { }
            return false;
        }
        public async Task<DNSResponse> GetDnsFromHackerTarget(string ipAddress)
        {
            try
            {
                var url = $"{_hackerTargetServiceServiceConfiguration.EndPoint}{ipAddress}";
                var response = await Get(url);

                if (response != null && response.ToLower() != "no records found")
                    return new DNSResponse { HostName = response.Replace(ipAddress, "").Trim() };                 
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occcurred while executing [{nameof(HackerTargetServiceConfiguration)}] [{nameof(GetDnsFromHackerTarget)}] method. " +
                    $"ERROR MESSAGE:{ex.Message} INNER EXCEPTION: {ex.InnerException?.Message} STACKTRACE: {ex.StackTrace}");
            }
            return new DNSResponse();
        }
    }
}
