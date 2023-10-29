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
    public class MenuDAO
    {
        private IMongoCollection<MenuModel> collection;
        public MenuDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<MenuModel>("Menu");
        }

        public MenuModel ReadAll(string name_type)
        {
            var type_of_dish_Filter = Builders<MenuModel>.Filter.Eq("type_of_dish", name_type);
            MenuModel type_of_dish = collection.Find(type_of_dish_Filter).FirstOrDefault();
            return type_of_dish;
        }
    }
}
