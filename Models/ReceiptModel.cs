using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DevCoffeeManagerApp.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class ReceiptModel
    {

        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [BsonElement("receipt_code"), BsonRepresentation(BsonType.String)]
        public String receipt_code { get; set; }

        [BsonElement("time"), BsonRepresentation(BsonType.DateTime), BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime time { get; set; }

        [BsonElement("customer")]
        public CustomerModel customer { get; set; }

        [BsonElement("tables")]
        public ObservableCollection<TableModel> tables { get; set; }

        [BsonElement("staff_phonenumber"), BsonRepresentation(BsonType.String)]
        public string staff_phone { get; set; }

        [BsonElement("Dishes")]
        public List<DishModel> Dishes { get; set; }

        [BsonElement("payments"), BsonRepresentation(BsonType.String)]
        public string payments { get; set; }

        [BsonElement("ship_code"), BsonRepresentation(BsonType.String), BsonIgnoreIfNull]
        public string ship_code { get; set; }

        [BsonElement("total_amount"), BsonRepresentation(BsonType.Int32)]
        public int total_amount { get; set; }

        [BsonElement("used_point"), BsonRepresentation(BsonType.Int32)]
        public int used_point { get; set; }

        [BsonElement("guest_monney"), BsonRepresentation(BsonType.Int32)]
        public int guest_monney { get; set; }

        [BsonElement("change"), BsonRepresentation(BsonType.Int32)]
        public int change { get; set; }
        [BsonElement("note"), BsonRepresentation(BsonType.String)]
        public string note { get; set; }

        public ReceiptModel(string receipt_code, DateTime time, CustomerModel customer, ObservableCollection<TableModel> tables, string staff_phone, List<DishModel> dishes, string payments, int used_point,int total_amount, int guest_monney, int change, string note)
        {
            this.receipt_code = receipt_code;
            this.time = time;
            this.customer = customer;
            this.tables = tables;
            this.staff_phone = staff_phone;
            this.Dishes = dishes;
            this.payments = payments;
            this.used_point = used_point;
            this.total_amount = total_amount;
            this.guest_monney = guest_monney;
            this.change = change;
            this.note = note;
        }
    }
}
