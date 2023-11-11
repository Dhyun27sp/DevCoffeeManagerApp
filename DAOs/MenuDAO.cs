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
        public ObservableCollection<MenuModel> ReadAll_Type_dish()
        {
            List<MenuModel> type_of_dish = collection.Find(new BsonDocument()).ToList();
            return new ObservableCollection<MenuModel>(type_of_dish);

        }
        public List<DishModel> ReadAll_Dish()// Tách DAO Cho Dish
        {
            ObservableCollection<MenuModel> All_Type_dish = ReadAll_Type_dish();
            List<DishModel> All_Dishs = new List< DishModel>();
            foreach (var item in All_Type_dish)
            {
                All_Dishs.AddRange(item.dish);
            }
            return All_Dishs;
        }
        public MenuModel ReadMenuOnly(ObjectId _id) {
            var Id = Builders<MenuModel>.Filter.Eq("_id", _id);
            MenuModel Menu = collection.Find(Id).FirstOrDefault();
            return Menu;
        }
    }
}
