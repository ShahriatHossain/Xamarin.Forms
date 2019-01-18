using MnMLilon.Service.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.DataAccess.Implementation
{
    public class UserHelper : BaseHelper
    {
        public static User GetUser()
        {
            var database = CreateDatabase();
            CreateUserTable();
            return database.GetItems<User>().ToList().FirstOrDefault();
        }

        public static void SaveUser(User user)
        {
            var database = CreateDatabase();
            CreateUserTable();
            database.SaveItem(user);
        }

        public static void CreateUserTable()
        {
            var database = CreateDatabase();
            database.CreateTable<User>();
        }

        public static void DeleteAllUsers()
        {
            var database = CreateDatabase();
            database.DeleteAll<User>();
        }
    }
}
