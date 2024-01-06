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
using System.Text.RegularExpressions;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace DevCoffeeManagerApp.Commands.CommandLogin
{
    public class LoginCommand : CommandBase
    {
        StaffDAO staffdao = new StaffDAO();
        ScheduleDAO scheduledao = new ScheduleDAO();
        private LoginViewModel Viewmodellogin;
        public LoginCommand(LoginViewModel viewmodellogin)
        {
            Viewmodellogin = viewmodellogin;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public bool checkInput()
        {
            if (Viewmodellogin.Phonenumber == null)
            {
                MessageBox.Show("Xin vui lòng nhập tài khoản");
                return false;
            }
            else if (Viewmodellogin.Password == null)
            {
                MessageBox.Show("Xin vui lòng nhập mật khẩu");
                return false;
            }
            if (Viewmodellogin.Phonenumber != null)
            {
                if (!Regex.Match(Viewmodellogin.Phonenumber, @"^(0[3|5|7|8|9][0-9]{8})$").Success)
                {
                    MessageBox.Show("Đây không phải là số điện thoại!");
                    return false;
                }
            }
            return true;
        }
        public override void Execute(object parameter)
        {
            if (checkInput() == true)
            {
                Entrance(parameter);
            }
        }

        private string GetShift()
        {
            var dt = DateTime.Now;
            string shift = "";
            string daymonth = dt.ToString("dd/MM/yyyy"); // 02/09S DD/mm 2/9S
            if (dt.Hour >= 7 && dt.Hour < 12) //>=7 < 12
            {
                shift = daymonth + "S";
            }
            else if (dt.Hour >= 12 && dt.Hour < 17)
            {
                shift = daymonth + "C";
            }
            else if (dt.Hour >= 17 && dt.Hour <= 23)// chỉnh lại 10 h
            {
                shift = daymonth + "T";
            }
            return shift;
        }

        private void Entrance(object parameter)
        {
            string role = "";
            string pass = "";
            string month_present = DateTime.Now.Month.ToString();
            var staff = staffdao.GetStaff(Viewmodellogin.Phonenumber);

            if (staff != null)
            {
                role = staff.account.Role.ToString();
                pass = staff.account.Password.ToString();
                switch (role)
                {
                    case "admin":
                        if (pass == Viewmodellogin.Password)
                        {
                            if (parameter is Window window)
                            {
                                AdminPage mainWindowAdmin = new AdminPage();
                                mainWindowAdmin.Show();
                                window.Close();
                            }
                        }
                        break;
                    case "staff":
                        if (pass == Viewmodellogin.Password)
                        {
                            if (Worked_Staff() == 1)
                            {
                                Session(parameter);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Sai Mật Khẩu");
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("không tìm thấy số điện thoại này");
            }
        }
        private int Worked_Staff()
        {
            string month_present = DateTime.Now.Month.ToString();
            string shift = GetShift();// lấy ca hiện tại
            ScheduleModel schedule = scheduledao.GetSchedule(shift); //nạp dự liệu cho listStaffIdInShift
            if (schedule is null)
            {
                MessageBox.Show("Không có lịch làm hôm nay");
                return 0; // lịch làm rỗng
            }


            ObjectId _idstaff = staffdao.GetStaff(Viewmodellogin.Phonenumber).staffid; // lấy Id nhân viên theo số điện thoại và tháng lương hiện tại

            if (Viewmodellogin.ItemShift != null)
            {

                foreach (var staff in schedule.evaluate)
                {
                    if (_idstaff == staff.staff_id)
                    {
                        if (staff.worked == false)
                        {
                            // đổi trạng thái worked 
                            scheduledao.Worked_for_Staff(shift, _idstaff);                                                     
                        }
                        MessageBox.Show("Bạn đã chấm công hôm nay");
                        return 1;
                    }
                }

                EvaluateModel evaluate = new EvaluateModel(_idstaff, true, 0);
                scheduledao.AddEvaluate(shift, evaluate); // thêm staff_id vào evalute của schedule                        
                MessageBox.Show("Bạn đã chấm công hôm nay");
            }
            return 1;
        }

        private void Session(object parameter)
        {
            if (Viewmodellogin.ItemShift == "Order")
            {
                SessionStatic.SetTask = Viewmodellogin.ItemShift;
                SessionStatic.SetPhoneNumber = Viewmodellogin.Phonenumber;
                SessionStatic.SetStaffName = staffdao.GetStaff(Viewmodellogin.Phonenumber).staffname;
                SessionStatic.SetPassWord = int.Parse(Viewmodellogin.Password);
                MainWindowStaff mainWindowstaff = new MainWindowStaff();
                mainWindowstaff.Show();
                if (parameter is Window window)
                {
                    window.Close();
                }
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

    }
}
