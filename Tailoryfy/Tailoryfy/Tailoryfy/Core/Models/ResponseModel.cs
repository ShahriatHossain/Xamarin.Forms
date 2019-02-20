namespace Tailoryfy.Core.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? ReturnId { get; set; }
        public string ReturnCode { get; set; }
        public string ErrorCode { get; set; }
    }
}
