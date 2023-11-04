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
            Items = tableDAO.ReadAll();
            if (SessionStatic.GetTables)
                Items = new ObservableCollection<TableModel>(SessionStatic.GetTables);
        }
    }
}
