using Database.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Sqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = null;

            //string path = Path.Combine(GetAssemblyLocation(), "sqlite.db");
            string path = @"N:\work\projects\GitHub\Sqlite\Sqlite\Sqlite\Data\pay.db";
            string pricesPath = @"N:\work\projects\GitHub\Sqlite\Sqlite\Sqlite\Data\prices.txt";
            //CreateDatabaseService.Instance.CreateDatabase(path, true);
            PopulateDbService pop = new PopulateDbService(path);
            GenerateDataService gen = new GenerateDataService();
            var prices = gen.ReadUnitSalePrices(pricesPath);

            Console.WriteLine("Data generating...");

            watch = Stopwatch.StartNew();
            
            //var prices = gen.PopulateUnitSalePrices(1000000);
            var res = pop.InsertUnitSalePrices(prices);

            watch.Stop();

            Console.WriteLine($"Prices added. Elapsed time: {watch.ElapsedMilliseconds} ms");

            Console.WriteLine("");
            Console.WriteLine("Data updating...");
            watch.Restart();

            pop.UpdateUnitSalePricesWithoutKeys(prices);

            watch.Stop();
            Console.WriteLine($"Prices updated. Elapsed time: {watch.ElapsedMilliseconds} ms");
        }

        static string GetAssemblyLocation()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        }
    }
}
