using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace DevCoffeeManagerApp.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class ProductModel
    {
        [BsonElement("product_name"), BsonRepresentation(BsonType.String)]
        public String Product_name { get; set; }
               
        [BsonElement("stock"), BsonRepresentation(BsonType.Int32)]
        public int Stock { get; set; }

        [BsonElement("unit"), BsonRepresentation(BsonType.String)]
        public String Unit { get; set; }

        [BsonElement("EXP"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? EXP_date { get; set; }

        public ProductModel(ProductModel model)
        {
            this.Product_name = model.Product_name;           
            this.Stock = model.Stock;
            this.Unit = model.Unit;
            this.EXP_date = model.EXP_date;
        }
        public ProductModel()
        {
        }
    }
}
