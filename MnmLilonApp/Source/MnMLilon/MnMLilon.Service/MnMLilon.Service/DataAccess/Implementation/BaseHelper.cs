namespace MnMLilon.Service.DataAccess.Implementation
{
    public class BaseHelper
    {
        public static Database CreateDatabase()
        {
            return new Database("MMTowers");
        }
    }
}
