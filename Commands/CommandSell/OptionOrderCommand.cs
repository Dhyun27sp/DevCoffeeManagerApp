using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.CommandSell
{
    public class OptionOrderCommand : CommandBase
    {
        OptionOrderViewModel optionOrderViewModel = new OptionOrderViewModel();
        private SellViewModel SellViewModel;
        public OptionOrderCommand(SellViewModel SellViewModel)
        {
            this.SellViewModel = SellViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            SellViewModel.CurrentViewModel = optionOrderViewModel;
        }
    }
}
