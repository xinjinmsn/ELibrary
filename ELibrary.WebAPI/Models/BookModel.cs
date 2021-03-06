﻿using ELibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.WebAPI.Models
{
    public class BookModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}