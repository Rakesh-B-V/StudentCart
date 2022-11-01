using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCart.Repository.Business.Models
{
    public class Categories
    {
        [BsonId]
        [BsonElement("_id")]
        public Guid Id { get; set; }
        public List<String> CategoryItems { get; set; }
    }
}
