using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentCart.Repository.Business.Models
{
    public class SignUp
    {
        [BsonId]
        [BsonElement("_id")] // Create one more Model in Data Repository, withou the below constarints... and we will check

        public Guid Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        [RegularExpression(@"[a-zA-Z]{1,20}$" ,ErrorMessage = "{0} should not include digits and special characters and Whitespace.")]
        public String UserName { get; set; }

        [Required]
        [StringLength(15,  ErrorMessage ="Length of the {0} must be between {2} and {1}", MinimumLength = 8)]
        public String Password { get; set; }

        public Boolean IsSessionActive { get; set; }


    }
}
