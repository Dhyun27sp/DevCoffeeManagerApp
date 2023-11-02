using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevCoffeeManagerApp.Commands.CommandMainStaff;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.DAOs;
using System.Windows;

namespace DevCoffeeManagerApp.ViewModels
{
    public class MainStaffViewModel : BaseViewModel
    {
      
        private object _currentViewModel;
        public object CurrentViewModel
        {
            get { return _currentViewModel; }
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
        public ICommand FoodOrder { get; }
        public MainStaffViewModel()
        {
            FoodOrder = new OrderFoodCommand(this);
            Exit = new ExitCommand();
        }
    }
}
