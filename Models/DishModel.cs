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
    public class DishModel : BaseViewModel
    {
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [BsonElement("dish_name"), BsonRepresentation(BsonType.String), BsonIgnoreIfNull]
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
        public string category { get; set; }

        public string Saleprice { get; set; }

        private string _strikethrough = "None";
        public string Strikethrough
        {
            get
            {
                return _strikethrough;
            }
            set
            {
                _strikethrough = value;
                OnPropertyChanged(nameof(Strikethrough));
            }
        }

        private string _quantity;

        public string Quantity
        {
            get
            {
                return _quantity;
            }

            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        private string _amount;
        public string Amount
        {
            get {
                return _amount;
            }
            set {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        private int _ordinalNumber;
        public int OrdinalNumber{
            get
            {
                return _ordinalNumber;
            }
            set
            {
                _ordinalNumber = value;
                OnPropertyChanged(nameof(OrdinalNumber));
            }
        }
        public DishModel(ObjectId _id, string dish_name, string _quantity, string Saleprice, int? price)// này cho order nhé
        {
            this._id = _id;
            this.dish_name = dish_name;
            this._quantity = _quantity;
            this.Saleprice = Saleprice;
            this.price = price;
        }
    }
}
