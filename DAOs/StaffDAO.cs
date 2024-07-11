using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;

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
        public void DeleteStaffByPhoneNumber(string phoneNumber)
        {
            var filter = Builders<StaffModel>.Filter.Eq("phone_number", phoneNumber);
            collection.DeleteOne(filter);
        }
        public StaffModel GetStaffbyid(ObjectId id)
        {
            var filter = Builders<StaffModel>.Filter.Eq("_id", id);
            return collection.Find(filter).FirstOrDefault();
        }
        public void updateNamePass(StaffModel staff)
        {
            var filter = Builders<StaffModel>.Filter.Eq("phone_number", staff.phone_staff);
            var update_name = Builders<StaffModel>.Update.Set("name", staff.staffname);
            var update_Pass = Builders<StaffModel>.Update.Set("account.password", staff.account.Password);
            collection.UpdateOne(filter, update_name);
            collection.UpdateOne(filter, update_Pass);
        }
        public void Createsalary()//tạo ra salary trong tháng của năm đó cho nhân viên
        {
            List<StaffModel> listStaff = ReadAll();
            foreach (var item in listStaff)
            {
                SalaryModel salary = new SalaryModel();// tạo salary 
                bool allowedcreatesalary = false;// biến cho phép tạo mới nếu nhân viên chưa có salary trong tháng
                foreach (var item1 in item.salary)// để vòng lặp ngược sẽ tốt hơn 
                {
                    if(item1.Month.Month == DateTime.Now.Month && item1.Month.Year == DateTime.Now.Year)// nếu mà có một Object của salary có thắng và năm đã có thì cờ cho phép tạo salary sẽ được bật 
                    {
                        allowedcreatesalary = true; // cờ được bật
                        break;
                    }
                }
                if(allowedcreatesalary == false)//nếu nếu cờ không bật thì cho phép tạo (cảm giác hơi ngược)
                {
                    salary.Month = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 4);
                    salary.Money = 0;
                    var filter = Builders<StaffModel>.Filter.Eq("phone_number", item.phone_staff);
                    var update = Builders<StaffModel>.Update.Push("salary", salary);
                    collection.UpdateOne(filter, update);
                }
            }
        }
        public void salaryInMonth()
        {

            List<ScheduleModel> filteredSchedules = new List<ScheduleModel>();//lấy lịch nhân viên trong tháng hiện tại
            List<StaffModel> StaffModel = new List<StaffModel>();// lấy hết danh sách ra 
            filteredSchedules = scheduledao.findSchedulebymothCurrent();
            StaffModel = ReadAll();
            List<EvaluateModel> evaluateModels = new List<EvaluateModel>(); //lấy danh sách evaluvate
            List<Dictionary<ObjectId, int>> staffDics = new List<Dictionary<ObjectId, int>>(); //Dictionary bộ key-value, mục dích tìm kiếm trong nạp objectid của nhân viên là key và value là số lượng cần chấm công
            foreach (var i in StaffModel) // tạo bộ dự liệu objectid-worked count
            {
                Dictionary<ObjectId, int> staffDic = new Dictionary<ObjectId, int>();
                staffDic[i.staffid] = 0;// value là 0 
                staffDics.Add(staffDic);
            }
            foreach (var i in filteredSchedules)/// lấy từng phần tử trong danh sách tháng trong năm
            {
                foreach (var item in i.evaluate)// lấy từng phần tử evaluate trong từng buổi làm 
                {
                    foreach (var item1 in staffDics)// lấy từng phần tử trong staffDics mục đích để tăng count worked cho nhân viên với _id là key
                    {
                        if (item1.ContainsKey(item.staff_id))// nếu nhân viên đó có trong evaluate
                        {
                            if(item.worked == true)//nếu nhân viên đó có worked == true 
                            {
                                item1[item.staff_id]++;// tăng count worked nhân viên đó lên 1 đơn vị
                                break;// thấy nhân viên đó rồi thôi thoát 
                            } 
                        }
                    }
                }
            }
            foreach (var item2 in StaffModel)// lấy ra từng nhân viên để lấy ra ObjectId
            {
                foreach (var item1 in staffDics)// lấy từng phần tử của staffDics
                {
                    if (item1.ContainsKey(item2.staffid))// nếu _id nhân viên đó là key
                    {
                        var filter = Builders<StaffModel>.Filter.Eq("_id", item2.staffid);//tạo filter tim nhân viên đó theo id
                        foreach(var i in item2.salary)// lọc tìm vị trí index để update
                        {
                            if(i.Month.Month == DateTime.Now.Month && i.Month.Year == DateTime.Now.Year)// vị trí index là vị trí có month là hiện tại
                            {
                                int index = item2.salary.IndexOf(i);//lấy được index
                                var update = Builders<StaffModel>.Update.Set($"salary.{index}.money", item1[item2.staffid]*150000);//mỗi worked là 150000đ
                                collection.UpdateOne(filter, update);//cập nhật salary
                                break;
                            }
                        }
                    }
                }
            }
        }
        public int CountStaffs()
        {
            int staffCount = Convert.ToInt32(collection.CountDocuments(new BsonDocument()));
            return staffCount;
        }
    }
}
