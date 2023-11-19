using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Commands.CommandOption
{
    public class UsePointCommand : CommandBase
    {
        public event EventHandler CanExecuteChanged;
        OptionOrderViewModel OptionOrderViewModel;
        public UsePointCommand(OptionOrderViewModel optionOrderViewModel)
        {
            OptionOrderViewModel = optionOrderViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (parameter is string UsePoint)
            {
                OptionOrderViewModel.UsePointText = Int32.Parse(UsePoint);

            }    
        }
    }
}
