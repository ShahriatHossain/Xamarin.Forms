namespace Pigeon.Services.Interface
{
    public interface IImageResizeService
    {
        byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}
