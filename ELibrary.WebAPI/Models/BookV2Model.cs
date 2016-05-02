using ELibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.WebAPI.Models
{
    public class BookV2Model
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Year { get; set; }
        public bool InStock { get; set; }

        public Author Author { get; set; }
        public string ImageUrl { get; set; }
    }
}