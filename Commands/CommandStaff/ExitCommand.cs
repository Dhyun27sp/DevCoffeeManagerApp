﻿using DevCoffeeManagerApp.StaticClass;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandStaff
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
                if (parameter is Window window)
                {
                    window.Hide();
                    if(SessionStatic.Dishs != null)
                    {
                        SessionStatic.Dishs = null;
                    }
                }
                LoginPage login = new LoginPage();
                login.Show();
            }
        }
    }
}
