﻿using DevCoffeeManagerApp.ViewModels;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandMap
{
    public class OpenCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            // Hiển thị hộp thoại xác nhận
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng popup?",
                                                      "Xác nhận đóng",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            // Kiểm tra kết quả của hộp thoại xác nhận
            if (result == MessageBoxResult.Yes)
            {
                if(parameter is Window window)
                {
                    window.Hide();
                }
                EventAggregator.Instance.PublishMessage("option");

            }
        }
    }
}
