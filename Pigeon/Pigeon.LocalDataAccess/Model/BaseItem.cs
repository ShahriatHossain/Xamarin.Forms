using SQLite;

namespace Pigeon.LocalDataAccess.Model
{
    public class BaseItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }

}
