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

        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [BsonElement("time"), BsonRepresentation(BsonType.DateTime)]
        public DateTime time { get; set; }

        [BsonElement("customer")]
        public CustomerModel customer { get; set; }

        [BsonElement("tables")]
        public List<TableModel> tables { get; set; }

        [BsonElement("staff_phonenumber"), BsonRepresentation(BsonType.String)]
        public string staff_phone { get; set; }

        [BsonElement("Dishes")]
        public List<DishModel> Dishes { get; set; }

        [BsonElement("discounts")]
        public List<DiscountModel> discounts { get; set; }

        [BsonElement("payments"), BsonRepresentation(BsonType.String)]
        public string payments { get; set; }

        [BsonElement("total_amount"), BsonRepresentation(BsonType.Int32)]
        public int total_amount { get; set; }

        [BsonElement("guest_monney"), BsonRepresentation(BsonType.Int32)]
        public int guest_monney { get; set; }

        [BsonElement("change"), BsonRepresentation(BsonType.Int32)]
        public int change { get; set; }

        public ReceiptModel(DateTime time, CustomerModel customer, List<TableModel> tables, string staff_phone, List<DishModel> dishes, List<DiscountModel> discounts, string payments, int total_amount, int guest_monney, int change)
        {
            this.time = time;
            this.customer = customer;
            this.tables = tables;
            this.staff_phone = staff_phone;
            this.Dishes = dishes;
            this.discounts = discounts;
            this.payments = payments;
            this.total_amount = total_amount;
            this.guest_monney = guest_monney;
            this.change = change;
        }
    }
}
