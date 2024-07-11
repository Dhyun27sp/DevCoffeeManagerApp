using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DevCoffeeManagerApp.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class AccountModel
    {
        [BsonElement("password"),BsonRepresentation(BsonType.String)]
        public string Password { get; set; }

        [BsonElement("role"), BsonRepresentation(BsonType.String)]
        public string Role { get; set; }
    }
}
