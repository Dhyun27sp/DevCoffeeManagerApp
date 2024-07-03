using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;

namespace DevCoffeeManagerApp.Commands.CommandSell
{
    public class NavigationCommand : CommandBase
    {
        private NavigationStore _navigationStore;
        private string page;
        public NavigationCommand(NavigationStore navigationStore, string page)
        {
            this._navigationStore = navigationStore;
            this.page = page;

        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            switch (page)
            {
                case "tablePage":
                    _navigationStore.CurrentViewModel = new TableViewModel(_navigationStore);
                    return;
                case "orderPage":
                    _navigationStore.CurrentViewModel = new OrderViewModel(_navigationStore);
                    return;
                case "optionPage":
                    _navigationStore.CurrentViewModel = new OptionViewModel(_navigationStore);
                    return;
                case "paymentPage":
                    _navigationStore.CurrentViewModel = new PaymentViewModel();
                    return;
            }
        }
    }
}
