using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.CommandStaff
{
    public class RegisterCommand : CommandBase
    {
        private StaffViewModel mainStaffViewModel;
        CustomerDAO customerDAO = new CustomerDAO();
        NavigationStore navigation = new NavigationStore();
        public RegisterCommand(StaffViewModel MainStaffViewModel)
        {
            this.mainStaffViewModel = MainStaffViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            if (parameter is StackPanel form)
            {
                string name = mainStaffViewModel.CustomerName;
                string phone = mainStaffViewModel.CustomerPhoneNumber;
                string date = mainStaffViewModel.CustomerBirthday.GetValueOrDefault().ToString("dd/MM/yyyy");
                if (checkInput(name, phone, date) == false)
                {
                    return;
                }
                CustomerModel newCustomer = new CustomerModel(name, phone, date, 0);
                customerDAO.CreateCustomer(newCustomer);
                MessageBox.Show("Đăng ký khách hàng thành công");
                mainStaffViewModel.CustomerName = null;
                mainStaffViewModel.CustomerPhoneNumber = null;
                mainStaffViewModel.CustomerBirthday = null;
            }
        }

        public bool checkInput(string name, string phone, string date)
        {
            DateTime result;
            if (name == "" || name == null)
            {
                MessageBox.Show("Xin vui lòng nhập tên");
                return false;
            }
            else if (phone == null || phone == "")
            {
                MessageBox.Show("Xin vui lòng nhập số điện thoại");
                return false;
            }
            else if (!Regex.Match(phone, @"^(0[3|5|7|8|9][0-9]{8})$").Success)
            {
                MessageBox.Show("Đây không phải là số điện thoại!");
                return false;
            }
            else if (date == "01/01/0001")
            {
                MessageBox.Show("Xin vui lòng nhập ngày sinh");
                return false;
            }
            else if (!DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result) || result >= DateTime.Today)
            {
                MessageBox.Show("Đây không phải là ngày sinh hợp lệ!");
                return false;
            }
            return true;
        }
    }
}
