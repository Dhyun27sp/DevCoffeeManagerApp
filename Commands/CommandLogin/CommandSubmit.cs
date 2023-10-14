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
                string shift = GetShift();
                if (pass == Viewmodellogin.Password)
                {
                    string _idstaff = staffdao.GetStaff(Viewmodellogin.Phonenumber, month_present).staffid;
                    MessageBox.Show("đây là trang staff");
                    EvaluateModel evaluateModel = new EvaluateModel(_idstaff,true,0);
                    ScheduleModel scheduleModel = new ScheduleModel(shift, Viewmodellogin.Phonenumber, evaluateModel);
                    scheduledao.createSchedule(scheduleModel);
                    staffdao.delete_shift_in_schedule_of_Staff(Viewmodellogin.Phonenumber, month_present, shift);
                }
            }
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
    }
}
