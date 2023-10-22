using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevCoffeeManagerApp.Commands.CommandMainStaff;

namespace DevCoffeeManagerApp.ViewModels
{
    public class MainStaffViewModel : BaseViewModel
    {
        public ICommand CommandTable { get; set; }
        public ICommand CommandOrder { get; set; }
        public ICommand CommandExit { get; }


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
        public MainStaffViewModel()
        {
            CommandTable = new TableCommand(this);
            CommandOrder = new OrderCommand(this);
            CommandExit = new ExitCommand();
        }
    }
}
