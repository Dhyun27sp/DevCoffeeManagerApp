using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;

namespace DevCoffeeManagerApp.Commands.CommandStaff
{
    public class SellCommand: CommandBase
    {
        private StaffViewModel MainStaffViewModel;
        NavigationStore navigation = new NavigationStore();
        public SellCommand(StaffViewModel MainStaffViewModel) { 
            this.MainStaffViewModel = MainStaffViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            MainStaffViewModel.CurrentViewModel = new ShopViewModel(navigation);
        }
    }
}
