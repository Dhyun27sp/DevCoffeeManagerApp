﻿using DevCoffeeManagerApp.ViewModels;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandOption
{
    public class UsePointCommand : CommandBase
    {
        OptionViewModel OptionOrderViewModel;
        public UsePointCommand(OptionViewModel optionOrderViewModel)
        {
            OptionOrderViewModel = optionOrderViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (parameter is string UsePoint)
            {
                if (OptionOrderViewModel.PhoneNumber == null || OptionOrderViewModel.PhoneNumber == "")
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại khách hàng");
                    return;
                }
                int point = int.Parse(UsePoint);
                if (OptionOrderViewModel.Point < point)
                {
                    MessageBox.Show("Điểm tích luỹ của khách hàng không đủ, vui lòng chọn hoặc nhập mức thấp hơn");
                    return;
                }
                OptionOrderViewModel.UsePoint = point.ToString();

            }    
        }
    }
}
