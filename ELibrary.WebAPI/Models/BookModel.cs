﻿using ELibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.WebAPI.Models
{
    public class BookModel
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public Author Author { get; set; }

        public IEnumerable<TagModel> Tags { get; set; }
    }
}