using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Store;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xamarin.Forms;

namespace DevCoffeeManagerApp.Commands.CommandOption
{
    public class SubmitOptionCommand:CommandBase
    {

        private OptionViewModel optionViewModel;
        private readonly NavigationStore _navigationStore;
        public SubmitOptionCommand(OptionViewModel optionViewModel, NavigationStore navigationStore)
        {
            this.optionViewModel = optionViewModel;
            this._navigationStore = navigationStore;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            int point = optionViewModel.Point;
            int plusPoint = optionViewModel.PlusPoint;
            string usePoint = optionViewModel.UsePoint.Replace(" ","");
            if (SessionStatic.Customer != null)
            {
                SessionStatic.Customer.pluspoint = plusPoint;
                SessionStatic.Customer.usedpoint = usePoint;
            }
            if (CheckValidPoint(usePoint, point))
            {
                MessageBox.Show("Xác nhận Order thành công");
                _navigationStore.CurrentViewModel = new PaymentViewModel();
            }
        }

        private bool CheckValidPoint(string used_point, int point)
        {
            if (int.TryParse(used_point, out _))
            {
                // Chuỗi là số
                if (int.Parse(used_point) <= point)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Điểm tích luỹ của khách hàng không đủ, vui lòng chọn hoặc nhập mức thấp hơn");
                    return false;
                }
            }
            else
            {
                // Chuỗi không phải là số
                MessageBox.Show("Điểm dùng không hợp lệ");
                return false;
            }
        }

    }
}
