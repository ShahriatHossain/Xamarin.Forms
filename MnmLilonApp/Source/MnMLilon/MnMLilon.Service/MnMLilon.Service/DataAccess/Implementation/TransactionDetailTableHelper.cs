using MnMLilon.Service.DataAccess.Model;
using System.Collections.Generic;
using System.Linq;

namespace MnMLilon.Service.DataAccess.Implementation
{
    public class TransactionDetailTableHelper : BaseHelper
    {
        public static List<TransactionDetailTable> GetTransactionDetail()
        {
            var database = CreateDatabase();
            CreateTransactionDetailTable();
            return database.GetItems<TransactionDetailTable>().ToList();
        }

        public static void SaveTransactionDetail(TransactionDetailTable transactionDetail)
        {
            var database = CreateDatabase();
            CreateTransactionDetailTable();
            database.SaveItem(transactionDetail);
        }

        public static void CreateTransactionDetailTable()
        {
            var database = CreateDatabase();
            database.CreateTable<TransactionDetailTable>();
        }

        public static void DeleteAllTransactionDetails()
        {
            var database = CreateDatabase();
            CreateTransactionDetailTable();
            database.DeleteAll<TransactionDetailTable>();
        }
    
    }
}
