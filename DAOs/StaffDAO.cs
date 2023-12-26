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
using System.Windows.Documents;
using System.Collections.ObjectModel;
using System.Collections;
using MaterialDesignThemes.Wpf;
using System.Xml.Linq;

namespace DevCoffeeManagerApp.DAOs
{
    public class StaffDAO
    {
        ScheduleDAO scheduledao = new ScheduleDAO();
        private IMongoCollection<StaffModel> collection;
        public StaffDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<StaffModel>("Staff");
        }

        public void createStaff(StaffModel staff)
        {
            collection.InsertOne(staff);
        }

        public List<StaffModel> ReadAll()
        {
            List<StaffModel> listStaff = collection.Find(new BsonDocument()).ToList();
            listStaff.RemoveAll(s => s.account.Role == "admin");
            return listStaff;
        }

        public StaffModel GetStaff(string phone_number)
        {
            var phoneFilter = Builders<StaffModel>.Filter.Eq("phone_number", phone_number); // Tạo một bộ lọc dựa trên phone_number
            StaffModel StaffModel = collection.Find(phoneFilter).FirstOrDefault(); // Thực hiện truy vấn và lấy bản ghi đầu tiên hoặc null nếu không tìm thấy.
            return StaffModel;
        }
        public void Createsalary()
        {
            List<StaffModel> listStaff = ReadAll();
            foreach (var item in listStaff)
            {
                SalaryModel salary = new SalaryModel();// tạo salary 
                bool allowedcreatesalary = false;// biến cho phép tạo mới nếu nhân viên chưa có salary trong tháng
                foreach (var item1 in item.salary)// để vòng lặp ngược sẽ tốt hơn 
                {
                    if(item1.Month.Month == DateTime.Now.Month && item1.Month.Year == DateTime.Now.Year)
                    {
                        allowedcreatesalary = true; 
                        break;
                    }
                }
                if(allowedcreatesalary == false)//nếu chưa có tạo thêm
                {
                    salary.Month = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 4);
                    salary.Money = 0;
                    var filter = Builders<StaffModel>.Filter.Eq("phone_number", item.phone_staff);
                    var update = Builders<StaffModel>.Update.Push("salary", salary);
                    collection.UpdateOne(filter, update);
                }
            }
        }
        public void salaryInMoth()
        {
            List<ScheduleModel> scheduleModels = new List<ScheduleModel>();//lấy hết lịch nhân viên 
            List<StaffModel> StaffModel = new List<StaffModel>();// lấy hết danh sách ra 
            scheduleModels = scheduledao.GetAllSchedule();
            StaffModel = ReadAll();
            List<ScheduleModel> filteredSchedules = scheduleModels.Where(s => s.shift.Contains(DateTime.Now.ToString("MM/yyyy"))).ToList();//lấy lịch trong tháng hiện tại
            List<EvaluateModel> evaluateModels = new List<EvaluateModel>();//lây danh sách evalua
            List<Dictionary<ObjectId, int>> staffDics = new List<Dictionary<ObjectId, int>>();//Dictionary bộ key-value, mục dích tìm kiếm trong 
            foreach (var i in StaffModel)
            {
                Dictionary<ObjectId, int> staffDic = new Dictionary<ObjectId, int>();
                staffDic[i.staffid] = 0;
                staffDics.Add(staffDic);
            }
            foreach (var i in filteredSchedules)
            {
                foreach (var item in i.evaluate)
                {
                    foreach (var item1 in staffDics)
                    {
                        if (item1.ContainsKey(item.staff_id))
                        {
                            if(item.worked == true)
                            {
                                item1[item.staff_id]++;
                                break;
                            } 
                        }
                    }
                }
            }
            foreach (var item2 in StaffModel)
            {
                foreach (var item1 in staffDics)
                {
                    if (item1.ContainsKey(item2.staffid))
                    {
                        var filter = Builders<StaffModel>.Filter.Eq("_id", item2.staffid);
                        foreach(var i in item2.salary)
                        {
                            if(i.Month.Month == DateTime.Now.Month)
                            {
                                int index = item2.salary.IndexOf(i);
                                var update = Builders<StaffModel>.Update.Set($"salary.{index}.money", item1[item2.staffid]*100000);
                                collection.UpdateOne(filter, update);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
