using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DevCoffeeManagerApp.DAOs
{
    public class SupplyDAO
    {
        private IMongoCollection<SupplyModel> collection;
        public SupplyDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<SupplyModel>("Supply");
        }
        public void createSupply(SupplyModel supplyModel)
        {
            collection.InsertOne(supplyModel);
        }
        public ObservableCollection<SupplyModel> GetAllSupplies()
        {
            List<SupplyModel> supplies = collection.Find(new BsonDocument()).ToList();
            return new ObservableCollection<SupplyModel>(supplies);
        }
        public SupplyModel GetSupplyByName(String name)
        {
            var filter = Builders<SupplyModel>.Filter.Eq("product_name", name);
            return collection.Find(filter).FirstOrDefault();
 
        }

        public SupplyModel GetUnUsingSupplyByName(String name)
        {
            var filter = Builders<SupplyModel>.Filter.Eq("product_name", name) & Builders<SupplyModel>.Filter.Eq("status", "Unused");
            return collection.Find(filter).FirstOrDefault();
        }

        public SupplyModel GetUsingSupplyByName(String name)
        {
            var filter = Builders<SupplyModel>.Filter.Eq("product_name", name) & Builders<SupplyModel>.Filter.Eq("status", "In-use");
            return collection.Find(filter).FirstOrDefault();
        }
        public void SetStatus(String name, String status)
        {
            var nameFilter = Builders<SupplyModel>.Filter.Eq("product_name", name);
            var status_update = Builders<SupplyModel>.Update.Set("status", status);
            var result = collection.UpdateOne(nameFilter, status_update);
            if (result.IsAcknowledged)
            {
                MessageBox.Show(result.MatchedCount.ToString());
                return;
            }
        }

        public void SetStatusMany(String name, String status)
        {
            var nameFilter = Builders<SupplyModel>.Filter.Eq("product_name", name);
            var status_update = Builders<SupplyModel>.Update.Set("status", status);
            var result = collection.UpdateMany(nameFilter, status_update);
            if (result.IsAcknowledged)
            {
                MessageBox.Show(result.MatchedCount.ToString());
                return;
            }
        }

        public void DeleteSupplyByProductName(string productName)
        {
            var filter = Builders<SupplyModel>.Filter.Eq("product_name", productName);
            collection.DeleteOne(filter);
        }
    }
}
