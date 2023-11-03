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
    public class MenuModel
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }

        [BsonElement("type_of_dish"), BsonRepresentation(BsonType.String)]
        public string type_of_dish { get; set; }

        [BsonElement("detail"), BsonRepresentation(BsonType.String)]
        public string detail { get; set;}

        [BsonElement("dish"), BsonIgnoreIfNull]
        public List<DishModel> dish { get; set; }
    }
}
