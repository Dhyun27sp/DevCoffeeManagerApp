using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DevCoffeeManagerApp.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class ReceiptModel
    {
        public ReceiptModel()
        {
        }

        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [BsonElement("time"), BsonRepresentation(BsonType.String)]
        public string time { get; set; }

        [BsonElement("customer")]
        public CustomerModel phone_number { get; set; }

        [BsonElement("tables")]
        public List<TableModel> tables { get; set; }

        [BsonElement("staff_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId staff_id { get; set; }

        [BsonElement("Dishes")]
        public List<DishModel> Dishes { get; set; }

        [BsonElement("discounts")]
        public List<DiscountModel> discounts { get; set; }

        [BsonElement("payments"), BsonRepresentation(BsonType.String)]
        public string payments { get; set; }


    }
}
