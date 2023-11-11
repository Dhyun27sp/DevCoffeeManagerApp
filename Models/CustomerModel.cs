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
        public CustomerModel()
        {
            // Đặt giá trị mặc định cho points_used và plus_points
            points_used = 0;
            plus_points = 0;
        }
        [BsonId, BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string name { get; set; }

        [BsonId, BsonElement("phone_number"), BsonRepresentation(BsonType.String)]
        public string phone_number { get; set; }

        [BsonElement("point"), BsonRepresentation(BsonType.Int32)]
        public int point { get; set; }

        [BsonElement("points_used"), BsonRepresentation(BsonType.Int32)]
        public int? points_used { get; set; }

        [BsonElement("plus_points"), BsonRepresentation(BsonType.Int32)]
        public int? plus_points { get; set; }
    }
}
