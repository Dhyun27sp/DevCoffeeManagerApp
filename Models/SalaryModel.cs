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
    public class SalaryModel
    {
        [BsonElement("month"),BsonRepresentation(BsonType.String)]
        public string Month { get; set; }

        [BsonElement("money"),BsonRepresentation(BsonType.Int32)]
        public int Money { get; set; }
    }
}
