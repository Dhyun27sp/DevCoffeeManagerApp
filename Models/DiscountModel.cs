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
    public class DiscountModel
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [BsonElement("discountid"), BsonRepresentation(BsonType.String)]
        public string discountid { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string name { get; set; }

        [BsonElement("detail"), BsonRepresentation(BsonType.String)]
        public string detail { get; set; }

        [BsonElement("apply")]
        public ApplyModel apply { get; set; }

        [BsonElement("daystart"), BsonRepresentation(BsonType.DateTime)]
        public DateTime daystart { get; set; }

        [BsonElement("dayend"), BsonRepresentation(BsonType.DateTime)]
        public DateTime dayend { get; set; }

        [BsonElement("value_dis"), BsonRepresentation(BsonType.String)]
        public string value_dis { get; set; }

        public DiscountModel(string discountid,string name,string detail,ApplyModel apply,DateTime daystart,DateTime dayend,string value_dis) {
            this.discountid = discountid;
            this.name = name;
            this.detail = detail;
            this.apply = apply;
            this.daystart = daystart;
            this.dayend = dayend;
            this.value_dis = value_dis;
        }
    }
}
