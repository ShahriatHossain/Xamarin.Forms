namespace MnMFiber.Core.Models
{
    public class CustomResponse
    {
        public bool IsSuccess { get; set; }
        public string AccessToken { get; set; }
        public string SuccessMessage { get; set; }
        public int UserId { get; set; }
    }
}
