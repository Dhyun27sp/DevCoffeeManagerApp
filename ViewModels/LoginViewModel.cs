using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevCoffeeManagerApp.Commands.CommandLogin;
namespace DevCoffeeManagerApp.ViewModels
{
    public class LoginViewModel: BaseViewModel
    {
        public ObservableCollection<string> Items
        {
            get; set;
        }
        public ICommand SubmitCommand { get; }
        public ICommand CloseCommand { get; }
        public LoginViewModel()
        {
            // Khởi tạo và điền dữ liệu vào danh sách Items ở đây
            SubmitCommand = new CommandSubmit(this);
            CloseCommand = new CloseCommand();

            Items = new ObservableCollection<string>();
            Items.Add("Order");
            Items.Add("Waiters");
        }

        private string _phonenumber;
        public string Phonenumber
        {
            get
            {
                return _phonenumber;
            }
            set
            {
                _phonenumber = value;
                OnPropertyChanged(nameof(Phonenumber));              
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _itemShift;
        public string ItemShift
        {
            get
            {
                return _itemShift;
            }
            set
            {
                _itemShift = value;
                OnPropertyChanged(nameof(_itemShift));
            }
        }

    }
}
