﻿using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.ViewModels;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandOption
{
    public class SearchCustomerCommand : CommandBase
    {
        OptionViewModel OptionOrderViewModel;
        CustomerDAO CustomerDAO = new CustomerDAO();
        public SearchCustomerCommand(OptionViewModel optionOrderViewModel)
        {
            OptionOrderViewModel = optionOrderViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (OptionOrderViewModel.PhoneNumber == null || OptionOrderViewModel.PhoneNumber == "")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại khách hàng");
                return;
            }
            CustomerModel customer = new CustomerModel();
            customer = CustomerDAO.GetCustomerByPhoneNumber(OptionOrderViewModel.PhoneNumber);
            if(customer == null)
            {
                MessageBox.Show("Khách hàng chưa đăng ký");
                return;
            }
            OptionOrderViewModel.Point = customer.point;
            OptionOrderViewModel.Name = customer.name;
            SessionStatic.Customer = customer;
        }
    }
}
