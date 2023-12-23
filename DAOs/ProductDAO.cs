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

        public void CreateCustomer(ProductModel product)
        {
            collection.InsertOne(product);
        }

        //public void UpdateCustomer(ProductModel customer)
        //{
        //    var phone_number = Builders<ProductModel>.Filter.Eq("phone_number", customer.phone_number);
        //    collection.ReplaceOne(phone_number, customer);
        //}        

    }
}
