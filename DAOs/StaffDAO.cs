﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using DevCoffeeManagerApp.Config;
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

        public StaffModel GetStaff(string phone_number)
        {
            var phoneFilter = Builders<StaffModel>.Filter.Eq("phone_number", phone_number); // Tạo một bộ lọc dựa trên phone_number
            StaffModel StaffModel = collection.Find(phoneFilter).FirstOrDefault(); // Thực hiện truy vấn và lấy bản ghi đầu tiên hoặc null nếu không tìm thấy.
            return StaffModel;
        }
    }
}
