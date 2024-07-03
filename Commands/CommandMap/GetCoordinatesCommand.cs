using DevCoffeeManagerApp.Shipping;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandMap
{
    public class GetCoordinatesCommand : CommandBase
    {
        MapViewModel mapViewModel;
        public GetCoordinatesCommand(MapViewModel mapViewModel)
        {
            this.mapViewModel = mapViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            string[] urls = mapViewModel.Url.Split('/');
            string[] paramters;
            string address ="";
            if (urls[urls.Length - 1].Contains("data"))
            {
                paramters = urls[urls.Length - 2].Split(',');
                address = urls[urls.Length - 3].Replace('+',' ');
            }
            else
            {
                paramters = urls[urls.Length - 1].Split(',');
            }
            string lat = paramters[0].Replace("@","");
            string lng = paramters[1];
            Stop stops = new Stop
            {
                coordinates = new Coordinates { latitude = lat, longitude = lng},
                address = address
            };
            SessionStatic.CusStop = stops;
            SessionStatic.stops = SessionStatic.stops;
            MessageBox.Show(address+" "+lat+" "+lng);            
        }
    }
}
