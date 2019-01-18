using Pigeon.LocalDataAccess.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pigeon.LocalDataAccess
{
    public class PigeonDatabase
    {
        readonly SQLiteAsyncConnection database;

        public PigeonDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ChannelNotice>().Wait();
        }

        public Task<List<ChannelNotice>> GetItemsAsync()
        {
            return database.Table<ChannelNotice>().ToListAsync();
        }

        public Task<List<ChannelNotice>> GetItemsByChannelAsync(int channelId)
        {
            return database.QueryAsync<ChannelNotice>($"SELECT * FROM [ChannelNotice] WHERE [ChannelId] = {channelId}");
        }

        public Task<ChannelNotice> GetItemAsync(int id)
        {
            return database.Table<ChannelNotice>().Where(i => i.NoticeId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ChannelNotice item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(ChannelNotice item)
        {
            return database.DeleteAsync(item);
        }
    }
}
