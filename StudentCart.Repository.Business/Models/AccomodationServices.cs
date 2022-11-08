using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCart.Repository.Business.Models
{
    public class AccomodationServices
    {
        [BsonId]
        [BsonElement("_id")]
        public Guid Id { get; set; }
        public String OwnerName { get; set; }
        public String Category { get; set; }
        public String OwnerNumber { get; set; }
        public String Address { get; set; }
        public String ApartmentType { get; set; }
        public String Price { get; set; }
    }
}
