using SQLite;

namespace MnMFiber.Core.Models
{
    public class BaseItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
