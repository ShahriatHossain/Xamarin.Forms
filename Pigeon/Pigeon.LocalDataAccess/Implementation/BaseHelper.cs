namespace Pigeon.LocalDataAccess.Implementation
{
    public class BaseHelper
    {
        public static Database CreateDatabase()
        {
            return new Database("Pigeon");
        }
    }
}
