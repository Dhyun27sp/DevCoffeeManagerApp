using DevCoffeeManagerApp.Views;
using DevCoffeeManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevCoffeeManagerApp.Store;

namespace DevCoffeeManagerApp.Commands.CommandOption
{
    public class OpenCommand : CommandBase
    {
        NavigationStore navigationStore;

        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            Map map = new Map();
            map.ShowDialog();
            return;
        }
    }
}
