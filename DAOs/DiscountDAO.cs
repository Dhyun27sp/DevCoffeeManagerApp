using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

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
            List<DiscountModel> ListDiscountsvalid = new List<DiscountModel>();
            foreach (DiscountModel Discount in ListDiscount)
            {
                if (Discount.dayend >= DateTime.UtcNow)
                {
                    ListDiscountsvalid.Add(Discount);
                }
            }
            foreach (DiscountModel discount in ListDiscountsvalid) {
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
            List<DiscountModel> ListDiscountsvalid = new List<DiscountModel>();
            foreach (DiscountModel Discount in ListDiscount)
            {
                if (Discount.dayend >= DateTime.UtcNow)
                {
                    ListDiscountsvalid.Add(Discount);
                }
            }
            foreach (DiscountModel discount in ListDiscountsvalid)
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
            List<DiscountModel> ListDiscountsvalid = new List<DiscountModel>();
            foreach (DiscountModel Discount in ListDiscounts)
            {
                if (Discount.dayend >= DateTime.UtcNow)
                {
                    ListDiscountsvalid.Add(Discount);
                }
            }
            foreach (DiscountModel Discount in ListDiscountsvalid)
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
            List<DiscountModel> ListDiscountsvalid = new List<DiscountModel>();
            foreach (DiscountModel Discount in ListDiscounts)
            {
                if (Discount.dayend >= DateTime.UtcNow)
                {
                    ListDiscountsvalid.Add(Discount);
                }
            }
            foreach (DiscountModel Discount in ListDiscountsvalid)
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

        public void AddDishToDiscount(string discountId, DishModel newDish)
        {
            // Tạo filter để xác định đối tượng DiscountModel cần cập nhật
            var filter = Builders<DiscountModel>.Filter.Eq("discountid", discountId);
            newDish.dish_name = null;
            newDish.ingredient = null;
            newDish.price = null;
            newDish.image = null;
            newDish.date_add = null;
            newDish.Saleprice = null;
            // Tạo update để thêm newDish vào danh sách dish của apply
            var update = Builders<DiscountModel>.Update.Push("apply.dish", newDish);

            // Thực hiện cập nhật
            collection.UpdateOne(filter, update);
        }
        public void RemoveDishFromDiscount(string discountId, DishModel dishRemove)
        {
            var filter = Builders<DiscountModel>.Filter.Eq("discountid", discountId);

            // Xác định điều kiện để loại bỏ dish từ mảng
            var update = Builders<DiscountModel>.Update.PullFilter("apply.dish", Builders<DishModel>.Filter.Eq("_id", dishRemove._id));

            // Cập nhật đối tượng DiscountModel trong cơ sở dữ liệu
            collection.UpdateOne(filter, update);
        }
        public void RemoveMenuFromDiscount(string discountId, MenuModel nemu)
        {
            var filter = Builders<DiscountModel>.Filter.Eq("discountid", discountId);

            // Xác định điều kiện để loại bỏ dish từ mảng
            var update = Builders<DiscountModel>.Update.PullFilter("apply.menu", Builders<MenuModel>.Filter.Eq("_id", nemu.id));

            // Cập nhật đối tượng DiscountModel trong cơ sở dữ liệu
            collection.UpdateOne(filter, update);
        }
        public void AddMenuToDiscount(string discountId, MenuModel newMenu)
        {
            // Tạo filter để xác định đối tượng DiscountModel cần cập nhật
            var filter = Builders<DiscountModel>.Filter.Eq("discountid", discountId);
            newMenu.dish = new List<DishModel>();
            newMenu.type_of_dish = null;
            newMenu.detail = null;
            // Tạo update để thêm newDish vào danh sách dish của apply
            var update = Builders<DiscountModel>.Update.Push("apply.menu", newMenu);

            // Thực hiện cập nhật
            collection.UpdateOne(filter, update);
        }
    }
}
