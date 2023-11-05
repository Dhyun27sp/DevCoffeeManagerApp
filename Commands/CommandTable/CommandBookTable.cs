using DevCoffeeManagerApp.DAOs;
using DevCoffeeManagerApp.Models;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevCoffeeManagerApp.Commands.CommandTable
{
    public class CommandBookTable: CommandBase
    {
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
            if (parameter is ListView listView)
            {

                MessageBox.Show("lol");
                //foreach (TableModel selectedItem in selectedItems)
                //{
                //    MessageBox.Show(selectedItem.No_.ToString());
                //}
            }
        }
    }
}
