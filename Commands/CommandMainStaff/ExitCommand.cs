using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandMainStaff
{
    public class ExitCommand : CommandBase
    {
        public override bool CanExecute(object parameter) 
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            // Hiển thị hộp thoại xác nhận
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng?",
                                                      "Xác nhận thoát",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            // Kiểm tra kết quả của hộp thoại xác nhận
            if (result == MessageBoxResult.Yes) 
            {
                // Thoát ứng dụng
                Application.Current.Shutdown();
            }
        }
    }
}
