using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandReceipt
{
    public class CloseCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            // Hiển thị hộp thoại xác nhận
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng hóa đơn?",
                                                      "Xác nhận đóng",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            // Kiểm tra kết quả của hộp thoại xác nhận
            if (result == MessageBoxResult.Yes)
            {
                if(parameter is Window window)
                {
                    window.Close();
                }
            }
        }
    }
}
