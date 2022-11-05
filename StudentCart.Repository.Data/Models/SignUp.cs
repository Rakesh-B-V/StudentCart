using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCart.Repository.Data.Models
{
    public class SignUp
    {
        [BsonId]
        [BsonElement("_id")] 

        public Guid Id { get; set; }
        public String UserName { get; set; }

        public String Password { get; set; }
    }
}
