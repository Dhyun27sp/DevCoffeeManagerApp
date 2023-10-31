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
    public class DishsRecieptModel
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
    }
}
