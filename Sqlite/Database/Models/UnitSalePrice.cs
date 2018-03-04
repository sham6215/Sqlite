using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace Database.Models
{
    [Table(Name = "unit_sale_price")]
    public class UnitSalePrice
    {
        [Column(Name = "id")]
        public int Id { get; set; }

        [Column(Name = "number_months")]
        public int NumberMonths { get; set; }

        [Column(Name = "rrp")]
        public double Rrp { get; set; }

        [Column(Name = "currency")]
        public int Currency { get; set; }

        [Column(Name = "unit_sale_id")]
        public int UnitSaleId { get; set; }
    }
}
