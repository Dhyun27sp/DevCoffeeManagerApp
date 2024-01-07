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
            var filter = Builders<SupplyModel>.Filter.Eq("product_name", name) & Builders<SupplyModel>.Filter.Ne("status", "unused");
            SupplyModel firstRecord = collection.Find(filter).FirstOrDefault();
            if (firstRecord !=null)
                return new  SupplyModel(firstRecord);
            else return null;
        }
        public void SetStatus(String name, String status)
        {
            var nameFilter = Builders<SupplyModel>.Filter.Eq("product_name", name);
            var status_update = Builders<SupplyModel>.Update.Set("status", status);
            UpdateResult result = collection.UpdateOne(nameFilter, status_update);
        }

        public void DeleteSupplyByProductName(string productName)
        {
            var filter = Builders<SupplyModel>.Filter.Eq("product_name", productName);
            collection.DeleteOne(filter);
        }
    }
}
