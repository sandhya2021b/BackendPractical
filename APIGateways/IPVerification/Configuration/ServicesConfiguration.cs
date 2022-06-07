namespace IPVerification.Configuration
{
    public class ServicesConfiguration
    {
        public GeoIPServiceConfiguration GeoIPServiceConfiguration { get; set; }
        public RDAPServiceConfiguration RDAPServiceConfiguration { get; set; }
        public ReverseDnsServiceConfiguration ReverseDnsServiceConfiguration { get; set; }
    }
    public class GeoIPServiceConfiguration
    {
        public string EndPoint { get; set; }
    }
    public class RDAPServiceConfiguration
    {
        public string EndPoint { get; set; }
    }
    public class ReverseDnsServiceConfiguration
    {
        public string EndPoint { get; set; }
    }
}
