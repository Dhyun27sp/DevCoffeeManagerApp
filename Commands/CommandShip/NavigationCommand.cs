using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandShip
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
                case "bookPage":
                    if (SessionStatic.GetReceipt == null)
                    {
                        MessageBox.Show("Chưa đặt hàng");
                        return;
                    }
                    _navigationStore.CurrentViewModel = new BookViewModel(_navigationStore);
                    return;
                case "checkPage":
                    _navigationStore.CurrentViewModel = new CheckViewModel(_navigationStore);
                    return;
            }
        }
    }
}
