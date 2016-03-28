using System;
using System.Collections.Generic;

namespace ELibrary.WebAPI.Models
{
    public class OrderModel
    {
        public string Url { get; set; }
        public DateTime CurrentDate { get; set; }

        public IEnumerable<OrderEntryModel> Entries { get; set; }
    }
}