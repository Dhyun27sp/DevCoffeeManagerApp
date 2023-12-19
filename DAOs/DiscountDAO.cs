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
        private MenuDAO menuDAO = new MenuDAO();
        public DiscountDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<DiscountModel>("Discount");
        }
        public void createdistcount(DiscountModel distcount)
        {
            collection.InsertOne(distcount);
        }
        public void UpdateDiscount(DiscountModel distcount)
        {
            distcount._id = findisbyId(distcount.discountid)._id;
            var discountid = Builders<DiscountModel>.Filter.Eq("discountid", distcount.discountid);
            collection.ReplaceOne(discountid, distcount);
        }
        public void DeleteDiscountBydiscountid(string distcountid)
        {
            var filter = Builders<DiscountModel>.Filter.Eq("discountid", distcountid);
            collection.DeleteOne(filter);
        }

        public List<DiscountModel> ReadDiscountAll()
        {
            List<DiscountModel> ListDiscount = collection.Find(new BsonDocument()).ToList();
            return ListDiscount;
        }



        public DiscountModel findisbyId(string disId)
        {
            var Filter = Builders<DiscountModel>.Filter.Eq("discountid", disId);
            DiscountModel discount = collection.Find(Filter).FirstOrDefault();
            return discount;
        }

        public List<MenuModel> ListMenuDiscount()
        {
            List<DiscountModel> ListDiscount = new List<DiscountModel>();
            List<MenuModel> ListMenuDiscountInDiscountModel = new List<MenuModel>();
            List<MenuModel> ListMenuDiscountReal = new List<MenuModel>();
            ListDiscount = ReadDiscountAll();
            foreach(DiscountModel discount in ListDiscount) {
                ListMenuDiscountInDiscountModel.AddRange(discount.apply.menu);
            }
            foreach(MenuModel menu in ListMenuDiscountInDiscountModel)
            {
                ListMenuDiscountReal.Add(menuDAO.ReadMenuOnly(menu.id));
            }
            return ListMenuDiscountReal;
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

        public List<string> MutiPriceSaleWithDish(ObjectId IdDish)
        {
            List<string> pricedishdiscounts = new List<string>();
            List<DiscountModel> ListDiscounts = new List<DiscountModel>();
            ListDiscounts = ReadDiscountAll();
            foreach(DiscountModel Discount in ListDiscounts)
            {
                foreach (var Dishsale in Discount.apply.dish)
                {
                    if(Dishsale._id == IdDish)
                    {
                        pricedishdiscounts.Add(Discount.value_dis);
                        break;
                    }
                }
            }
            return pricedishdiscounts;
        }

        public List<string> MutiPriceSaleWithMenu(ObjectId IdMenu)
        {
            List<string> pricemenudiscount = new List<string>();
            List<DiscountModel> ListDiscounts = new List<DiscountModel>();
            ListDiscounts = ReadDiscountAll();
            foreach (DiscountModel Discount in ListDiscounts)
            {
                foreach (var Menusale in Discount.apply.menu)
                {
                    if(Menusale.id == IdMenu)
                    {
                        pricemenudiscount.Add(Discount.value_dis);
                        break;
                    }
                }
            }

            return pricemenudiscount;
        }
    }
}
