using MnMLilon.Service.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.DataAccess.Implementation
{
    public class TransactionBatteryDetailTableHelper : BaseHelper
    {
        public static List<TransactionBatteryDetailTable> GetTransactionBatteryDetail()
        {
            var database = CreateDatabase();
            CreateTransactionBatteryDetailTable();
            return database.GetItems<TransactionBatteryDetailTable>().ToList();
        }

        public static void SaveTransactionBatteryDetail(TransactionBatteryDetailTable transactionBatteryDetail)
        {
            var database = CreateDatabase();
            CreateTransactionBatteryDetailTable();
            database.SaveItem(transactionBatteryDetail);
        }

        public static void CreateTransactionBatteryDetailTable()
        {
            var database = CreateDatabase();
            database.CreateTable<TransactionBatteryDetailTable>();
        }

        public static void DeleteAllTransactionBatteryDetails()
        {
            var database = CreateDatabase();
            CreateTransactionBatteryDetailTable();
            database.DeleteAll<TransactionBatteryDetailTable>();
        }
    }
}
