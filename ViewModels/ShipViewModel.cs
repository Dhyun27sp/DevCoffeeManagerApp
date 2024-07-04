using DevCoffeeManagerApp.Commands.CommandShip;
using DevCoffeeManagerApp.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    public class ShipViewModel: BaseViewModel
    {
        public ICommand CommandShip { get; set; }
        public ICommand CommandCheck { get; set; }
        public ICommand CommandUpdate { get; set; }

        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        public ShipViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChaged += OnCurrentViewModelChanged;

            CommandShip = new NavigationCommand(navigationStore, "bookPage");
            CommandCheck = new NavigationCommand(navigationStore, "checkPage");
            CommandUpdate = new NavigationCommand(navigationStore, "updatePage");

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
