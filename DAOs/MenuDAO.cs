using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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

        public void CreateMenu(MenuModel menu)
        {
            collection.InsertOne(menu);
        }

        public MenuModel ReadOnetype(string name_type)
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
            List<DishModel> All_Dishs = new List<DishModel>();
            foreach (var item in All_Type_dish)
            {
                All_Dishs.AddRange(item.dish);
            }
            return All_Dishs;
        }
        public MenuModel ReadMenuOnly(ObjectId _id)
        {
            var Id = Builders<MenuModel>.Filter.Eq("_id", _id);
            MenuModel Menu = collection.Find(Id).FirstOrDefault();
            return Menu;
        }
        public MenuModel findMenubyname(string name_menu)
        {
            var Filter_name = Builders<MenuModel>.Filter.Eq("type_of_dish", name_menu);
            MenuModel Menu = collection.Find(Filter_name).FirstOrDefault();
            return Menu;
        }
        public List<DishModel> ReadAll_NewDish()
        {
            DateTime DayCurrent = DateTime.Now;
            List<DishModel> All_Dishs_Sort = new List<DishModel>();
            List<DishModel> New_Dish = new List<DishModel>();
            List<DishModel> All_Dishs = new List<DishModel>();
            All_Dishs = ReadAll_Dish();
            All_Dishs_Sort = All_Dishs.OrderByDescending(x => x.date_add).ToList();//xắp sếp món theo ngày dảm dần
            int index = 0; // index này lưu giá trị cuối cùng của điều kiện 60 ngày
            for (int i = 0; i < All_Dishs_Sort.Count(); i++)
            {
                DateTime Dayaddish = DateTime.Parse(All_Dishs_Sort[i].date_add);
                TimeSpan Distancetime = Dayaddish - DayCurrent;
                int Numbertime = Math.Abs(Distancetime.Days);
                if (Numbertime < 60)//lấy những món mới với điều kiện trong 2 tháng(món mới nhất)
                {
                    New_Dish.Add(All_Dishs_Sort[i]);
                    index = i;
                }
            }
            if (New_Dish.Count() < 3)//nếu số lượng ko đủ 3 món trong món mới 2 tháng thêm mới nhì
            {
                if ((index + 1) != All_Dishs_Sort.Count())
                {
                    for (int i = index + 1; i < All_Dishs_Sort.Count(); i++)
                    {
                        New_Dish.Add(All_Dishs_Sort[i]);
                        if (New_Dish.Count() == 3) break;
                    }
                }
            }
            return New_Dish;
        }
        public void AddDishInMenu(string type_dish, DishModel newDish)
        {
            var filter = Builders<MenuModel>.Filter.Eq("type_of_dish", type_dish);
            var update = Builders<MenuModel>.Update.Push("dish", newDish);
            // Thực hiện cập nhật
            collection.UpdateOne(filter, update);
        }
        public int CountDishesInMenu()
        {
                var unwindDishes = new BsonDocument("$unwind", "$dish");
                var groupByDishName = new BsonDocument("$group", new BsonDocument
                {
                    { "_id", "$dish.dish_name" },
                    { "count", new BsonDocument("$sum", 1) }
                });
                var pipeline = new[] { unwindDishes, groupByDishName };
                var result = collection.Aggregate<MenuDishCount>(pipeline).ToList();
                int totalDishes = Convert.ToInt32(result.Sum(x => x.Count));
                return totalDishes;
        }

        public void DeleteMenuByMenuName(string menuName)
        {
            var filter = Builders<MenuModel>.Filter.Eq("type_of_dish", menuName);
            collection.DeleteOne(filter);
        }

        public void DeleteDishByID(ObjectId dishId, string menuName)
        {
            var filter = Builders<MenuModel>.Filter.Eq("type_of_dish", menuName);
            var update = Builders<MenuModel>.Update.PullFilter("dish", Builders<DishModel>.Filter.Eq("_id", dishId));
            collection.UpdateOne(filter, update);
        }

        private class MenuDishCount
        {
            [BsonElement("_id")]
            public string DishName { get; set; }

            [BsonElement("count")]
            public int Count { get; set; }
        }
    }
}
