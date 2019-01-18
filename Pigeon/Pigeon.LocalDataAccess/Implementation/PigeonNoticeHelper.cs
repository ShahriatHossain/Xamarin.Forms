using Pigeon.LocalDataAccess.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pigeon.LocalDataAccess.Implementation
{
    public class PigeonNoticeHelper
    {
        static PigeonDatabase database;

        public static PigeonDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new PigeonDatabase(DependencyService.Get<ISqliteFileHelper>().GetLocalFilePath("PigeonSQLite.db3"));
                }
                return database;
            }
        }

        public static async Task<ObservableCollection<ChannelNotice>> GetNewerNoticesAsync(string userId, int channelId, int noticeId)
        {
            var newerNotices = new ObservableCollection<ChannelNotice>();
            var notices = await Database.GetItemsAsync();

            foreach (var notice in notices
                .Where(x => x.UserId == userId && x.ChannelId == channelId && x.NoticeId > noticeId))
            {
                newerNotices.Add(notice);
            }

            return newerNotices;
        }

        public static async Task<ChannelNotice> GetNoticeDetailAsync(int noticeId)
        {
            return await Database.GetItemAsync(noticeId);
        }

        public static async Task<ObservableCollection<ChannelNotice>> GetAsync(string userId, int channelId, int currentPage, int pageSize)
        {
            var newerNotices = new ObservableCollection<ChannelNotice>();

            var skip = (currentPage - 1) * pageSize;

            var results = await Database.GetItemsAsync();

            var notices = (results
                        .Where(x => x.UserId == userId && x.ChannelId == channelId)
                        .OrderByDescending(x => x.CreationTime)
                        .Skip(skip)
                        .Take(pageSize));

            foreach (var notice in notices)
            {
                newerNotices.Add(notice);
            }

            return newerNotices;
        }

        public static async Task<int?> GetLatestNoticeIdAsync(string userId, int channelId)
        {
            var results = await Database.GetItemsAsync();

            return results.Any(c => c.ChannelId == channelId) ? results
                .Where(c => c.ChannelId == channelId).Max(x => x.NoticeId) : 0;
        }

        public static async Task<bool> UpdateVoteCastAsync(int noticeId, string userId)
        {
            var results = await Database.GetItemsAsync();

            var notice = results
                .Where(x => x.UserId == userId && x.NoticeId == noticeId).First();

            notice.VotingStatus = "vote casted";
            notice.IsVoteCasted = true;

            await Database.SaveItemAsync(notice);
            return true;
        }

        public static async Task SaveNoticeAsync(ChannelNotice notice)
        {
            var result = await Database.SaveItemAsync(notice);
        }

        public static async Task InsertAllAsync(IEnumerable<ChannelNotice> notices)
        {
            foreach (var notice in notices)
            {
                var result = await Database.SaveItemAsync(notice);
            }
        }
    }
}
