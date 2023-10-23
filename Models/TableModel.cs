using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class TableModel
    {
        [BsonElement("No."), BsonRepresentation(BsonType.Int32)]
        public int No_ { get; set; }

        [BsonElement("Number_of_seat"), BsonRepresentation(BsonType.Int32)]
        public int Seat { get; set; }
        [BsonElement("Status"), BsonRepresentation(BsonType.Boolean)]
        public bool Status { get; set; }

        [BsonElement("IsSelected"), BsonRepresentation(BsonType.Boolean)]
        public bool IsSelected { get; set; }

    }
}
