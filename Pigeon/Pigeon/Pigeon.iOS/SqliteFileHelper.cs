using Pigeon.iOS;
using Pigeon.LocalDataAccess;
using System;
using System.IO;
using Xamarin.Forms;


[assembly: Dependency(typeof(SqliteFileHelper))]
namespace Pigeon.iOS
{
    public class SqliteFileHelper : ISqliteFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}
