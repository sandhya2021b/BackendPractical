namespace ReverseDnsService.API.Configuration
{
    public class ServicesConfiguration
    {
        public HackerTargetServiceConfiguration HackerTargetServiceConfiguration;
    }
    public class HackerTargetServiceConfiguration
    {
        public string EndPoint { get; set; }
    }
}
