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

        public void AddReceipt(ReceiptModel receipt)
        {
            collection.InsertOne(receipt);
        }

        public List<ReceiptModel> FindReceiptInYear()
        {
            DateTime endDate = DateTime.UtcNow;  // Current date
            DateTime startDate = endDate.AddYears(-1);  // One year ago

            var filter = Builders<ReceiptModel>.Filter.Gte("time", startDate) &
                         Builders<ReceiptModel>.Filter.Lt("time", endDate);

            List<ReceiptModel> receiptsLastYear = collection.Find(filter).ToList();
            return receiptsLastYear;
        }
    }
}
