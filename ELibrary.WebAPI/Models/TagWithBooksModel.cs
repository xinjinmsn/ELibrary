using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.WebAPI.Models
{
    public class TagWithBooksModel : TagModel
    {
        public IEnumerable<BookModel> Books { get; set; }
    }
}