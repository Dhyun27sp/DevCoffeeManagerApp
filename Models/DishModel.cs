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
    public class DishModel
    {
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [BsonElement("dish_name"), BsonRepresentation(BsonType.String)]
        public string dish_name { get; set; }

        [BsonElement("ingredient")]
        public List<IngredientModel> ingredient { get; set; }

        [BsonElement("price"), BsonRepresentation(BsonType.Int32)]
        public int price { get; set; }

        [BsonElement("image"), BsonRepresentation(BsonType.String)]
        public string image { get; set; }

        public byte[] imageconvert
        {
            get
            {
                string resuildimg = image;
                byte[] imageBytes = Convert.FromBase64String(resuildimg);
                return imageBytes;
            }
            set
            {

            }
        }
    }
}
