namespace RDAPService.API.Services.Verisign.Models
{
    public class VerisignResponseModel
    {
        public string ObjectClassName { get; set; }
        public string Handle { get; set; }
        public string LdhName { get; set; }
        public List<Link> Links { get; set; }
        public List<string> Status { get; set; }
        public List<Entity> Entities { get; set; }
        public List<Event> Events { get; set; }
        public Securedns SecureDNS { get; set; }
        public List<Nameserver> Nameservers { get; set; }
        public List<string> RdapConformance { get; set; }
        public List<Notice> Notices { get; set; }
    }
    public class Securedns
    {
        public bool DelegationSigned { get; set; }
    }

    public class Link
    {
        public string Value { get; set; }
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Type { get; set; }
    }

    public class Entity
    {
        public string ObjectClassName { get; set; }
        public string Handle { get; set; }
        public List<string> Roles { get; set; }
        public List<Publicid> PublicIds { get; set; }
        public List<object> VcardArray { get; set; }
        public List<Entity> Entities { get; set; }
    }

    public class Publicid
    {
        public string Type { get; set; }
        public string Identifier { get; set; }
    }
    public class Event
    {
        public string EventAction { get; set; }
        public DateTime EventDate { get; set; }
    }

    public class Nameserver
    {
        public string ObjectClassName { get; set; }
        public string LdhName { get; set; }
    }

    public class Notice
    {
        public string Title { get; set; }
        public List<string> Description { get; set; }
        public List<Link> Links { get; set; }
    }
}
