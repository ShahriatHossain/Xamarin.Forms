using Pigeon.Droid;
using Pigeon.LocalDataAccess;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqliteFileHelper))]
namespace Pigeon.Droid
{
    public class SqliteFileHelper : ISqliteFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
    }
}