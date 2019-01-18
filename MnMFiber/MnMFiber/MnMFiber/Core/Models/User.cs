namespace MnMFiber.Core.Models
{
    public class User : BaseItem
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public override string ToString()
        {
            return $"{ID}, {UserName}, {AccessToken}";
        }
    }
}
