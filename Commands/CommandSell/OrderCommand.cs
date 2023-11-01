using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.CommandMainStaff
{
    public class OrderCommand : CommandBase
    {
        OrderFoodViewModel orderFoodViewModel = new OrderFoodViewModel();
        private SellViewModel SellViewModel;
        public OrderCommand(SellViewModel SellViewModel)
        {
            this.SellViewModel = SellViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            SellViewModel.CurrentViewModel = orderFoodViewModel;
        }
    }
}
