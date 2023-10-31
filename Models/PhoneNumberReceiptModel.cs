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
    public class PhoneNumberReceiptModel
    {
        [BsonId, BsonElement("phone_number"), BsonRepresentation(BsonType.String)]
        public string phone_number { get; set; }

        [BsonElement("points_used"), BsonRepresentation(BsonType.Int32)]
        public int points_used { get; set; }

        [BsonElement("plus_points"), BsonRepresentation(BsonType.Int32)]
        public int plus_points { get; set; }
    }
}
