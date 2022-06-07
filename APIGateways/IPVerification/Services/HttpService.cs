using Newtonsoft.Json;
using Polly;

namespace IPVerification.Services
{
    public abstract class HttpService
    {
        private readonly System.Net.Http.HttpClient _client;
        private readonly ILogger<HttpService> _logger;

        public HttpService(System.Net.Http.HttpClient client, ILogger<HttpService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected async Task<T> Get<T>(string endpoint, string token = null)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, GetRequestUrl(endpoint));

                var response = await Execute(request, token);
                var content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(content))
                    return (T)JsonConvert.DeserializeObject(content, typeof(T));
            }
            catch (UnauthorizedAccessException authorizationException)
            {
                _logger.LogError($"Microservice authorization exception while executing request {HttpMethod.Get} {endpoint} - Exception {authorizationException.Message} - Inner exception {authorizationException.InnerException?.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while executing request {HttpMethod.Get} {_client.BaseAddress + endpoint} - Exception {ex.Message} - Inner exception {ex.InnerException?.Message}");
                throw;
            }

            return default(T);
        }
        private Uri GetRequestUrl(string endpoint)
        {
            if (endpoint.StartsWith("http://") || endpoint.StartsWith("https://"))
            {
                return new Uri(endpoint);
            }
            else
            {
                return new Uri(_client.BaseAddress + endpoint);
            }
        }

        private async Task<HttpResponseMessage> Execute(HttpRequestMessage request, string token = null)
        {
            request.SetPolicyExecutionContext(new Context(request.RequestUri.ToString()));

            if (_client.DefaultRequestHeaders.Authorization == null && !string.IsNullOrEmpty(token))
                _client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await _client.SendAsync(request);
            var responseStatusCode = (int)response.StatusCode;
            if (responseStatusCode == (int)System.Net.HttpStatusCode.Unauthorized)
            {
                _logger.LogError($"Request to the microservice unauthorized with reason phrase {response.ReasonPhrase}");
                throw new UnauthorizedAccessException($"Request to the microservice Unauthorized with reason phrase: {response.ReasonPhrase}");
            }           

            return response;
        }
    }
}
