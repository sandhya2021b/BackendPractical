namespace IPVerification.Services.Models
{
    public class VerificationInputModel
    {
        public string? IpAdderss { get; set; }
        public string? DomainName { get; set; }
        public List<string>? ServiceNames { get; set; }
    }
}
