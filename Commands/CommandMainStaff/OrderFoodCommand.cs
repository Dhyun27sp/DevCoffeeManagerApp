using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.CommandMainStaff
{
    public class OrderFoodCommand: CommandBase
    {
        private MainStaffViewModel MainStaffViewModel;
        private SellViewModel SellViewModel = new SellViewModel();
        public OrderFoodCommand(MainStaffViewModel MainStaffViewModel) { 
            this.MainStaffViewModel = MainStaffViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            MainStaffViewModel.CurrentViewModel = SellViewModel;
        }
    }
}
