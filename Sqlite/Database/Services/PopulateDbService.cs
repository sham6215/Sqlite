using Database.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Database.Services
{
    public class PopulateDbService : DbConnectionBaseService
    {
        public string DbPath { get; private set; }
        public PopulateDbService(string dbPath)
        {
            DbPath = dbPath;
        }
        
        public List<UnitSalePrice> InsertUnitSalePrices(List<UnitSalePrice> prices)
        {
            using (var conn = new SQLiteConnection(GetDbSource(DbPath)))
            {
                conn.Open();
                using (var tr = conn.BeginTransaction())
                {
                    using (var com = new SQLiteCommand() { Transaction = tr })
                    {
                        com.Transaction = tr;
                        string parId = "@id";
                        string parNumberMonths = "@NumberMonths";
                        string parRrp = "@Rrp";
                        string parCurrency = "@Currency";
                        string parUniSaleId = "@UniSaleId";
                        com.CommandText = "INSERT INTO unit_sale_price (id, number_months, rrp, currency, unit_sale_id) VALUES "
                            + $"({parId}, {parNumberMonths}, {parRrp}, {parCurrency}, {parUniSaleId})";

                        com.Parameters.Add(new SQLiteParameter(parId, 1));
                        com.Parameters.Add(new SQLiteParameter(parNumberMonths, 1));
                        com.Parameters.Add(new SQLiteParameter(parRrp, (double)1.1));
                        com.Parameters.Add(new SQLiteParameter(parCurrency, 1));
                        com.Parameters.Add(new SQLiteParameter(parUniSaleId, 1));

                        prices.ForEach(price =>
                        {
                            com.Parameters[parId].Value = price.Id;
                            com.Parameters[parNumberMonths].Value = price.NumberMonths;
                            com.Parameters[parRrp].Value = price.Rrp;
                            com.Parameters[parCurrency].Value = price.Currency;
                            com.Parameters[parUniSaleId].Value = price.UnitSaleId;

                            var res = com.ExecuteNonQuery();
                        });

                        /// Slow method:
                        ///
                        //prices.ForEach(price =>
                        //{
                        //    com.CommandText = "INSERT INTO unit_sale_price (id, number_months, rrp, currency, unit_sale_id) VALUES "
                        //    + $"({price.Id}, {price.NumberMonths}, {price.Rrp}, {price.Currency}, {price.UnitSaleId})";

                        //    var res = com.ExecuteNonQuery();
                        //});
                    }
                    tr.Commit();
                }
            }

            return prices;
        }


        public List<UnitSalePrice> UpdateUnitSalePrices(List<UnitSalePrice> prices)
        {
            using (var conn = new SQLiteConnection(GetDbSource(DbPath)))
            {
                conn.Open();
                using (var tr = conn.BeginTransaction())
                {
                    using (var com = new SQLiteCommand() { Transaction = tr })
                    {
                        var count = prices.Count + 1000;
                        string parId = "@id";
                        string parNumberMonths = "@NumberMonths";
                        string parRrp = "@Rrp";
                        string parCurrency = "@Currency";
                        string parUniSaleId = "@UniSaleId";
                        com.CommandText = $"UPDATE unit_sale_price SET id = {parId}, number_months = {parNumberMonths}, "
                            + $"rrp = {parRrp}, currency = {parCurrency}, unit_sale_id = {parUniSaleId}  "
                            + $"WHERE id = {parId}";

                        com.Parameters.Add(new SQLiteParameter(parId, 1));
                        com.Parameters.Add(new SQLiteParameter(parNumberMonths, 1));
                        com.Parameters.Add(new SQLiteParameter(parRrp, (double)1.1));
                        com.Parameters.Add(new SQLiteParameter(parCurrency, 1));
                        com.Parameters.Add(new SQLiteParameter(parUniSaleId, 1));

                        prices.ForEach(price => {
                            com.Parameters[parId].Value = price.Id + count;
                            com.Parameters[parNumberMonths].Value = price.NumberMonths + count;
                            com.Parameters[parRrp].Value = price.Rrp + count;
                            com.Parameters[parCurrency].Value = price.Currency + count;
                            com.Parameters[parUniSaleId].Value = price.UnitSaleId + count;

                            /// Slow method:
                            ///
                            //com.CommandText = "INSERT INTO unit_sale_price (id, number_months, rrp, currency, unit_sale_id) VALUES "
                            //+ $"({price.Id}, {price.NumberMonths}, {price.Rrp}, {price.Currency}, {price.UnitSaleId})";

                            var res = com.ExecuteNonQuery();
                        });
                    }
                    tr.Commit();
                }
            }

            return prices;
        }

        public List<UnitSalePrice> UpdateUnitSalePricesWithoutKeys(List<UnitSalePrice> prices)
        {
            using (var conn = new SQLiteConnection(GetDbSource(DbPath)))
            {
                conn.Open();
                using (var tr = conn.BeginTransaction())
                {
                    using (var com = new SQLiteCommand() { Transaction = tr })
                    {
                        var count = prices.Count + 1000;
                        string parId = "@id";
                        string parNumberMonths = "@NumberMonths";
                        string parRrp = "@Rrp";
                        string parCurrency = "@Currency";
                        string parUniSaleId = "@UniSaleId";
                        com.CommandText = $"UPDATE unit_sale_price SET id = {parId}, rrp = {parRrp}, currency = {parCurrency} "
                            + $"WHERE number_months = {parNumberMonths} AND unit_sale_id = {parUniSaleId}";

                        com.Parameters.Add(new SQLiteParameter(parId, 1));
                        com.Parameters.Add(new SQLiteParameter(parNumberMonths, 1));
                        com.Parameters.Add(new SQLiteParameter(parRrp, (double)1.1));
                        com.Parameters.Add(new SQLiteParameter(parCurrency, 1));
                        com.Parameters.Add(new SQLiteParameter(parUniSaleId, 1));

                        prices.ForEach(price => {
                            com.Parameters[parId].Value = price.Id + 9999999;
                            com.Parameters[parNumberMonths].Value = price.NumberMonths;
                            com.Parameters[parRrp].Value = price.Rrp + count;
                            com.Parameters[parCurrency].Value = 1;
                            com.Parameters[parUniSaleId].Value = price.UnitSaleId;

                            //com.CommandText = "INSERT INTO unit_sale_price (id, number_months, rrp, currency, unit_sale_id) VALUES "
                            //+ $"({price.Id}, {price.NumberMonths}, {price.Rrp}, {price.Currency}, {price.UnitSaleId})";

                            var res = com.ExecuteNonQuery();
                        });
                    }
                    tr.Commit();
                }
            }

            return prices;
        }
    }
}
