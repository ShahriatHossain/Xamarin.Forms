namespace Core.Models
{
    public class ProductStepAttachment
    {
        public int Id { get; set; }
        public int ProductStepId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsSelected { get; set; }
    }
}
