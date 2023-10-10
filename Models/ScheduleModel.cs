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

        [BsonElement("staff_number"), BsonRepresentation(BsonType.Int32)]
        public int staff_number { get; set; }

        [BsonElement("staff_id"), BsonRepresentation(BsonType.String)]
        public List<string> staff_id { get; set; }

        public ScheduleModel(string scheduleId, string shift, int staff_number, List<string> staff_id)
        {
            ScheduleId = scheduleId;
            this.shift = shift;
            this.staff_number = staff_number;
            this.staff_id = staff_id;
        }
    }
}
