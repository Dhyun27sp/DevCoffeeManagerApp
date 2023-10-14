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
    public class ScheduleModel
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string ScheduleId { get; set; }

        [BsonElement("shift"), BsonRepresentation(BsonType.String)]
        public string shift { get; set; }

        [BsonElement("staff_number"), BsonRepresentation(BsonType.String)]
        public String staff_number { get; set; }

        [BsonElement("evaluate")]
        public EvaluateModel evaluate { get; set; }

        public ScheduleModel(string shift, String staff_number, EvaluateModel evaluate)
        {
            this.shift = shift;
            this.staff_number = staff_number;
            this.evaluate = evaluate;
        }
    }
}
