using Database.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Database.Services
{
    public class GenerateDataService
    {
        public List<UnitSalePrice> GenerateUnitSalePrices(int count)
        {
            Random rnd = new Random();
            var list = new List<UnitSalePrice>();

            for (int i = 0; i < count; ++i)
            {
                list.Add(new UnitSalePrice()
                {
                    Id = i + 1,
                    Currency = rnd.Next(1, 4),
                    NumberMonths = rnd.Next(1, 13),
                    Rrp = rnd.Next(1, 500) + rnd.Next(1, 100) / 100,
                    UnitSaleId = 10000 + i * 3
                });
            }

            return list;
        }

        /// <summary>
        /// Reads and parses text file with fields separated by tab.
        /// Fields order: id    number_months   rrp    currency    unit_sale_id
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<UnitSalePrice> ReadUnitSalePrices(string filePath)
        {
            var list = new List<UnitSalePrice>();
            string line = string.Empty;
            using (var file = File.OpenText(filePath))
            {
                while (!string.IsNullOrEmpty((line = file.ReadLine())))
                {
                    try
                    {
                        var ar = line.Split('\t');
                        if (ar.Length != 5)
                        {
                            continue;
                        }

                        list.Add(new UnitSalePrice() {
                            Id = int.Parse(ar[0]),
                            NumberMonths = int.Parse(ar[1]),
                            Rrp = double.Parse(ar[2], new CultureInfo("en-US")),
                            Currency = int.Parse(ar[3]),
                            UnitSaleId = int.Parse(ar[4])
                        });
                    } catch (Exception) { }
                }
            }

            return list;
        }
    }
}
