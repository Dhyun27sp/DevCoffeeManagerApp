using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.CommandAdminStaff
{
    internal class CommandClickStaffAd : CommandBase
    {
        private AdminStaffViewModel viewModel;
        private string action;
        StaffDAO staffDAO = new StaffDAO();
        public CommandClickStaffAd(AdminStaffViewModel viewModel, string action)
        {
            this.action = action;
            this.viewModel = viewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            switch (action)
            {
                case "choose":
                    chooseStaff(parameter);
                    return;
                case "add":
                    addStaff(parameter);
                    return;
                case "deletestaff":
                    deleteStaff(parameter);
                    return;
                case "deletef":
                    deletef(parameter);
                    return;
                case "update":
                    updatestaff(parameter);
                    return;
            }
        }
        private void chooseStaff(object parameter)
        {
            if (parameter is ListView liststaff)
            {
                if (liststaff.SelectedItem != null)
                {
                    viewModel.StatusUpdate = true;
                    Tuple<StaffModel, int, string, int> selectedItem = (Tuple<StaffModel, int, string, int>)liststaff.SelectedItem;
                    viewModel.NameStaff = selectedItem.Item1.staffname;
                    viewModel.PhoneStaff = selectedItem.Item1.phone_staff;
                    viewModel.PasswordStaff = selectedItem.Item1.account.Password;
                    viewModel.DetailSalary = new ObservableCollection<SalaryModel>(selectedItem.Item1.salary);
                }
            }
        }
        private void addStaff(object parameter)
        {
            if(checkinput() == true)
            {
                AccountModel account = new AccountModel();
                List<SalaryModel> salaries = new List<SalaryModel>();
                account.Password = viewModel.PasswordStaff.Value;
                account.Role = "staff";
                StaffModel staff = new StaffModel(viewModel.NameStaff, viewModel.PhoneStaff, account, salaries);
                if (staffDAO.GetStaff(viewModel.PhoneStaff) == null)
                {
                    staffDAO.createStaff(staff);
                    MessageBox.Show("Thêm Nhân viên thành công");
                    viewModel.Staffs.Add(staff);
                    staffDAO.Createsalary();
                    viewModel.Staffs = new ObservableCollection<StaffModel>(staffDAO.ReadAll());
                }
                else
                {
                    MessageBox.Show("Nhân viên này đã tồn tại");
                }
            }
        }
        private void deleteStaff(object parameter)
        {
            if (parameter is StaffModel Staff)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    staffDAO.DeleteStaffByPhoneNumber(Staff.phone_staff);
                    MessageBox.Show("Xóa thành công");
                    viewModel.Staffs = new ObservableCollection<StaffModel>(staffDAO.ReadAll());
                }
            }
        }
        private void deletef(object parameter)
        {
            viewModel.StatusUpdate = false;
            viewModel.PhoneStaff = null;
            viewModel.NameStaff = null;
            viewModel.PasswordStaff = null;
        }
        private void updatestaff(object parameter)
        {
            AccountModel account = new AccountModel();
            List<SalaryModel> salaries = new List<SalaryModel>();
            account.Password = viewModel.PasswordStaff.Value;
            account.Role = "staff";
            StaffModel staff = new StaffModel(viewModel.NameStaff, viewModel.PhoneStaff, account, salaries);
            if (checkinput() == true)
            {

                staffDAO.updateNamePass(staff);
                MessageBox.Show("cập nhật thành công");
                viewModel.Staffs = new ObservableCollection<StaffModel>(staffDAO.ReadAll());
            }
        }
        private bool checkinput()
        {
            if (viewModel.PhoneStaff == null)
            {
                MessageBox.Show("Xin vui lòng nhập số Điện thoại");
                return false;
            }
            else if (viewModel.PasswordStaff == null)
            {
                MessageBox.Show("Xin vui lòng nhập mật khẩu");
                return false;
            }
            else if (viewModel.NameStaff == null)
            {
                MessageBox.Show("Xin vui lòng nhập tên nhân viên");
                return false;
            }
            if (viewModel.PhoneStaff != null)
            {
                if (!Regex.Match(viewModel.PhoneStaff, @"^(0[3|5|7|8|9][0-9]{8})$").Success)
                {
                    MessageBox.Show("Đây không phải là số điện thoại!");
                    return false;
                }
            }
            return true;
        }
    }
}
