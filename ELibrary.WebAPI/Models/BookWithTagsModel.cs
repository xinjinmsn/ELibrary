using ELibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.WebAPI.Models
{
    public class BookWithTagsModel : BookModel
    {
        public IEnumerable<TagModel> Tags { get; set; }
    }
}