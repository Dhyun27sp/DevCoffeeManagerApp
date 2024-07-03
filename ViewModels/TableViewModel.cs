using DevCoffeeManagerApp.Commands.CommandTable;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using DevCoffeeManagerApp.Store;

namespace DevCoffeeManagerApp.ViewModels
{
    public class TableViewModel : BaseViewModel
    {
        TableDAO tableDAO = new TableDAO();
        public ICommand SubmitCommand { get; set; }
        public ICommand CancelallTableCommand { get; set; }
        private ObservableCollection<TableModel> _items;
        public ObservableCollection<TableModel> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public TableViewModel(NavigationStore navigationStore)
        {
            SubmitCommand = new CommandSubmitTable(this, navigationStore);
            CancelallTableCommand = new CancelallTableCommand(this);
            Items = tableDAO.ReadAll();
            if(SessionStatic.GetTables != null)
            {
                checkItemSame();
            }
        }
        private void checkItemSame()
        {
            ObservableCollection<TableModel> selectedItems = new ObservableCollection<TableModel>(SessionStatic.GetTables);
            foreach (var item2 in selectedItems)
            {
                // Tìm phần tử trong Items có cùng thuộc tính xác định (ví dụ: ID)
                var existingItem = Items.FirstOrDefault(item => item.No_ == item2.No_);
                // Nếu tìm thấy phần tử trùng lặp, thay thế nó bằng phần tử từ Items2
                if (existingItem != null)
                {
                    int index = Items.IndexOf(existingItem);
                    Items[index].Status = item2.Status;
                }
            }
        }
    }
}
