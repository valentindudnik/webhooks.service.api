namespace Webhooks.Service.Models.Dtos
{
    public class ErrorInfoDto
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
