using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DevCoffeeManagerApp.Commands.AdminCommand.CustomerCommands
{
    public class FilterCustomerCommand : CommandBase
    {
        private AdminCustomerViewModel viewModel;
        private string action;
        private ReceiptDAO receipt = new ReceiptDAO();
        CustomerDAO customerdao = new CustomerDAO();
        ReceiptDAO receiptdao = new ReceiptDAO();
        public FilterCustomerCommand(AdminCustomerViewModel viewModel,string action) {
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
                case "cbb":
                    combbcustomner();
                    return;
                case "search":
                    Search();
                    return;
            }
        }

        private void combbcustomner()
        {
            Filter();
        }

        private void Search()
        {
            Filter();
        }

        private void Filter()
        {
            ObservableCollection<CustomerModel> CustommersFilter = new ObservableCollection<CustomerModel>();
            if (viewModel.itemcbb == "All")
            {
                if (!string.IsNullOrWhiteSpace(viewModel.Customersearch))
                {
                    foreach(CustomerModel cus in viewModel.CustommersReadonly)
                    {
                        if (cus.phone_number.Contains(viewModel.Customersearch) || cus.name.Contains(viewModel.Customersearch))
                        {
                            CustommersFilter.Add(cus);
                        }
                    }
                    viewModel.Custommers = CustommersFilter;
                }
                else
                {
                    viewModel.Custommers = viewModel.CustommersReadonly;
                }
            }
            else if (viewModel.itemcbb == "Customer in Not action in year")
            {
                
                if (!string.IsNullOrWhiteSpace(viewModel.Customersearch))
                {
                    foreach (CustomerModel cus in CustomerNotActionInOneYear())
                    {
                        if (cus.phone_number.Contains(viewModel.Customersearch) || cus.name.Contains(viewModel.Customersearch))
                        {
                            CustommersFilter.Add(cus);
                        }
                    }
                    viewModel.Custommers = CustommersFilter;
                }
                else
                {
                    viewModel.Custommers = CustomerNotActionInOneYear();
                }
            }
            else
            {
                string number = viewModel.itemcbb.Substring(0,viewModel.itemcbb.IndexOf(" "));
                ObservableCollection<CustomerModel> Custommersnumber = new ObservableCollection<CustomerModel>();
                int numbercus = 0;
                if (int.Parse(number) == 10)
                {
                    if(viewModel.CustommersReadonly.Count >= 10)
                    {
                        numbercus = 1;
                        for (int i = 0; i < 10; i++)
                        {
                            Custommersnumber.Add(viewModel.CustommersReadonly[i]);
                        }
                    }
                    
                }
                else
                {
                    if (viewModel.CustommersReadonly.Count >= 100)
                    {
                        numbercus = 1;
                        for (int i = 0; i < 100; i++)
                        {
                            Custommersnumber.Add(viewModel.CustommersReadonly[i]);
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(viewModel.Customersearch))
                {
                    if(numbercus == 1)
                    foreach (CustomerModel cus in Custommersnumber)
                    {
                        if (cus.phone_number.Contains(viewModel.Customersearch) || cus.name.Contains(viewModel.Customersearch))
                        {
                            CustommersFilter.Add(cus);
                        }
                    }
                    else
                    {
                        foreach (CustomerModel cus in viewModel.CustommersReadonly)
                        {
                            if (cus.phone_number.Contains(viewModel.Customersearch) || cus.name.Contains(viewModel.Customersearch))
                            {
                                CustommersFilter.Add(cus);
                            }
                        }
                    }
                    viewModel.Custommers = CustommersFilter;
                }
                else
                {
                    if(numbercus != 0)
                    {
                        viewModel.Custommers = Custommersnumber;
                    }
                    else
                    {
                        viewModel.Custommers = viewModel.CustommersReadonly;
                    }
                }
            }
        }
        private List<ReceiptModel> ListReceiptActionInYear()
        {
            List<ReceiptModel> listReceiptInOneYear = receiptdao.FindReceiptInYear();
            List<ReceiptModel> filteredList = listReceiptInOneYear.Where(receipt => receipt.customer != null).ToList();
            List<ReceiptModel> uniqueItems = filteredList
            .GroupBy(item => item.customer.phone_number)
            .Select(group => group.First())
            .ToList();
            return uniqueItems;
        }
        private ObservableCollection<CustomerModel> CustomerNotActionInOneYear()
        {
            ObservableCollection<CustomerModel> customerModelsAction = new ObservableCollection<CustomerModel>();
            ObservableCollection<CustomerModel> customerModels = new ObservableCollection<CustomerModel>();
            customerModels = customerdao.GetAllCustomers();
            foreach (ReceiptModel receipt in ListReceiptActionInYear())
            {
                customerModelsAction.Add(receipt.customer);
            }
            var result = customerModels.Where(cm => !customerModelsAction.Any(cma => cma.phone_number == cm.phone_number)).ToList();
            ObservableCollection<CustomerModel> resultCollection = new ObservableCollection<CustomerModel>(result);
            return resultCollection;
        }
    }
}
