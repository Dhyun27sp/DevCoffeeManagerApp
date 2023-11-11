﻿using DevCoffeeManagerApp.Config;
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
    public class CustomerDAO
    {
        private IMongoCollection<CustomerModel> collection;

        public CustomerDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<CustomerModel>("Customer");
        }

        public CustomerModel GetCustomerByPhoneNumber(string phoneNumber)
        {
            var filter = Builders<CustomerModel>.Filter.Eq(c => c.phone_number, phoneNumber);
            return collection.Find(filter).FirstOrDefault();
        }

        public ObservableCollection<CustomerModel> GetAllCustomers()
        {
            List<CustomerModel> customers = collection.Find(new BsonDocument()).ToList();
            return new ObservableCollection<CustomerModel>(customers);
        }
    }
}
