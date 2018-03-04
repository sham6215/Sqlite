using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace Database.Services
{
    public class CreateDatabaseService : DbConnectionBaseService
    {
        private static CreateDatabaseService _instance = new CreateDatabaseService();
        public static CreateDatabaseService Instance => _instance;
        private CreateDatabaseService() { }

        public void CreateDatabase(string dbPath, bool overwrite)
        {
            if (!overwrite && File.Exists(dbPath))
            {
                throw new IOException("File exists");
            }
            SQLiteConnection.CreateFile(dbPath);
            string source = GetDbSource(dbPath);
            using (var conn = new SQLiteConnection(source))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    CreateTables(transaction);

                    transaction.Commit();
                }
            }
        }

        private void CreateTables(SQLiteTransaction transaction)
        {
            CreateUnitPriceTable(transaction);
        }

        private void CreateUnitPriceTable(SQLiteTransaction transaction)
        {
            using (var command = new SQLiteCommand() { Transaction = transaction })
            {
                command.CommandText = Resources.Sql.CreateTable_UnitSalePrice;
                var res = command.ExecuteNonQuery();
            }
        }
    }
}
