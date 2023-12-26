using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;
using System.Windows;
using System.Collections.ObjectModel;

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
