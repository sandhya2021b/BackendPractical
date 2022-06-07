namespace IPVerification.Services.ReverseDNS
{
    public interface IReverseDnsService
    {
        Task<string> GetHostName(string ipAddress);
    }
}
