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
    public class StaffModel
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string staffid { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string staffname { get; set; }

        [BsonElement("phone_number"), BsonRepresentation(BsonType.String)]
        public string phone_staff { get; set; }

        [BsonElement("schedule"), BsonRepresentation(BsonType.String)]
        public List<string> schedule { get; set; }

        public StaffModel(string staffid, string staffname, string phone_staff, List<string> schedule)
        {
            this.staffid = staffid;
            this.staffname = staffname;
            this.phone_staff = phone_staff;
            this.schedule = schedule;
        }
    }
}
