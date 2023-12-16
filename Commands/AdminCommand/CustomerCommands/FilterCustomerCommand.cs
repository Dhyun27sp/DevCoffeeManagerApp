using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
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
        CustomerDAO customerdao = new CustomerDAO();
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

                }
                else
                {

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
    }
}
