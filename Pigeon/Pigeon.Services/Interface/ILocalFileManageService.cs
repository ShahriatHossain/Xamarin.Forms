namespace Pigeon.Services.Interface
{
    public interface ILocalFileManageService
    {
        void DeleteFile(string filePath);
        byte[] GetFileDataArray(string filePath);
    }
}
