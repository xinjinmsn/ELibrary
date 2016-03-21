using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Data.Entities
{
    public class Book
    {
        public Book()
        {
            Tags = new List<Tag>();
        }
        public int Id { get; set; }

        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }

        public int Pages { get; set;}

        public int Stock { get; set; }

        public Author Author { get; set; }
        public virtual ICollection<Tag> Tags {get;set;}

    }
}
