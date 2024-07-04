using DevCoffeeManagerApp.Commands.CommandMap;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private string _url = "https://www.google.com/maps/@41.2950114,-91.9826847,14z";
        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged(nameof(Url));

            }
        }

        private string _customername;
        public string CustomerName
        {
            get { return _customername; }
            set
            {

                _customername = value;
                OnPropertyChanged(nameof(CustomerName));

            }
        }

        private string _customerphone;
        public string CustomerPhone
        {
            get { return _customerphone; }
            set
            {
                _customerphone = "+84" + value.Substring(1);
                OnPropertyChanged(nameof(CustomerPhone));

            }
        }

        public ICommand CloseCommand { get; }
        public ICommand GetCommand { get; }

        public MapViewModel()
        {
            CloseCommand = new OpenCommand();
            GetCommand = new GetCoordinatesCommand(this);
        }
    }
}
