using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DevCoffeeManagerApp.DAOs
{
    public class ProductDAO
    {
        private IMongoCollection<ProductModel> collection;

        public ProductDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<ProductModel>("Product");
        }

        public ObservableCollection<ProductModel> GetAllProducts()
        {
            List<ProductModel> customers = collection.Find(new BsonDocument()).ToList();
            return new ObservableCollection<ProductModel>(customers);
        }
        public ProductModel GetProductbyName(string name)
        {
            var filter = Builders<ProductModel>.Filter.Eq("Product_name", name);
            return collection.Find(filter).FirstOrDefault();
        }
        public ProductModel GetProductbyId(ObjectId Id)
        {
            var filter = Builders<ProductModel>.Filter.Eq("_id", Id);
            return collection.Find(filter).FirstOrDefault();
        }
        public List<String> GetAllProductName()
        {
            List<ProductModel> customers = collection.Find(new BsonDocument()).ToList();
            List<String> products = new List<String>();
            foreach (var document in customers)
            {
                products.Add(document.Product_name);
            }
            return products;
        }

        public void CreateProduct(ProductModel product)
        {
            collection.InsertOne(product);
        }

        public void UpdateProduct(ProductModel product)
        {
            var update = Builders<ProductModel>.Filter.Eq("_id", product.Id);
            collection.ReplaceOne(update, product);
        }
        public void DeleteProductByProductName(string productName)
        {
            var filter = Builders<ProductModel>.Filter.Eq("product_name", productName);
            collection.DeleteOne(filter);
        }
        public int CountProducts()
        {
            int productCount = Convert.ToInt32(collection.CountDocuments(new BsonDocument()));
            return productCount;
        }
        public void MinusProduct(ObservableCollection<DishModel> dish)
        {
            foreach (var item in dish)
            {
                int i = item.Quantity;
                foreach (var product in item.ingredient)
                {
                    int usedamount = product.Stock;
                    var filter = Builders<ProductModel>.Filter.Eq("product_name", product.Product_name);
                    var update = Builders<ProductModel>.Update.Inc("stock", -usedamount * i);
                    collection.UpdateOne(filter, update);
                }
            }
            return;
        }
        public void CheckAvailableProduct(List<DishModel> order)
        {
            bool flag = false;
            List<DishModel> list_dish = new List<DishModel>();
            foreach (var item in order)
            {
                int count = item.Quantity;
                DishModel dish = new DishModel();
                dish.dish_name = item.dish_name;
                foreach (var ingredient in item.ingredient)
                {
                    ProductModel product = new ProductModel();
                    product = this.GetProductbyName(ingredient.Product_name);
                    // Tính số lượng có thể phục vụ của món = số tồn kho / SL trong công thức món
                    int available = product.Stock / ingredient.Stock; 
                    if (count > available)
                    {
                        dish.Quantity = available;
                        count = dish.Quantity;
                        flag = true;
                    }                        
                }
                list_dish.Add(dish);
            }
            if (flag)
            {
                string result = $"Bạn chỉ có thể mua: {string.Join(", ", list_dish.Select(kvp => $"{kvp.Quantity} {kvp.dish_name}"))}.";
                throw new Exception(result);
            }

        }
        private Tuple<Boolean, int> checkPositiveStock(FilterDefinition<ProductModel> filter, int usedamount)
        {
            ProductModel product = collection.Find(filter).FirstOrDefault();
            if (product.Stock < 0)
            {
                int unsellNum = -(product.Stock / usedamount);
                if (unsellNum * usedamount + product.Stock < 0)
                    unsellNum++;
                return Tuple.Create(false, unsellNum);
            }
            else
                return Tuple.Create(false, 0);
        }
    }
}
