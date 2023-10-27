using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DevCoffeeManagerApp.Commands.newe
{
    public class CommandBookTable: CommandBase
    {
        public CommandBookTable() { }
        TableDAO tableDAO = new TableDAO();
        private TableViewModel Viewmodeltable;
        public CommandBookTable(TableViewModel viewmodeltable)
        {
            Viewmodeltable = viewmodeltable;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            MessageBox.Show("lol");
            Viewmodeltable.Foreground = Colors.Blue;
        }
    }
}
