using SQLite;

namespace MnMFiber.Core.Repositories
{
    public interface ISQLiteService
    {
        SQLiteConnection GetConnection(string databaseName);
        long GetSize(string databaseName);
    }
}
