using ReverseDnsService.API.Configuration;
using ReverseDnsService.API.Services.Models;

namespace ReverseDnsService.API.Services.HackerTarget
{
    public class HackerTargetService: HttpService, IHackerTargetService
    {
        private readonly IConfiguration _configuration;
        private readonly HackerTargetServiceConfiguration _hackerTargetServiceServiceConfiguration;
        private readonly ILogger<HackerTargetService> _logger;
        public HackerTargetService(IConfiguration configuration,
            System.Net.Http.HttpClient client,
            ILogger<HackerTargetService> logger) : base(client, logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _hackerTargetServiceServiceConfiguration = _configuration.GetSection("ServicesConfiguration:HackerTargetServiceConfiguration").Get<HackerTargetServiceConfiguration>();
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
