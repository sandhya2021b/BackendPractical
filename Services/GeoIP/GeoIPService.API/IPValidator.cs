using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace GeoIPService.API
{
    public static class IPValidator
    {
        private static readonly Regex validIpV4AddressRegex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]).){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", RegexOptions.IgnoreCase);
        public static bool ValidateIP(string ipAddress)
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
    }
}
