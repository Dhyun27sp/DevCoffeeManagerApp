using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var phone_number = Builders<ProductModel>.Filter.Eq("product_name", product.Product_name);
            collection.ReplaceOne(phone_number, product);
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
    }
}
