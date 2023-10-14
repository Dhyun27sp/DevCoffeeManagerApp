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

        public StaffModel GetStaff(string phone_number, string month)
        {
            var phoneFilter = Builders<StaffModel>.Filter.Eq("phone_number", phone_number); // Tạo một bộ lọc dựa trên phone_number
            var monthFilter = Builders<StaffModel>.Filter.Eq("salary.month", month);
            var andFilter = Builders<StaffModel>.Filter.And(phoneFilter, monthFilter);
            StaffModel StaffModel = collection.Find(andFilter).FirstOrDefault(); // Thực hiện truy vấn và lấy bản ghi đầu tiên hoặc null nếu không tìm thấy.
            return StaffModel;
        }

        public void delete_shift_in_schedule_of_Staff(string phone_number,string month, string shiftpresent)
        {
            var phoneFilter = Builders<StaffModel>.Filter.Eq("phone_number", phone_number); // Tạo bộ lọc theo phone_number
            var monthFilter = Builders<StaffModel>.Filter.Eq("salary.month", month);
            var andFilter = Builders<StaffModel>.Filter.And(phoneFilter, monthFilter);
            var update = Builders<StaffModel>.Update.Pull("schedule", shiftpresent); // Tạo một bản cập nhật để xóa shiftpresent

            UpdateResult updateResult = collection.UpdateOne(andFilter, update); // Thực hiện cập nhật

        }
    }
}
