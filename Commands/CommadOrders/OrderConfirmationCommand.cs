using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommadOrders
{
    public class OrderConfirmationCommand : CommandBase
    {
        private OrderFoodViewModel orderFoodViewModel;
        private readonly NavigationStore _navigationStore;
        public OrderConfirmationCommand(OrderFoodViewModel orderFoodViewModel, NavigationStore navigationStore) { 
            this.orderFoodViewModel = orderFoodViewModel;
            this._navigationStore = navigationStore;
        }

        public event EventHandler CanExecuteChanged;

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            MessageBox.Show("Đặt Món thành công");
            SessionStatic.Ordereds = orderFoodViewModel.Ordereds;
            _navigationStore.CurrentViewModel = new OptionOrderViewModel();
        }
    }
}
