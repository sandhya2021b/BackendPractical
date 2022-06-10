using System.Net.NetworkInformation;

namespace IPVerification.Services.PingService
{
    public  class PingService
    {
        private readonly ILogger<PingService> _logger;
        public PingService(ILogger<PingService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException ex)
            {
                _logger.LogError($"Error occcurred while executing [{nameof(PingService)}] [{nameof(PingHost)}] method. " +
                    $"ERROR MESSAGE:{ex.Message} INNER EXCEPTION: {ex.InnerException?.Message} STACKTRACE: {ex.StackTrace}");            
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
            return pingable;
        }
    }
}
