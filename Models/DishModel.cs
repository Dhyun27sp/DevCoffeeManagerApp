using System;
using System.Collections.Generic;
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
        public List<ProductModel> ingredient { get; set; }

        [BsonElement("price"), BsonRepresentation(BsonType.Int32), BsonIgnoreIfNull]
        public int? price { get; set; }

        [BsonElement("image"), BsonRepresentation(BsonType.String), BsonIgnoreIfNull]
        public string image { get; set; }

        [BsonElement("dateadd"), BsonRepresentation(BsonType.String), BsonIgnoreIfNull]
        public string date_add { get; set; }

        [BsonIgnore]
        public byte[] imageconvert
        {
            get
            {
                if(image != null) {
                    string resuildimg = image;
                    byte[] imageBytes = Convert.FromBase64String(resuildimg);
                    return imageBytes;
                }
                return null;
            }
            set
            {

            }
        }
        [BsonIgnore]
        public bool? newDish { get; set; }// use bool
        [BsonIgnore]
        public bool? HotDish { get; set; }// use bool
        [BsonIgnore]
        public bool? SaleDish { get; set; }//use bool
        [BsonIgnore]
        public string category { get; set; }

        public int? Saleprice { get; set; }

        private int _quantity;

        private string _ice;
        private string _sugar;

        public int Quantity
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
        private int _amount;
        public int Amount
        {
            get {
                return _amount;
            }
            set {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
        public string Ice
        {
            get
            {
                return _ice;
            }
            set
            {
                _ice = value;
                OnPropertyChanged(nameof(Ice));
            }
        }

        public string Sugar
        {
            get
            {
                return _sugar;
            }
            set
            {
                _sugar = value;
                OnPropertyChanged(nameof(Sugar));
            }
        }


        public DishModel(ObjectId _id, string dish_name, int _quantity, int? Saleprice, int? price, List<ProductModel> ingredient, string Sugar, string Ice)// này cho order nhé
        {
            this._id = _id;
            this.dish_name = dish_name;
            this._quantity = _quantity;
            this.Saleprice = Saleprice;
            this.price = price;
            this.ingredient= ingredient;
            this.Sugar = Sugar;
            this.Ice = Ice;
        }

        public DishModel(ObjectId _id,string dishName,List<ProductModel> ingredients,int? dishPrice,string dishImage,
        string dateAdd,string dishCategory,int? salePrice,int quantity,int amount)
        {
            this._id = _id;
            this.dish_name = dishName;
            this.ingredient = ingredients;
            this.price = dishPrice;
            this.image = dishImage;
            this.date_add = dateAdd;
            this.category = dishCategory;
            this.Saleprice = salePrice;
            this.Quantity = quantity;
            this.Amount = amount;
        }
        public DishModel() { }

    }
}
