using DevCoffeeManagerApp.Commands.CommandStaff;
using DevCoffeeManagerApp.Commands.CommandSell;
using DevCoffeeManagerApp.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Xamarin.Forms;
using DevCoffeeManagerApp.Views.UserControlStaff;

namespace DevCoffeeManagerApp.ViewModels
{
    public class SellViewModel : BaseViewModel
    {
        public ICommand CommandTable { get; set; }
        public ICommand CommandOrder { get; set; }
        public ICommand CommandOptionOrder { get; set; }
        public ICommand CommandPayment { get; set; }
        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        public SellViewModel(NavigationStore navigationStore)
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
            _navigationStore.CurrentViewModel = new TableViewModel(_navigationStore);
        }
    }
        
}
