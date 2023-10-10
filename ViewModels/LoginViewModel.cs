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

        public ObservableCollection<string> Items { get; set; }
        public ICommand SubmitCommand { get; }
        public LoginViewModel()
        {
            // Khởi tạo và điền dữ liệu vào danh sách Items ở đây
            Items = new ObservableCollection<string>();
            Items.Add("Mục 1");
            Items.Add("Mục 2");
            Items.Add("Mục 3");
            // ... 
            SubmitCommand = new CommandSubmit(this, Items);
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

        private bool _isWorked = false;

        public bool IsWorked
        {
            get { return _isWorked; }
            set
            {
                if (_isWorked != value)
                {
                    _isWorked = value;
                    OnPropertyChanged(nameof(IsWorked));
                }
            }
        }

    }
}
