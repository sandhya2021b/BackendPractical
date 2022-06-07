using IPVerification.Services.GeoIPService;
using IPVerification.Services.RDAPService;
using IPVerification.Services.ReverseDNS;
using IPVerification.Services.PingService;
using System.Net;
using System.Net.Sockets;
using IPVerification.Services.Models;

namespace IPVerification.Services
{
    public class IPVerificationService : IIPVerificationService
    {
        private readonly IGeoIPService _geoIPService;
        private readonly IRDAPService _rdapService;
        private readonly IReverseDnsService _reverseDnsService;
        private readonly PingService.PingService _pingService;
        public IPVerificationService(IGeoIPService geoIPService, IRDAPService rdapService,
            IReverseDnsService reverseDnsService, PingService.PingService pingService)
        {
            _geoIPService = geoIPService ?? throw new ArgumentNullException(nameof(geoIPService));
            _rdapService = rdapService ?? throw new ArgumentNullException(nameof(rdapService));
            _reverseDnsService = reverseDnsService ?? throw new ArgumentNullException(nameof(reverseDnsService));
            _pingService = pingService ?? throw new ArgumentNullException(nameof(pingService)); ;
        }
        public async Task<bool> IsValidIpAddress(string ipAddress)
        {
            bool flag = false;
            try
            {
                string IPv = string.Empty;
                IPAddress address;
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    if (ipAddress.Count(c => c == '.') == 3)
                    {
                        flag = IPAddress.TryParse(ipAddress, out address);
                        IPv = "IPv4";
                    }
                    else if (ipAddress.Contains(':'))
                    {
                        if (IPAddress.TryParse(ipAddress, out address))
                        {
                            flag = address.AddressFamily == AddressFamily.InterNetworkV6;
                        }
                        IPv = "IPv6";
                    }
                    else
                    {
                        IPv = "Version of";
                        flag = false;
                    }
                }
            }
            catch (Exception) { }
            return flag;
        }

        public async Task<bool> IsValidDomainName(string domainName)
        {
            return Uri.CheckHostName(domainName) != UriHostNameType.Unknown;
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
