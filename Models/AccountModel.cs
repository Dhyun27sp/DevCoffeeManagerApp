using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DevCoffeeManagerApp.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class AccountModel
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string staffid { get; set; }

        [BsonElement("phone_number"), BsonRepresentation(BsonType.String)]
        public string phone_number { get; set; }

        [BsonElement("password"), BsonRepresentation(BsonType.String)]
        public string password { get; set; }

        [BsonElement("role"), BsonRepresentation(BsonType.String)]
        public string role { get; set; }
    }
}
