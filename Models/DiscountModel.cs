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
    public class DiscountModel
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [BsonId, BsonElement("name"), BsonRepresentation(BsonType.String),BsonIgnoreIfNull]
        public string name { get; set; }
        [BsonId, BsonElement("detail"), BsonRepresentation(BsonType.String), BsonIgnoreIfNull]
        public string detail { get; set; }
        [BsonId, BsonElement("apply"), BsonIgnoreIfNull]
        public object apply { get; set; }
        [BsonId, BsonElement("name"), BsonRepresentation(BsonType.ObjectId), BsonIgnoreIfNull]
        public string tt { get; set; }

    }
}
