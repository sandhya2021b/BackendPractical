namespace GeoIPService.API.Services.Models
{
    public class GeoIPResponse
    {
        public string Ip { get; set; }
        public bool? Bogon { get; set; }
        public string? Hostname { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        public string? Loc { get; set; }
        public string? Postal { get; set; }
        public string? Timezone { get; set; }
        public string? Org { get; set; }
    }
}
