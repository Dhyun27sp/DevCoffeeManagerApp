using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevCoffeeManagerApp.Commands.CommandStaff;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.DAOs;
using System.Windows;

namespace DevCoffeeManagerApp.ViewModels
{
    public class StaffViewModel : BaseViewModel
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

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }


        private string _phone;
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {

                _phone = value;
                OnPropertyChanged(nameof(Phone));

            }
        }

        private DateTime? _birthday = null;
        public DateTime? Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(Birthday));

            }
        }

        public ICommand Exit { get; }
        public ICommand FoodOrder { get; }
        public ICommand RegisterCommand { get; }
        public StaffViewModel()
        {
            RegisterCommand = new RegisterCommand(this);
            FoodOrder = new SellCommand(this);
            Exit = new ExitCommand();
        }
    }
}
