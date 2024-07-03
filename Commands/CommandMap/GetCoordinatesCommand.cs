using DevCoffeeManagerApp.Shipping;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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

                address = urls[urls.Length - 3];
            }
            else
            {
                paramters = urls[urls.Length - 1].Split(',');
            }
            string decode_address = CheckAndDecodeUrlEncoding(address);
            address = decode_address.Replace("+"," ");
            Console.WriteLine(address);
            string lat = paramters[0].Replace("@","");
            string lng = paramters[1];
            Stop stops = new Stop
            {
                coordinates = new Coordinates { latitude = lat, longitude = lng},
                address = address
            };
            SessionStatic.CusStop = stops;
            MessageBox.Show(address+" "+lat+" "+lng);            
        }

        public static string CheckAndDecodeUrlEncoding(string inputString)
        {
            if (Uri.IsWellFormedUriString(inputString, UriKind.Absolute))
            {
                // Đây là URL hợp lệ, không cần giải mã
                return inputString;
            }
            else
            {
                // Kiểm tra xem chuỗi có được mã hóa URL hay không
                try
                {
                    return HttpUtility.UrlDecode(inputString);
                }
                catch (ArgumentException)
                {
                    // Lỗi giải mã, chuỗi có thể không được mã hóa URL
                    return inputString;
                }
            }
        }
    }
}
