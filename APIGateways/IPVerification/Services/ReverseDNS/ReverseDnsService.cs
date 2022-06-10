using IPVerification.Services.Models;
using IPVerification.Configuration;

namespace IPVerification.Services.ReverseDNS
{
    public class ReverseDnsService : HttpService, IReverseDnsService
    {
        private readonly IConfiguration _configuration;
        private readonly ReverseDnsServiceConfiguration _reverseDnsServiceConfiguration;
        private readonly ILogger<ReverseDnsService> _logger;
        public ReverseDnsService(IConfiguration configuration, HttpClient client, ILogger<ReverseDnsService> logger) : base(client, logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _reverseDnsServiceConfiguration = _configuration.GetSection("ServicesConfiguration:ReverseDnsServiceConfiguration").Get<ReverseDnsServiceConfiguration>();
        }
        public async Task<string> GetHostName(string ipAddress)
        {
            try
            {
                var url = $"{_reverseDnsServiceConfiguration.EndPoint}{ipAddress}";

                var response = await Get<DNSResponse>(url);

                return response.HostName;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occcurred while executing [{nameof(ReverseDnsService)}] [{nameof(GetHostName)}] method. " +
                    $"ERROR MESSAGE:{ex.Message} INNER EXCEPTION: {ex.InnerException?.Message} STACKTRACE: {ex.StackTrace}");
            }
            return String.Empty;
        }
    }
}
