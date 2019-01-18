using MnMLilon.Service.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.DataAccess.Implementation
{
    public class TransactionElectricalInstallationTableHelper : BaseHelper
    {
        public static List<TransactionElectricalInstallationTable> GetTransactionElectricalInstallation()
        {
            var database = CreateDatabase();
            CreateTransactionElectricalInstallationTable();
            return database.GetItems<TransactionElectricalInstallationTable>().ToList();
        }

        public static void SaveTransactionElectricalInstallation(TransactionElectricalInstallationTable transactionElectricalInstallation)
        {
            var database = CreateDatabase();
            CreateTransactionElectricalInstallationTable();
            database.SaveItem(transactionElectricalInstallation);
        }

        public static void CreateTransactionElectricalInstallationTable()
        {
            var database = CreateDatabase();
            database.CreateTable<TransactionElectricalInstallationTable>();
        }

        public static void DeleteAllTransactionElectricalInstallations()
        {
            var database = CreateDatabase();
            CreateTransactionElectricalInstallationTable();
            database.DeleteAll<TransactionElectricalInstallationTable>();
        }
    }
}
