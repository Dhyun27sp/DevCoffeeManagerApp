using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class EvaluateModel
    {
        [BsonElement("staff_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId staff_id { get; set; }

        [BsonElement("worked"), BsonRepresentation(BsonType.Boolean)]
        public Boolean worked { get; set; }

        [BsonElement("score"), BsonRepresentation(BsonType.Int32)]
        public int score { get; set; }

       public EvaluateModel(ObjectId staff_id, bool worked, int score)
        {
            this.staff_id = staff_id;
            this.worked = worked;
            this.score = score;
        }

        public EvaluateModel() { }
    }
}
