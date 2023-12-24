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
    public class SupplyModel
    {
        [BsonElement("product_name"), BsonRepresentation(BsonType.String)]
        public String Product_name { get; set; }

        [BsonElement("status"), BsonRepresentation(BsonType.String)]
        public String Status { get; set; }

        [BsonElement("date"), BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; }

        [BsonElement("price"), BsonRepresentation(BsonType.Int32)]
        public int Price { get; set; }

        [BsonElement("quantity"), BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }

        [BsonElement("unit"), BsonRepresentation(BsonType.String)]
        public String Unit { get; set; }

        [BsonElement("MFG"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? MFG_date { get; set; }

        [BsonElement("EXP"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? EXP_date { get; set; }

        [BsonElement("company"), BsonRepresentation(BsonType.String)]
        public String Company { get; set; }
        public SupplyModel(SupplyModel model)
        {
            this.Product_name = model.Product_name;
            this.Status = model.Status;
            this.Date = model.Date;
            this.Price = model.Price;
            this.Quantity = model.Quantity;
            this.Unit = model.Unit;
            this.MFG_date = model.MFG_date;
            this.EXP_date = model.EXP_date;
            this.Company = model.Company;
        }
        public SupplyModel()
        {
        }
    }
}
