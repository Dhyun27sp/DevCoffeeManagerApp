using DevCoffeeManagerApp.ViewModels;
using System.Windows;

namespace DevCoffeeManagerApp.Commands.CommandPayment
{
    public class MoneyReceivedCommand : CommandBase
    {
        private PaymentViewModel PaymentOrderViewModel;
        public MoneyReceivedCommand(PaymentViewModel PaymentOrderViewModel) {
            this.PaymentOrderViewModel = PaymentOrderViewModel;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            int change;
            string inputMonney = PaymentOrderViewModel.InputMoney.Replace(" ", "");
            if (CheckValidPoint(inputMonney) && inputMonney != "")
            {
                change = int.Parse(inputMonney) - PaymentOrderViewModel.TotalAmount;
                if (change >= 0)
                {
                    PaymentOrderViewModel.Change = change;
                }    
                else { PaymentOrderViewModel.Change = 0; }
            }
        }
        private bool CheckValidPoint(string input_monney)
        {
            if (int.TryParse(input_monney, out _))
            {
                return true;
            }
            else
            {
                if (input_monney == "")
                    return true;
                // Chuỗi không phải là số
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ");
                return false;
            }
        }

    }
}
