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
    public class DiscountDAO
    {
        private IMongoCollection<DiscountModel> collection;
        public DiscountDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<DiscountModel>("Discount");
        }

        public List<DiscountModel> ReadDiscountAll()
        {
            List<DiscountModel> ListDiscount = collection.Find(new BsonDocument()).ToList();
            return ListDiscount;
        }

        public List<MenuModel> ListMenuDiscount()
        {
            List<DiscountModel> ListDiscount = new List<DiscountModel>();
            List<MenuModel> ListMenuDiscount = new List<MenuModel>();
            ListDiscount = ReadDiscountAll();
            foreach(DiscountModel discount in ListDiscount) {
                ListMenuDiscount.AddRange(discount.apply.menu);
            }
            return ListMenuDiscount;
        }

        public List<DishModel> ListDishDiscount()
        {
            List<DiscountModel> ListDiscount = new List<DiscountModel>();
            List<DishModel> ListDishDiscount = new List<DishModel>();
            ListDiscount = ReadDiscountAll();
            foreach (DiscountModel discount in ListDiscount)
            {
                ListDishDiscount.AddRange(discount.apply.dish);
            }
            return ListDishDiscount;
        }
    }
}
