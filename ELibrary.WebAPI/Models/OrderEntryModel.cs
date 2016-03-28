using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.WebAPI.Models
{
    public class OrderEntryModel
    {
        public string Url { get; set; }

        public string BookTitle { get; set; }

        public string BookUrl { get; set; }
        public int Quantity { get; set; }
    }
}