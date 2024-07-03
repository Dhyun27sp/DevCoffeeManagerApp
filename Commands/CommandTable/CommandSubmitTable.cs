using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using DevCoffeeManagerApp.StaticClass;
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
                ObservableCollection<TableModel> Items = new ObservableCollection<TableModel>(listView.SelectedItems.Cast<TableModel>());
                SessionStatic.SetTables = Items;
                _navigationStore.CurrentViewModel = new OrderViewModel(_navigationStore);
            }
        }
    }
}
