using IPVerification.Services.GeoIPService;
using IPVerification.Services.Models;
using IPVerification.Services.RDAPService;
using IPVerification.Services.ReverseDNS;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace IPVerification.Services
{
    public class IPVerificationService : IIPVerificationService
    {
        private readonly IGeoIPService _geoIPService;
        private readonly IRDAPService _rdapService;
        private readonly IReverseDnsService _reverseDnsService;
        private readonly PingService.PingService _pingService;
        private static readonly Regex validIpV4AddressRegex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]).){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", RegexOptions.IgnoreCase);
        public IPVerificationService(IGeoIPService geoIPService, IRDAPService rdapService, IReverseDnsService reverseDnsService, PingService.PingService pingService)
        {
            _geoIPService = geoIPService ?? throw new ArgumentNullException(nameof(geoIPService));
            _rdapService = rdapService ?? throw new ArgumentNullException(nameof(rdapService));
            _reverseDnsService = reverseDnsService ?? throw new ArgumentNullException(nameof(reverseDnsService));
            _pingService = pingService ?? throw new ArgumentNullException(nameof(pingService)); ;
        }
        public async Task<bool> IsValidIpAddress(string ipAddress)
        { 
            try
            {
                if (validIpV4AddressRegex.IsMatch(ipAddress.Trim()))
                    return true;

                IPAddress ip;
                if(IPAddress.TryParse(ipAddress, out ip))
                {
                    if(ip.AddressFamily == AddressFamily.InterNetworkV6)
                        return true;
                }
            }
            catch (Exception) { }
            return false;
        }

        public bool IsValidDomainName(string domainName)
        {
            bool isDomainExist = false;
            IPHostEntry host;
            try
            {
                host = Dns.GetHostEntry(domainName);
                if (host != null)
                {
                    isDomainExist = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Host not exists")
                {
                    isDomainExist = false;
                }
            }
            return isDomainExist;
        }
        public async Task<VerificationResponseModel> GetIpDetails(string ipAddress, string domainName)
        {
            VerificationResponseModel responseModel = new VerificationResponseModel();

            if (!string.IsNullOrEmpty(ipAddress))
            {
                responseModel.GeoIpResponse = await _geoIPService.GetGeoIP(ipAddress);

                responseModel.Hostname = await _reverseDnsService.GetHostName(ipAddress);                

                var isPingSuccess = await _pingService.PingHost(ipAddress);

                if (isPingSuccess)
                    responseModel.PingStatus = "Success";
                else
                    responseModel.PingStatus = "Failed";

            }
            if (!string.IsNullOrEmpty(domainName))
            {
                responseModel.RdapResponse = await _rdapService.GetRdapDetails(domainName);
            }
            return responseModel;
        }
    }
}
