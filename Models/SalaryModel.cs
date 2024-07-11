using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DevCoffeeManagerApp.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class SalaryModel
    {
        [BsonElement("month"),BsonRepresentation(BsonType.DateTime), BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime Month { get; set; }

        [BsonElement("money"),BsonRepresentation(BsonType.Int32)]
        public int Money { get; set; }
    }
}
