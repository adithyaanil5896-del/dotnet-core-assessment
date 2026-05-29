using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreAssessment.Models
{
    public class SaleRecord
    {
        public DateTime Date { get; set; }

        public string SKU { get; set; }

        public int UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int TotalPrice { get; set; }
    }
}
