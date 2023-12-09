﻿using DevCoffeeManagerApp.Commands.AdminCommand.NavigateSidebarCommand;
using DevCoffeeManagerApp.Commands.CommandStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        private object _currentViewModel;
        public object CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel));
                }
            }
        }
        public ICommand Exit { get; }

        public ICommand DashboardComand { get; }
        public ICommand ProductCommand { get; }
        public ICommand SupplyCommand { get; }
        public ICommand CustomerCommand { get; }


        public AdminViewModel() {
            DashboardComand = new NavigateComand(this,"dashboard");
            ProductCommand = new NavigateComand(this, "product");
            SupplyCommand = new NavigateComand(this, "supply");
            CustomerCommand = new NavigateComand(this, "customer");
            Exit = new ExitCommand();
        }
    }
}
