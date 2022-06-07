using IPVerification.Services.Models;
using IPVerification.Configuration;

namespace IPVerification.Services.GeoIPService
{
    public class GeoIPService : HttpService, IGeoIPService
    {
        private readonly IConfiguration _configuration;
        private readonly GeoIPServiceConfiguration _geoIPServiceConfiguration;
        private readonly ILogger<GeoIPService> _logger;
        public GeoIPService(IConfiguration configuration,
            System.Net.Http.HttpClient client, 
            ILogger<GeoIPService> logger) : base(client, logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _geoIPServiceConfiguration = _configuration.GetSection("ServicesConfiguration:GeoIPServiceConfiguration").Get<GeoIPServiceConfiguration>();
        }
        public async Task<GeoIPModel> GetGeoIP(string ipAddress)
        {
            try
            {
                var url = $"{_geoIPServiceConfiguration.EndPoint}{ipAddress}";
                var response = await Get<GeoIPModel>(url);

                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error occcurred while executing [{nameof(GeoIPService)}] [{nameof(GetGeoIP)}] method. " +
                    $"ERROR MESSAGE:{ex.Message} INNER EXCEPTION: {ex.InnerException?.Message} STACKTRACE: {ex.StackTrace}");
            }
            return new GeoIPModel();
        }
    }
}
