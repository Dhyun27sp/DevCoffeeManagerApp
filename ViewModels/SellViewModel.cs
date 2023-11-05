using DevCoffeeManagerApp.Commands.CommandMainStaff;
using DevCoffeeManagerApp.Commands.CommandSell;
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

namespace DevCoffeeManagerApp.ViewModels
{
    public class SellViewModel : BaseViewModel
    {
        private object _currentViewModel;

        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel));
                }
            }

        }
        private string _gach;

        public string Gach
        {
            get { return _gach; }
            set
            {
                 _gach = value;
                 OnPropertyChanged(nameof(Gach));
            }

        }

        public ICommand CommandTable { get; set; }
        public ICommand CommandOrder { get; set; }
        public ICommand CommandOptionOrder { get; set; }
        public SellViewModel() 
        {

            CommandTable = new TableCommand(this);
            CommandOrder = new OrderCommand(this);
            CommandOptionOrder = new OptionOrderCommand(this);
        }
    }
}
