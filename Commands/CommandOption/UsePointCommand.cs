using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandOption
{
    public class UsePointCommand : CommandBase
    {
        public event EventHandler CanExecuteChanged;
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
                OptionOrderViewModel.UsePointText = point;

            }    
        }
    }
}
