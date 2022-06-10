using IpInfo;
using GeoIPService.API.Services.Models;
using AutoMapper;

namespace GeoIPService.API.Services.IPinfo
{
    public class IPinfoService : IIPinfoService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<IPinfoService> _logger;
        public IPinfoService(IMapper mapper, ILogger<IPinfoService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<GeoIPResponse> GetGeoIP(string ipAddress)
        {
            try
            {
                using var client = new HttpClient();
                var api = new IpInfoApi(client);

                var response = await api.GetInformationByIpAsync(ipAddress);

                return _mapper.Map<GeoIPResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occcurred while executing [{nameof(IPinfoService)}] [{nameof(GetGeoIP)}] method. " +
                    $"ERROR MESSAGE:{ex.Message} INNER EXCEPTION: {ex.InnerException?.Message} STACKTRACE: {ex.StackTrace}");
            }
            return new GeoIPResponse();
        }        
    }
}
