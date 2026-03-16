using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GulyaevLib
{
    public class SalesView
    {
        public int Id { get; set; }
        public int Partner_Id { get; set; }

        public string? Product_name { get; set; }

        public int? PartnerCount { get; set; }

        public DateOnly? SaleDate { get; set; }
    }
}
