namespace Pigeon.Services.Model
{
    public class CustomJResponse
    {
        public bool IsOtpSent { get; set; }
        public string OtpCode { get; set; }
        public string Token { get; set; }
    }
}
