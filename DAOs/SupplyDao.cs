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
        //public void SetStatus(int no_)
        //{
        //    var shiftFilter = Builders<TableModel>.Filter.Eq("No.", no_);
        //    var false_update = Builders<TableModel>.Update.Set("Status", false);
        //    UpdateResult result = collection.UpdateOne(shiftFilter, false_update);
        //    if (result.ModifiedCount == 0)
        //    {
        //        var true_update = Builders<TableModel>.Update.Set("Status", true);
        //        collection.UpdateOne(shiftFilter, true_update);
        //    }
        //}

    }
}
