using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;
using System.Windows;

namespace DevCoffeeManagerApp.DAOs
{
    public class StaffDAO
    {
        private IMongoCollection<StaffModel> collection;
        public StaffDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<StaffModel>("Staff");
        }

        public void createStaff(StaffModel staff)
        {
            collection.InsertOne(staff);
        }

        public List<StaffModel> ReadAll()
        {
            List<StaffModel> listStaff = collection.Find(new BsonDocument()).ToList();
            return listStaff;
        }

        public void delete_shift_in_schedule(string phone_number, string shiftpresent)
        {
            var filter = Builders<StaffModel>.Filter.Eq("phone_number", phone_number); // Tạo bộ lọc theo phone_number
            var update = Builders<StaffModel>.Update.Pull("schedule", shiftpresent); // Tạo một bản cập nhật để xóa shiftpresent

            UpdateResult updateResult = collection.UpdateOne(filter, update); // Thực hiện cập nhật

        }
    }
}
