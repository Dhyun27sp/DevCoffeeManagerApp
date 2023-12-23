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
    public class ProductModel
    {
        [BsonElement("product_name"), BsonRepresentation(BsonType.String)]
        public String Product_name { get; set; }

        [BsonElement("product_id"), BsonRepresentation(BsonType.String)]
        public String Product_id { get; set; }
               
        [BsonElement("stock"), BsonRepresentation(BsonType.Int32)]
        public int Stock { get; set; }

        [BsonElement("unit"), BsonRepresentation(BsonType.String)]
        public String Unit { get; set; }

        [BsonElement("EXP"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? EXP_date { get; set; }

        [BsonElement("manufacturer"), BsonRepresentation(BsonType.String)]
        public String Manufacturer { get; set; }
        public ProductModel(ProductModel model)
        {
            this.Product_name = model.Product_name;
            this.Product_id = model.Product_id;            
            this.Stock = model.Stock;
            this.Unit = model.Unit;
            this.EXP_date = model.EXP_date;
            this.Manufacturer = model.Manufacturer;
        }
        public ProductModel()
        {
        }
    }
}
