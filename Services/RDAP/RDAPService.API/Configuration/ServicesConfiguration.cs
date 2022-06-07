namespace RDAPService.API.Configuration
{
    public class ServicesConfiguration
    {
        public VerisignServiceConfiguration VerisignService { get; set; }
    }
    public class VerisignServiceConfiguration
    {
        public string EndPoint { get; set; }
    }
}
