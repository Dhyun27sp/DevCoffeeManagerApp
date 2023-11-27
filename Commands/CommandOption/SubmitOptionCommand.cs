using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandOption
{
    public class SubmitOptionCommand:CommandBase
    {

        private OptionViewModel optionViewModel;
        private readonly NavigationStore _navigationStore;
        public SubmitOptionCommand(OptionViewModel optionViewModel, NavigationStore navigationStore)
        {
            this.optionViewModel = optionViewModel;
            this._navigationStore = navigationStore;
        }

        public event EventHandler CanExecuteChanged;

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            MessageBox.Show("Chuyển Qua thanh toán");
            _navigationStore.CurrentViewModel = new PaymentViewModel();
        }
    }
}
