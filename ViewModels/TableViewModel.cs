using DevCoffeeManagerApp.Commands.CommandLogin;
using DevCoffeeManagerApp.Commands.newe;
using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.Views.UserControlStaff;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace DevCoffeeManagerApp.ViewModels
{
    public class TableViewModel : BaseViewModel
    {
        TableDAO tableDAO = new TableDAO();
        public ICommand BookTable { get; set; }
        public Color _foreground = Colors.Red;

        public Color Foreground
        {
            get
            {
                return _foreground;
            }
            set
            {
                _foreground = value;
                OnPropertyChanged(nameof(Foreground));
            }
        }
        public ObservableCollection<TableModel> Items { get; set; }
        public TableViewModel()
        {
            BookTable = new CommandBookTable(this);
        }
    }
}
