using DevCoffeeManagerApp.Commands.CommandMap;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Xamarin.Forms;

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
                if (_url != value)
                {
                    _url = value;
                    OnPropertyChanged(nameof(Url));
                }
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
