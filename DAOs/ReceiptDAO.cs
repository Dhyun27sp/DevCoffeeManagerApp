using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;

namespace DevCoffeeManagerApp.DAOs
{
    public class ReceiptDAO
    {
        private IMongoCollection<ReceiptModel> collection;
        
        public ReceiptDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<ReceiptModel>("Receipt");
        }

        public List<ReceiptModel> ReadAll()
        {
            List<ReceiptModel> listReceipt = collection.Find(new BsonDocument()).ToList();
            return listReceipt;
        }

        public void AddReceipt(ReceiptModel receipt)
        {
            collection.InsertOne(receipt);
        }

        public ReceiptModel FindReceiptbyReceiptCode(string receipt)
        {
            var filter = Builders<ReceiptModel>.Filter.Eq("receipt_code", receipt);
            var item = collection.Find(filter).FirstOrDefault();
            return item;
        }

        public List<DishModel> FindOrderbyReceiptCode(string receipt)
        {
            var filter = Builders<ReceiptModel>.Filter.Eq("receipt_code", receipt);
            var item = collection.Find(filter).FirstOrDefault();
            return item.Dishes;            
        }

        public void UpdateOrder (string receipt, string ship)
        {
            // Điều kiện để tìm bản ghi duy nhất
            var filter = Builders<ReceiptModel>.Filter.Eq("receipt_code", receipt);

            // Trường mới cần thêm vào bản ghi
            var update = Builders<ReceiptModel>.Update.Set("ship_code", ship);

            // Tùy chọn cập nhật: cập nhật một bản ghi duy nhất
            var updateOptions = new UpdateOptions { IsUpsert = false };
            collection.UpdateOne(filter, update, updateOptions);
        } 

        public List<ReceiptModel> FindReceiptInYear()
        {
            DateTime endDate = DateTime.UtcNow;  // Current date
            DateTime startDate = endDate.AddYears(-1);  // One year ago

            var filter = Builders<ReceiptModel>.Filter.Gte("time", startDate) &
                         Builders<ReceiptModel>.Filter.Lt("time", endDate);

            List<ReceiptModel> receiptsLastYear = collection.Find(filter).ToList();
            return receiptsLastYear;
        }
        public void DeleteReceiptByID(ObjectId id)
        {
            var filter = Builders<ReceiptModel>.Filter.Eq("_id", id);
            collection.DeleteOne(filter);
        }
        public List<ReceiptModel> FindReceiptOnDate(DateTime date)
        {
            DateTime startDate = date.AddDays(-2);
            DateTime endDate = date.AddDays(2);
            var filter = Builders<ReceiptModel>.Filter.Gte("time", startDate) &
                         Builders<ReceiptModel>.Filter.Lt("time", endDate);
            List<ReceiptModel> receiptsOnDate = collection.Find(filter).ToList();
            List<ReceiptModel> receiptsOnDatereal = receiptsOnDate.Where(receipt => receipt.time.Day == date.Day).ToList();
            return receiptsOnDatereal;
        }

        public int GetTotalRevenue()
        {
            var totalAmountSum = collection.AsQueryable()
                .Sum(receipt => receipt.total_amount);
            return totalAmountSum;
        }
        public List<ReceiptModel> FindReceiptsInMonth()
        {
            // Xác định tháng và năm hiện tại
            DateTime currentDate = DateTime.UtcNow;  // Lấy ngày hiện tại (theo múi giờ UTC)
            int currentMonth = currentDate.Month;
            int currentYear = currentDate.Year;

            // Tính toán ngày đầu tiên của tháng hiện tại
            DateTime endDate = new DateTime(currentYear, currentMonth, DateTime.DaysInMonth(currentYear, currentMonth));

            // Nếu tháng hiện tại là tháng 1, thì tháng trước đó là tháng 12 của năm trước
            int previousMonth = currentMonth == 1 ? 12 : currentMonth - 1;
            int previousYear = currentMonth == 1 ? currentYear - 1 : currentYear;

            // Tính toán ngày cuối cùng của tháng trước đó
            DateTime startDate = new DateTime(previousYear, previousMonth, 1);

            // Lọc hóa đơn trong khoảng thời gian này
            var filter = Builders<ReceiptModel>.Filter.Gte("time", startDate) &
                         Builders<ReceiptModel>.Filter.Lte("time", endDate);

            List<ReceiptModel> receiptsInTwoMonths = collection.Find(filter).ToList();
            return receiptsInTwoMonths;
        }

        public List<ReceiptModel> GetData3Month()
        {
            var data = new List<ReceiptModel>();

            // Lọc hóa đơn trong khoảng thời gian này
            var startDate = DateTime.Now.AddMonths(-3);
            var filter = Builders<ReceiptModel>.Filter.Gte("time", startDate);
            List<ReceiptModel> receiptsInThreeMonths = collection.Find(filter).ToList();
            data = receiptsInThreeMonths.OrderBy(x => x.time).ToList();
            return data;
        }
    }
}
