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
            collection = db.GetCollection<TableModel>("table");
        }
        public void createTable(TableModel tableModel)
        {
            collection.InsertOne(tableModel);
        }

        public void SetStatus(int no_)
        {
            var shiftFilter = Builders<TableModel>.Filter.Eq("No.", no_); // Tạo một bộ lọc dựa trên shift
            var update = Builders<TableModel>.Update.Set("Status", false); // Sử dụng $push để thêm một phần tử vào mảng evaluate
            collection.UpdateOne(shiftFilter, update);
        }
        
    }
}
