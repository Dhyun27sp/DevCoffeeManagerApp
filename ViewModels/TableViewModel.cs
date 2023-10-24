using DevCoffeeManagerApp.Commands.CommandLogin;
using DevCoffeeManagerApp.Commands.newe;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.Views.UserControlStaff;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DevCoffeeManagerApp.ViewModels
{
    public class TableViewModel
    {
        TableDAO tableDAO = new TableDAO();
        public ICommand BookTable { get; set; }
        public ObservableCollection<TableModel> Items { get; set; }
        public TableViewModel()
        {
            Items = tableDAO.ReadAll();

            BookTable = new CommandBookTable(this);
        }
    }
}
