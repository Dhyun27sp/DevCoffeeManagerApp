using System;
using System.Windows.Input;
using DevCoffeeManagerApp.Commands.CommandStaff;
using DevCoffeeManagerApp.StaticClass;

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

        private string _staffName;
        public string StaffName
        {
            get
            {
                return _staffName;
            }
            set
            {

                _staffName = value;
                OnPropertyChanged(nameof(StaffName));

            }
        }

        private string _customerName;
        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                _customerName = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }


        private string _customerPhoneNumber;
        public string CustomerPhoneNumber
        {
            get
            {
                return _customerPhoneNumber;
            }
            set
            {

                _customerPhoneNumber = value;
                OnPropertyChanged(nameof(CustomerPhoneNumber));

            }
        }

        private DateTime? _customerBirthday = null;
        public DateTime? CustomerBirthday
        {
            get
            {
                return _customerBirthday;
            }
            set
            {
                _customerBirthday = value;
                OnPropertyChanged(nameof(CustomerBirthday));

            }
        }

        public ICommand Exit { get; }
        public ICommand FoodOrder { get; }
        public ICommand BookShip { get; }
        public ICommand RegisterCommand { get; }
        public StaffViewModel()
        {
            RegisterCommand = new RegisterCommand(this);
            FoodOrder = new SellCommand(this);
            BookShip = new ShipCommand(this);
            Exit = new ExitCommand();
            if(SessionStatic.GetStaffName != null)
            {
                StaffName = SessionStatic.GetStaffName;
            }
        }
    }
}
