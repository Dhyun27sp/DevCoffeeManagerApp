using DevCoffeeManagerApp.Commands.CommandSell;
using DevCoffeeManagerApp.Store;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    public class ShopViewModel : BaseViewModel
    {
        public ICommand CommandTable { get; set; }
        public ICommand CommandOrder { get; set; }
        public ICommand CommandOptionOrder { get; set; }
        public ICommand CommandPayment { get; set; }
        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        public ShopViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChaged += OnCurrentViewModelChanged;

            CommandTable = new NavigationCommand(navigationStore,"tablePage"); 
            CommandOrder = new NavigationCommand(navigationStore, "orderPage");
            CommandOptionOrder = new NavigationCommand(navigationStore, "optionPage");
            CommandPayment = new NavigationCommand(navigationStore, "paymentPage");

            EventAggregator.Instance.MessagePublished += OnMessagePublished;
        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnMessagePublished(object sender, MessageEventArgs e)
        {
            if (e.Message == "default")
                _navigationStore.CurrentViewModel = new TableViewModel(_navigationStore);
            if (e.Message == "option")
                _navigationStore.CurrentViewModel = new OptionViewModel(_navigationStore);

        }
    }
        
}
