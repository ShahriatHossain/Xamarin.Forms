using System;
using SQLite;


namespace MMTowers.Service.Interface
{
    public interface ISQLiteService
    {
        SQLiteConnection GetConnection(string databaseName);
        long GetSize(string databaseName);
    }
}
