using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Views;
using DevCoffeeManagerApp.Store;

namespace DevCoffeeManagerApp.Commands.CommandTable
{
    public class CommandSubmitTable : CommandBase
    {
        private NavigationStore _navigationStore;
        public CommandSubmitTable(TableViewModel viewmodeltable, NavigationStore navigationStore)
        {
            this._navigationStore = navigationStore;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            if (parameter is ListView listView)
            {
                MessageBox.Show("Đặt bàn thành công");
                // Lấy danh sách các mục đã chọn từ SelectedItems
                List<TableModel> Items = new List<TableModel>(listView.SelectedItems.Cast<TableModel>());
                int countSelected = listView.SelectedItems.Count;
                //// Bây giờ, `selectedItems` chứa danh sách các hàng đã chọn
                //// Bạn có thể duyệt qua danh sách này để làm bất kỳ thao tác nào bạn cần
                SessionStatic.SetTables = Items;
                _navigationStore.CurrentViewModel = new OrderFoodViewModel(_navigationStore);
            }
        }
    }
}
