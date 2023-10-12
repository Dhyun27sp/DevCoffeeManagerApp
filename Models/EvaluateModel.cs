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
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("staff_id"), BsonRepresentation(BsonType.String)]
        public string staff_id { get; set; }

        [BsonElement("shift"), BsonRepresentation(BsonType.String)]
        public string shift { get; set; }

        [BsonElement("timekeeping"), BsonRepresentation(BsonType.String)]
        public string timekeeping { get; set; }

        [BsonElement("score"), BsonRepresentation(BsonType.String)]
        public string score { get; set; }

        public EvaluateModel(string staff_id, string shift, string timekeeping, string score)
        {
            this.staff_id = staff_id;
            this.shift = shift;
            this.timekeeping = timekeeping;
            this.score = score;
        }
        public EvaluateModel() { }
    }
}
