using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;
using System.Security.Cryptography;

namespace DevCoffeeManagerApp.DAOs
{
    public class ReceiptDAO
    {
        private IMongoCollection<ReceiptModel> collection;
        
        public ReceiptDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<ReceiptModel>("Receipt");
        }

        public List<ReceiptModel> ReadAll()
        {
            List<ReceiptModel> listReceipt = collection.Find(new BsonDocument()).ToList();
            return listReceipt;
        }

        /*public void test_update_time()
        {
            var hexString = "6505c2319e966625332da9f3";
            var objectId = new ObjectId(hexString);

            var Filter = Builders<ReceiptModel>.Filter.Eq("_id", objectId); // Tạo một bộ lọc dựa trên shift
            var update = Builders<ReceiptModel>.Update.Set("time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            collection.UpdateOne(Filter, update);
        }*/

        //public void test_add_table()
        //{
        //    TableModel table = new TableModel(1);
        //    var hexString = "6505c2319e966625332da9f3";

        //    var objectId = new ObjectId(hexString);
        //    var shiftFilter = Builders<ReceiptModel>.Filter.Eq("_id", objectId); // Tạo một bộ lọc dựa trên shift
        //    var update = Builders<ReceiptModel>.Update.Push("tables", table); 
        //    collection.UpdateOne(shiftFilter, update);
        //}

        public void AddReceipt(ReceiptModel receipt)
        {
            collection.InsertOne(receipt);
        }
    }
}
