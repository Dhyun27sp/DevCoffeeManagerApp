using DevCoffeeManagerApp.Commands.CommandLogin;
using DevCoffeeManagerApp.Commands.newe;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.StaticClass;
using DevCoffeeManagerApp.Views.UserControlStaff;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace DevCoffeeManagerApp.ViewModels
{
    public class TableViewModel : BaseViewModel
    {
        TableDAO tableDAO = new TableDAO();
        public ICommand SelectionCommand { get; set; }
        public ObservableCollection<TableModel> SelectedItem { get; set; }
        public ObservableCollection<TableModel> Items { get; set; }

        public TableViewModel()
        {
            if (SessionStatic.GetTables != null)
                SelectedItem = new ObservableCollection<TableModel>(SessionStatic.GetTables);
            SelectionCommand = new CommandBookTable(this);
            Items = tableDAO.ReadAll();

        }
    }
}
