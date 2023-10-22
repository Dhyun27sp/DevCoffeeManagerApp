using DevCoffeeManagerApp.Commands.CommandLogin;
using DevCoffeeManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    public class TableViewModel
    {
        public List<TableModel> Items
        {
            get; set;
        }
        public ICommand ReservationCommand { get; }
        public ICommand CloseCommand { get; }
        public TableViewModel()
        {
            // Khởi tạo và điền dữ liệu vào danh sách Items ở đây
            ReservationCommand = new LoadTableCommand(this);
            CloseCommand = new CloseCommand();

            Items = new List<TableModel>();

        }
    }
}
