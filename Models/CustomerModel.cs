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
    public class CustomerModel
    {
        // Constructor
        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string name { get; set; }

        [BsonElement("phone_number"), BsonRepresentation(BsonType.String)]
        public string phone_number { get; set; }

        [BsonElement("point"), BsonRepresentation(BsonType.Int32)]
        public int point { get; set; }
    }
}
