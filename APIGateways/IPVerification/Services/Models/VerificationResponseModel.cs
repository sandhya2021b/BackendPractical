namespace IPVerification.Services.Models
{
    public class VerificationResponseModel
    {
        public GeoIPModel GeoIpResponse { get; set; }
        public string? Hostname { get; set; }
        public string? PingStatus { get; set; }
        public RdapResponseModel RdapResponse { get; set; }
    }
}
