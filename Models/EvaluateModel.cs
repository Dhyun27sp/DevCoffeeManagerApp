using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DevCoffeeManagerApp.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class EvaluateModel
    {
        [BsonElement("staff_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId staff_id { get; set; }

        [BsonElement("worked"), BsonRepresentation(BsonType.Boolean)]
        public Boolean worked { get; set; }

       public EvaluateModel(ObjectId staff_id, bool worked)
        {
            this.staff_id = staff_id;
            this.worked = worked;
        }

        public EvaluateModel() { }
    }
}
