using System.Net;
using System.Net.Sockets;

namespace GeoIPService.API
{
    public static class IPValidator
    {
        public static bool ValidateIP(string IpAddress)
        {
            bool flag = false;
            try
            {               
                string IPv = string.Empty;
                IPAddress address;
                if (!string.IsNullOrEmpty(IpAddress))
                {
                    if (IpAddress.Count(c => c == '.') == 3)
                    {
                        flag = IPAddress.TryParse(IpAddress, out address);
                        IPv = "IPv4";
                    }
                    else if (IpAddress.Contains(':'))
                    {
                        if (IPAddress.TryParse(IpAddress, out address))
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
    }
}
