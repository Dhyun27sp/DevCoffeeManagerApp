using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;
using System.Text.RegularExpressions;

namespace DevCoffeeManagerApp.DAOs
{
    public class ScheduleDAO
    {
        private IMongoCollection<ScheduleModel> collection;
        public ScheduleDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<ScheduleModel>("Schedule");
        }

        public void createSchedule(ScheduleModel scheduleModel)
        {
            collection.InsertOne(scheduleModel);
        }
        public void AddEvaluteWithIdSchedule(EvaluateModel evaluate, ObjectId scheduleid)
        {
            // Tạo filter để xác định đối tượng DiscountModel cần cập nhật
            var filter = Builders<ScheduleModel>.Filter.Eq("_id", scheduleid);
            // Tạo update để thêm newDish vào danh sách dish của apply
            var update = Builders<ScheduleModel>.Update.Push("evaluate", evaluate);

            // Thực hiện cập nhật
            collection.UpdateOne(filter, update);
        }
        public void RemoveEvaluateFromSchedule(EvaluateModel evaluate, ObjectId scheduleid)
        {
            var filter = Builders<ScheduleModel>.Filter.Eq("_id", scheduleid);

            // Xác định điều kiện để loại bỏ dish từ mảng
            var update = Builders<ScheduleModel>.Update.PullFilter("evaluate", Builders<EvaluateModel>.Filter.Eq("staff_id", evaluate.staff_id));

            // Cập nhật đối tượng DiscountModel trong cơ sở dữ liệu
            collection.UpdateOne(filter, update);
        }
        public ScheduleModel GetLatestSchedule()
        {
            // Sắp xếp các document theo trường "_id" giảm dần (từ mới đến cũ).
            var sortDefinition = Builders<ScheduleModel>.Sort.Descending("_id");

            // Lấy document đầu tiên sau khi đã sắp xếp.
            var latestSchedule = collection.Find(new BsonDocument()).Sort(sortDefinition).Limit(1).FirstOrDefault();

            return latestSchedule;
        }
        public List<ScheduleModel> GetNSchedules(int n)
        {
            // Sắp xếp các document theo trường "_id" giảm dần (từ mới đến cũ).
            var sortDefinition = Builders<ScheduleModel>.Sort.Descending("_id");

            // Lấy n document cuối cùng sau khi đã sắp xếp.
            var schedules = collection.Find(new BsonDocument()).Sort(sortDefinition).Limit(n).ToList();

            return schedules;
        }

        public List<ScheduleModel> findSchedulebymothCurrent() {
            var regexPattern = new BsonRegularExpression(new Regex(Regex.Escape(DateTime.Now.ToString("MM/yyyy")), RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace));

            // Tìm kiếm dữ liệu
            var filter = Builders<ScheduleModel>.Filter.Regex("shift", regexPattern);
            var result = collection.Find(filter).ToList();
            return result;
        }
        public List<ScheduleModel> findSchedulebyDayCurrent()
        {
            var regexPattern = new BsonRegularExpression(new Regex(Regex.Escape(DateTime.Now.ToString("dd/MM/yyyy")), RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace));

            // Tìm kiếm dữ liệu
            var filter = Builders<ScheduleModel>.Filter.Regex("shift", regexPattern);
            var result = collection.Find(filter).ToList();
            return result;
        }
        public void DeleteScheduleByid(ObjectId Id)
        {
            var filter = Builders<ScheduleModel>.Filter.Eq("_id", Id);
            collection.DeleteOne(filter);
        }
        public List<ScheduleModel> GetAllSchedule()
        {
            List<ScheduleModel> schedules = collection.Find(new BsonDocument()).ToList();
            return schedules;
        }
        public ScheduleModel GetSchedule(string Shift)
        {
            var ShiftFilter = Builders<ScheduleModel>.Filter.Eq("shift", Shift); // Tạo một bộ lọc dựa trên mã shift
            ScheduleModel ScheduleModel = collection.Find(ShiftFilter).FirstOrDefault(); // Thực hiện truy vấn và lấy bản ghi đầu tiên hoặc null nếu không tìm thấy.
            return ScheduleModel;
        }

        public void AddEvaluate(string Shift ,EvaluateModel evaluate)
        {
            var shiftFilter = Builders<ScheduleModel>.Filter.Eq("shift", Shift); // Tạo một bộ lọc dựa trên shift
            var update = Builders<ScheduleModel>.Update.Push("evaluate", evaluate); // Sử dụng $push để thêm một phần tử vào mảng evaluate
            collection.UpdateOne(shiftFilter, update);
        }

        public void Worked_for_Staff(string shift, ObjectId staffId)
        {
            var shiftFilter = Builders<ScheduleModel>.Filter.Eq("shift", shift); // Tạo một bộ lọc dựa trên shift
            var evaluateFilter = Builders<ScheduleModel>.Filter.ElemMatch("evaluate", Builders<EvaluateModel>.Filter.Eq("staff_id", staffId)); // Tạo bộ lọc để tìm đối tượng EvaluateModel trong mảng evaluate có staff_id tương ứng

            var update = Builders<ScheduleModel>.Update.Set("evaluate.$[elem].worked", true); // Sử dụng $set để cập nhật trường worked của EvaluateModel tương ứng

            var arrayFilters = new List<ArrayFilterDefinition>
    {
        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("elem.staff_id", staffId))
    };

            var options = new UpdateOptions
            {
                ArrayFilters = arrayFilters
            };

            collection.UpdateOne(shiftFilter, update, options);
        }
        public List<EvaluateModel> GetEvalute(string shift)
        {
            List<EvaluateModel> evaluates = new List<EvaluateModel>();
            List<string> staffids = new List<string>();
            var ShiftFilter = Builders<ScheduleModel>.Filter.Eq("shift", shift);
            ScheduleModel schedulemodel = collection.Find(ShiftFilter).FirstOrDefault();
            try
            {
                if(schedulemodel != null)
                { 
                    evaluates = schedulemodel.evaluate;
                }
                else
                {
                    return evaluates;
                }
            }
            catch { }
                
            return evaluates;
        }
    }
}
