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
        private Brush _foreground = Brushes.Red;

        public Brush Foreground
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
        private int _isselected = 1;

        public int IsSelected
        {
            get
            {
                return _isselected;
            }
            set
            {
                _isselected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        public ObservableCollection<TableModel> Items { get; set; }
        public TableViewModel()
        {
            Items = tableDAO.ReadAll();
            BookTable = new CommandBookTable(this);
        }
    }
}
