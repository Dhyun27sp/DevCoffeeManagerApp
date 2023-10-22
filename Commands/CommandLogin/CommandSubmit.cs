using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using MongoDB.Bson;
using DevCoffeeManagerApp.Views;

namespace DevCoffeeManagerApp.Commands.CommandLogin
{
    public class CommandSubmit : CommandBase
    {
        StaffDAO staffdao = new StaffDAO();
        ScheduleDAO scheduledao = new ScheduleDAO();
        private LoginViewModel Viewmodellogin;
        public CommandSubmit(LoginViewModel viewmodellogin)
        {
            Viewmodellogin = viewmodellogin;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            Entrance();
        }

        private string GetShift()
        {
            var dt = DateTime.Now;
            string shift = "";
            string daymonth = dt.ToString("dd/MM"); // 02/09S DD/mm 2/9S
            if (dt.Hour >= 7 && dt.Hour < 12)
            {
                shift = daymonth + "S";
            }
            else if (dt.Hour >= 12 && dt.Hour < 17)
            {
                shift = daymonth + "C";
            }
            else if (dt.Hour >= 17 && dt.Hour < 22)
            {
                shift = daymonth + "T";
            }
            return shift;
        }

        private void Entrance()
        {
            string month_present = DateTime.Now.Month.ToString();
            string role = staffdao.GetStaff(Viewmodellogin.Phonenumber, month_present).account.Role.ToString();
            string pass = staffdao.GetStaff(Viewmodellogin.Phonenumber, month_present).account.Password.ToString();
            if (role == "admin")
            {
                if (pass == Viewmodellogin.Password)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }
            }
            else if (role == "staff")
            {
                if (pass == Viewmodellogin.Password)
                {
                    Worked_Staff();
                    Session();
                }
            }
        }
        private void Worked_Staff()
        {
            string month_present = DateTime.Now.Month.ToString();
            int Add = 0; // cờ thêm nhân viên mới
            string shift = GetShift();// lấy ca hiện tại
            int count_Worked_in_shift = Count_Staff_Had_Worked(shift);
            List<ObjectId> listStaffIdInShift = new List<ObjectId>(); ;// danh sách ID nhân viên
            listStaffIdInShift = Get_StaffIDs_in_Evaluate(shift); ;//nạp dự liệu cho listStaffIdInShift

            ObjectId _idstaff = staffdao.GetStaff(Viewmodellogin.Phonenumber, month_present).staffid; // lấy Id nhân viên theo số điện thoại và tháng lương hiện tại

            if (Viewmodellogin.ItemShift == "Order" || Viewmodellogin.ItemShift == "Waiters")
            {
                if (count_Worked_in_shift < 4)
                {
                    foreach (var staffid in listStaffIdInShift)
                    {
                        if (staffid == _idstaff)
                        {
                            scheduledao.Worked_for_Staff(shift, staffid);// đổi trạng thái worked
                            Add = -1;
                            break;
                        }
                    }
                    if (Add == 0)
                    {
                        EvaluateModel evaluate = new EvaluateModel(_idstaff, true, 0);
                        scheduledao.AddEvaluate(shift, evaluate);
                        // thêm staff_id vào evalute của schedule
                    }
                    MessageBox.Show("Bạn đã chấm công hôm nay");
                }
                else MessageBox.Show("Số lượng nhân viên làm đã tối đa");
            }
            
        }

        private void Session()
        {
            if (Viewmodellogin.ItemShift == "Order")
            {
                SessionStatic.SetTask = Viewmodellogin.ItemShift;
                SessionStatic.SetPhoneNumber = Viewmodellogin.Phonenumber;
                SessionStatic.SetPassWord = int.Parse(Viewmodellogin.Password);
                MainWindowStaff mainWindowstaff = new MainWindowStaff();
                mainWindowstaff.Show();
            }
            else if (Viewmodellogin.ItemShift == "Waiters")
            {
                if (SessionStatic.GetTask != null)
                {
                    MainWindowStaff mainWindowstaff = new MainWindowStaff();
                    mainWindowstaff.Show();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn việc làm");
            }
        }

        private List<ObjectId> Get_StaffIDs_in_Evaluate(string shift)
        {
            List<ObjectId> Staffids = new List<ObjectId>();
            List<EvaluateModel> evaluates = new List<EvaluateModel>();
            evaluates = scheduledao.GetEvalute(shift);
            foreach(var evalute in evaluates)
            {
                Staffids.Add(evalute.staff_id);
            }
            return Staffids;
        }

        private int Count_Staff_Had_Worked(string shift)
        {
            int count = 0;
            List<ObjectId> Staffids = new List<ObjectId>();
            List<EvaluateModel> evaluates = new List<EvaluateModel>();
            evaluates = scheduledao.GetEvalute(shift);
            foreach (var evalute in evaluates)
            {
                if(evalute.worked == true)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
