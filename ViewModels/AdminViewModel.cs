using DevCoffeeManagerApp.Commands.AdminCommand.NavigateSidebarCommand;
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
        public ICommand DiscountCommand { get; }
        public ICommand ReceiptCommand { get; }
        public ICommand MenuCommand { get; }
        public ICommand StaffPageCommand { get; }


        public AdminViewModel() {
            DashboardComand = new NavigateComand(this,"dashboard");
            ProductCommand = new NavigateComand(this, "product");
            SupplyCommand = new NavigateComand(this, "supply");
            CustomerCommand = new NavigateComand(this, "customer");
            DiscountCommand = new NavigateComand(this, "discount");
            ReceiptCommand = new NavigateComand(this, "receipt");
            MenuCommand = new NavigateComand(this, "menu");
            StaffPageCommand = new NavigateComand(this, "staff");
            Exit = new ExitCommand();
        }
    }
}
