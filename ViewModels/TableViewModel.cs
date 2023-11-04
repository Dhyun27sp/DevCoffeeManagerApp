using DevCoffeeManagerApp.Commands.CommandLogin;
using DevCoffeeManagerApp.Commands.CommandTable;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Views.UserControlStaff;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;

namespace DevCoffeeManagerApp.ViewModels
{
    public class TableViewModel : BaseViewModel
    {
        TableDAO tableDAO = new TableDAO();
        public ICommand SelectionCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public object SelectedItem { get; set; }
        public ObservableCollection<TableModel> Items { get; set; }

        public TableViewModel()
        {
            SelectionCommand = new CommandBookTable(this);
            SubmitCommand= new CommandSubmitTable(this);
            if (Items != null)
                Items = null;
            Items = tableDAO.ReadAll();
            ObservableCollection<TableModel> selectedItems = new ObservableCollection<TableModel>(SessionStatic.GetTables);
            foreach (var item2 in selectedItems)
            {
                // Tìm phần tử trong Items có cùng thuộc tính xác định (ví dụ: ID)
                var existingItem = Items.FirstOrDefault(item => item.No_ == item2.No_);

                // Nếu tìm thấy phần tử trùng lặp, thay thế nó bằng phần tử từ Items2
                if (existingItem != null)
                {
                    int index = Items.IndexOf(existingItem);
                    var intttt = Items[index];
                    Items[index].Status = item2.Status;
                }
            }
        }
    }
}
