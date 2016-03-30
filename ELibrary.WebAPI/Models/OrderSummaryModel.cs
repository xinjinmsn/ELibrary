using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.WebAPI.Models
{
    public class OrderSummaryModel
    {
        public DateTime OrderDate { get; set; }
        public decimal CostSum { get; set; }
        public int TotalCount { get; set; }
    }
}