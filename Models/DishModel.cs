using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCoffeeManagerApp.ViewModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DevCoffeeManagerApp.Models
{

    [Serializable, BsonIgnoreExtraElements]
    public class DishModel :BaseViewModel
    {
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [BsonElement("dish_name"), BsonRepresentation(BsonType.String),BsonIgnoreIfNull]
        public string dish_name { get; set; }

        [BsonElement("ingredient"), BsonIgnoreIfNull]
        public List<IngredientModel> ingredient { get; set; }

        [BsonElement("price"), BsonRepresentation(BsonType.Int32), BsonIgnoreIfNull]
        public int? price { get; set; }

        [BsonElement("image"), BsonRepresentation(BsonType.String), BsonIgnoreIfNull]
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

        public string newDish { get; set; }

        public string HotDish { get; set; }

        public string SaleDish { get; set; }

        private string _hidden = "Hidden";
        public string Hidden
        {
            get
            {
                return _hidden;
            }
            set
            {
                _hidden = value;
                OnPropertyChanged(nameof(Hidden));
            }
        }
    }
}
