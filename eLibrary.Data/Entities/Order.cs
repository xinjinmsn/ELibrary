using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Data.Entities
{
    public class Order
    {
        public Order()
        {
            Entries = new List<OrderEntry>();
        }
        public int Id { get; set; }
        public DateTime CurrentDate { get; set; }
        public ICollection<OrderEntry> Entries { get; set; }

        public string UserName { get; set; }
    }
}
