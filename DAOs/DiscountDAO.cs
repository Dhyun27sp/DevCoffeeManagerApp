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

        public List<DiscountModel> ReadDiscountAll()
        {
            List<DiscountModel> ListDiscount = collection.Find(new BsonDocument()).ToList();
            return ListDiscount;
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

        public string PriceSaleWithDish(ObjectId IdDish)
        {
            string pricedishdiscount = "0";
            int outsite = 0;
            List<DiscountModel> ListDiscounts = new List<DiscountModel>();
            ListDiscounts = ReadDiscountAll();
            foreach(DiscountModel Discount in ListDiscounts)
            {
                foreach (var Dishsale in Discount.apply.dish)
                {
                    if(Dishsale._id == IdDish)
                    {
                        pricedishdiscount = Discount.value_dis;
                        outsite = 1;
                        break;
                    }
                }
                if(outsite == 1)
                {
                    break;
                }
            }
            return pricedishdiscount;
        }

        public string PriceSaleWithMenu(ObjectId IdMenu)
        {
            string pricemenudiscount = "0";
            int outsite = 0;
            List<DiscountModel> ListDiscounts = new List<DiscountModel>();
            ListDiscounts = ReadDiscountAll();
            foreach (DiscountModel Discount in ListDiscounts)
            {
                foreach (var Menusale in Discount.apply.menu)
                {
                    if(Menusale.id == IdMenu)
                    {
                        pricemenudiscount = Discount.value_dis;
                        outsite = 1;
                        break;
                    }
                }
                if (outsite == 1)
                {
                    break;
                }
            }

            return pricemenudiscount;
        }
    }
}
