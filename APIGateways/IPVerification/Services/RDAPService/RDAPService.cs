using IPVerification.Configuration;
using IPVerification.Services.Models;

namespace IPVerification.Services.RDAPService
{
    public class RDAPService : HttpService, IRDAPService
    {
        private readonly IConfiguration _configuration;
        private readonly RDAPServiceConfiguration _rdapServiceConfiguration;
        private readonly ILogger<RDAPService> _logger;
        public RDAPService(IConfiguration configuration, HttpClient client, ILogger<RDAPService> logger) : base(client, logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _rdapServiceConfiguration = _configuration.GetSection("ServicesConfiguration:RDAPServiceConfiguration").Get<RDAPServiceConfiguration>();
        }
        public async Task<RdapResponseModel> GetRdapDetails(string domainName)
        {
            try
            {
                var url = $"{_rdapServiceConfiguration.EndPoint}{domainName}";
                var response = await Get<RdapResponseModel>(url);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occcurred while executing [{nameof(RDAPService)}] [{nameof(GetRdapDetails)}] method. " +
                    $"ERROR MESSAGE:{ex.Message} INNER EXCEPTION: {ex.InnerException?.Message} STACKTRACE: {ex.StackTrace}");
            }
            return new RdapResponseModel();
        }
    }
}
