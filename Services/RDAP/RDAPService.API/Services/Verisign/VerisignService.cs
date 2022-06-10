using RDAPService.API.Configuration;
using RDAPService.API.Services.Verisign.Models;

namespace RDAPService.API.Services.Verisign
{
    public class VerisignService : HttpService, IVerisignService
    {
        private readonly IConfiguration _configuration;
        private readonly VerisignServiceConfiguration _verisignServiceConfiguration;
        private readonly ILogger<VerisignService> _logger;
        public VerisignService(IConfiguration configuration, HttpClient client, ILogger<VerisignService> logger) : base(client, logger)
        { 
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _verisignServiceConfiguration = _configuration.GetSection("ServicesConfiguration:VerisignServiceConfiguration").Get<VerisignServiceConfiguration>();
        }
        public async Task<VerisignResponseModel> GetRDAPfromVerisign(string domainName)
        {
            try
            {
                var url = $"{_verisignServiceConfiguration.EndPoint}{domainName}";
                var response = await Get<VerisignResponseModel>(url);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occcurred while executing [{nameof(VerisignService)}] [{nameof(GetRDAPfromVerisign)}] method. " +
                    $"ERROR MESSAGE:{ex.Message} INNER EXCEPTION: {ex.InnerException?.Message} STACKTRACE: {ex.StackTrace}");
            }
            return new VerisignResponseModel();
        }
    }
}
