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
        AccountDAO accountdao = new AccountDAO();
        StaffDAO staffdao = new StaffDAO();
        EvaluateDAO evaluatedao = new EvaluateDAO();
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
            string role = accountdao.getAccount(Viewmodellogin.Phonenumber).role.ToString();
            string pass = accountdao.getAccount(Viewmodellogin.Phonenumber).password.ToString();
            if (role == "admin")
            {
                if (pass == Viewmodellogin.Password)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }
            }
            else if(role == "staff")
            {
                string shift = GetShift();
                if (pass == Viewmodellogin.Password)
                {
                    MessageBox.Show("đây là trang staff");
                    EvaluateModel evaluate = new EvaluateModel(Viewmodellogin.Phonenumber, shift, "1", "");
                    evaluatedao.createEvaluate(evaluate);
                    staffdao.delete_shift_in_schedule(Viewmodellogin.Phonenumber, shift);
                }
                
            }
        }

        private string GetShift()
        {
            var dt = DateTime.Now;
            string shift = "";
            string daymonth = string.Format("{0}/{1}", dt.Day, dt.Month);
            if (dt.Hour >= 7 && dt.Hour <= 12)
            {
                shift = daymonth + "S";
            }
            else if (dt.Hour >= 12 && dt.Hour <= 17)
            {
                shift = daymonth + "T";
            }
            else if (dt.Hour >= 17 && dt.Hour <= 23 && dt.Minute < 59)
            {
                shift = daymonth + "C";
            }
            return shift;
        }
    }
}
