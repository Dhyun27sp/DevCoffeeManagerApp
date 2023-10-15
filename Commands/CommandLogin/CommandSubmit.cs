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
            if (dt.Hour >= 7 && dt.Hour <= 12)
            {
                shift = daymonth + "S";
            }
            else if (dt.Hour > 12 && dt.Hour <= 17)
            {
                shift = daymonth + "C";
            }
            else if (dt.Hour > 17 && dt.Hour <= 22)
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
                    Session();
                    Worked_Staff();
                }
            }
        }
        private void Worked_Staff()
        {
            string month_present = DateTime.Now.Month.ToString();
            int Add = 0; // cờ thêm nhân viên mới
            string shift = GetShift();// lấy ca hiện tại
            List<string> listStaffIdInShift = new List<string>();// danh sách ID nhân viên
            int staff_number = listStaffIdInShift.Count;// số lượng nhân viên có trong ca
            listStaffIdInShift = scheduledao.Get_StaffId_In_Shift(shift);//nạp dự liệu cho listStaffIdInShift
            string _idstaff = staffdao.GetStaff(Viewmodellogin.Phonenumber, month_present).staffid; // lấy Id nhân viên theo số điện thoại và tháng lương hiện tại

            if (staff_number <= 8)
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
            }
        }

        private void Session()
        {
            if (Viewmodellogin.ItemShift == "Order")
            {
                SessionStatic.SetTask = Viewmodellogin.ItemShift;
                SessionStatic.SetPhoneNumber = Viewmodellogin.Phonenumber;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else if (Viewmodellogin.ItemShift == "Waiters")
            {
                if (SessionStatic.GetTask != null)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Quay Lại trang đăng nhập");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn việc làm");
            }
        }
    }
}
