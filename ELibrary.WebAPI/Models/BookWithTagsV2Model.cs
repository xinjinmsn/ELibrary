using ELibrary.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELibrary.WebAPI.Models
{
    public class BookWithTagsV2Model : BookV2Model
    {
        public IEnumerable<TagModel> Tags { get; set; }
    }
}