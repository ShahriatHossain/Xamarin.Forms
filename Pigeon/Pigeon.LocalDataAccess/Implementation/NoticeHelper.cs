using Pigeon.LocalDataAccess.Model;
using System.Collections.Generic;
using System.Linq;

namespace Pigeon.LocalDataAccess.Implementation
{
    public class NoticeHelper : BaseHelper
    {
        public static ChannelNotice GetNotice()
        {
            var database = CreateDatabase();
            CreateNoticeTable();
            return database.GetItems<ChannelNotice>().ToList().FirstOrDefault();
        }

        public static IEnumerable<ChannelNotice> Get(string userId, int channelId)
        {
            var database = CreateDatabase();
            CreateNoticeTable();
            return database.GetItems<ChannelNotice>().Where(x => x.UserId == userId && x.ChannelId == channelId);
        }
        public static IEnumerable<ChannelNotice> GetNewerNotices(string userId, int channelId, int noticeId)
        {
            var database = CreateDatabase();
            CreateNoticeTable();
            return database.GetItems<ChannelNotice>().Where(x => x.UserId == userId && x.ChannelId == channelId && x.NoticeId > noticeId);
        }

        public static ChannelNotice GetNoticeDetail(int noticeId)
        {
            var database = CreateDatabase();
            CreateNoticeTable();
            return database.GetItems<ChannelNotice>().Where(x => x.NoticeId == noticeId).FirstOrDefault();
        }

        public static IEnumerable<ChannelNotice> Get(string userId, int channelId, int currentPage, int pageSize)
        {
            var database = CreateDatabase();
            CreateNoticeTable();
            var skip = (currentPage - 1) * pageSize;
            return database.GetItems<ChannelNotice>()
                                .Where(x => x.UserId == userId && x.ChannelId == channelId)
                                .OrderByDescending(x => x.CreationTime)
                                .Skip(skip)
                                .Take(pageSize);
        }

        public static int? GetLatestNoticeId(string userId, int channelId)
        {
            var database = CreateDatabase();
            CreateNoticeTable();
            return database.GetItems<ChannelNotice>().Any(c => c.ChannelId == channelId) ? database.GetItems<ChannelNotice>().Where(c => c.ChannelId == channelId).Max(x => x.NoticeId) : 0;
        }

        public static bool UpdateVoteCast(int noticeId, string userId)
        {
            var database = CreateDatabase();
            CreateNoticeTable();
            var notice = database.GetItems<ChannelNotice>().Where(x => x.UserId == userId && x.NoticeId == noticeId).First();
            notice.VotingStatus = "vote casted";
            notice.IsVoteCasted = true;
            SaveNotice(notice);
            return true;
        }

        public static void SaveNotice(ChannelNotice notice)
        {
            var database = CreateDatabase();
            CreateNoticeTable();
            database.SaveItem(notice);
        }

        public static void InsertAll(IEnumerable<ChannelNotice> notices)
        {
            var database = CreateDatabase();
            CreateNoticeTable();
            database.InsertAll(notices);
        }

        public static void CreateNoticeTable()
        {
            var database = CreateDatabase();
            database.CreateTable<ChannelNotice>();
        }

        public static void DeleteAllNotices()
        {
            var database = CreateDatabase();
            database.DeleteAll<ChannelNotice>();
        }
    }
}
