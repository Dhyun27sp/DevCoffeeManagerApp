using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.DAOs
{
    public class TableDAO
    {
        private IMongoCollection<TableModel> collection;
        public TableDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<TableModel>("Table");
        }
        public void createTable(TableModel tableModel)
        {
            collection.InsertOne(tableModel);
        }
        public List<TableModel> ReadAll()
        {
            List<TableModel> listTable = collection.Find(new BsonDocument()).ToList();
            return listTable;
        }
        public void SetStatus(int no_)
        {
            var shiftFilter = Builders<TableModel>.Filter.Eq("No.", no_);
            var false_update = Builders<TableModel>.Update.Set("Status", false); 
            UpdateResult result = collection.UpdateOne(shiftFilter, false_update);
            if (result.ModifiedCount == 0)
            {
                var true_update = Builders<TableModel>.Update.Set("Status", true);
                collection.UpdateOne(shiftFilter, true_update);
            }
        }
        
    }
}
