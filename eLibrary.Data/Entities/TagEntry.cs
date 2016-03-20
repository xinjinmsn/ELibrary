using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Data.Entities
{
    public class TagEntry
    {
        public int Id { get; set; }
        public virtual Book BookItem { get; set; }
        public Tag TagItem { get; set; }

    }
}
