using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.AdminCommand.CustomerCommands
{
    public class CustomerComand : CommandBase
    {
        private AdminCustomerViewModel viewModel;
        private string action;
        CustomerDAO customerdao = new CustomerDAO();

        ReceiptDAO receiptDAO = new ReceiptDAO();
        public CustomerComand(AdminCustomerViewModel viewModel,string action) { 
            this.viewModel = viewModel;
            this.action = action;
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
                    choosecustomer(parameter);
                    return;
                case "add":
                    addcustomer(parameter);
                    return;
                case "deletef":
                    DeletefieldCustomer(parameter);
                    return;
                case "update":
                    Updatecustomer(parameter);
                    return;
                case "deletec":
                    DeleteCustomer(parameter);
                    return;
            }
        }

        private void addcustomer(object parameter)
        {
            if(viewModel.StatusAdd == false)
            {
                CustomerModel customer = new CustomerModel();
                if(checkinput() == true)
                {
                    ObservableCollection<CustomerModel> listcustomer = new ObservableCollection<CustomerModel>();
                    listcustomer = customerdao.GetAllCustomers();
                    int havecust = 0;
                    foreach(CustomerModel cus in listcustomer)
                    {
                        if(cus.phone_number == viewModel.PhoneCustom)
                        {
                            havecust++;
                            break;
                        }
                    }
                    if (havecust == 0)
                    {
                        customer.phone_number = viewModel.PhoneCustom;
                        customer.point = 0;
                        customer.name = viewModel.NameCustom;
                        if (viewModel.Dobcustom.ToString() == "01-01-0001" || viewModel.Dobcustom == null)
                        {
                            customer.dob = "";
                        }
                        else
                        {
                            customer.dob = viewModel.Dobcustom.Value.ToString("dd/MM/yyyy");
                        }
                        customerdao.CreateCustomer(customer);
                        viewModel.Dobcustom = null;
                        MessageBox.Show("thêm khách hàng thành công");
                        viewModel.Custommers = customerdao.GetAllCustomers();
                    }
                    else
                    {
                        MessageBox.Show("Thêm khách hàng thất bại: Khách hàng tồn tại");
                    }
                }
            }
            else
            {
                MessageBox.Show("khách hàng tồn tại");
            }
        }
        private void DeletefieldCustomer(object parameter)
        {
            viewModel.NameCustom = null;
            viewModel.Point = 0;
            viewModel.PhoneCustom = null;
            viewModel.Dobcustom = null;
            viewModel.StatusAdd = false;
        }
        private void Updatecustomer(object parameter)
        {
            CustomerModel customer = new CustomerModel();
            customer.phone_number = viewModel.PhoneCustom;
            customer.point = viewModel.Point;
            customer.name = viewModel.NameCustom;
            if (viewModel.Dobcustom.ToString() == "01-01-0001" || viewModel.Dobcustom == null)
            {
                customer.dob = "";
            }
            else
            {
                customer.dob = viewModel.Dobcustom.Value.ToString("dd/MM/yyyy");
            }
            customerdao.UpdateCustomer(customer);
            MessageBox.Show("cập nhật thành công");
            viewModel.Custommers = customerdao.GetAllCustomers();
        }
        private void choosecustomer(object parameter)
        {
            if (parameter is ListView listcustomer)
            {
                if (listcustomer.SelectedItem != null)
                {
                    viewModel.StatusAdd = true;
                    Tuple<CustomerModel, int> selectedItem = (Tuple<CustomerModel, int>)listcustomer.SelectedItem;
                    viewModel.NameCustom = selectedItem.Item1.name;
                    viewModel.Point = selectedItem.Item1.point;
                    viewModel.PhoneCustom = selectedItem.Item1.phone_number;
                    try
                    {
                        DateTime parsedDate = DateTime.ParseExact(selectedItem.Item1.dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        viewModel.Dobcustom = parsedDate;
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void DeleteCustomer(object parameter)
        {
            if (parameter is CustomerModel customer)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    customerdao.DeleteCustomerByPhoneNumber(customer.phone_number);
                    MessageBox.Show("Xóa thành công");
                    viewModel.Custommers = customerdao.GetAllCustomers();
                }
            }
        }
        private bool checkinput()
        {
            if (string.IsNullOrWhiteSpace(viewModel.PhoneCustom))
            {
                MessageBox.Show("thiếu số điện thoại");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(viewModel.NameCustom))
            {
                MessageBox.Show("Thiếu tên khách hàng");
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(viewModel.PhoneCustom))
            {
                if (!Regex.Match(viewModel.PhoneCustom, @"^(0[3|5|7|8|9][0-9]{8})$").Success)
                {
                    MessageBox.Show("Đây không phải là số điện thoại!");
                    return false;
                }
            }
            return true;
        }
    }
   
}
