namespace ReverseDnsService.API.Services.Models
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; }
    }

    public class ErrorModel
    {
        public string Message { get; set; }
    }
}
