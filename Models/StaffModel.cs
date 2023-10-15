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

        [BsonElement("account")]
        public AccountModel account { get; set; }

        [BsonElement("salary")]
        public SalaryModel salary { get; set; }

        public StaffModel(string staffid, string staffname, string phone_staff, AccountModel account, SalaryModel salary)
        {
            this.staffid = staffid;
            this.staffname = staffname;
            this.phone_staff = phone_staff;
            this.account = account;
            this.salary = salary;
        }
    }
}
