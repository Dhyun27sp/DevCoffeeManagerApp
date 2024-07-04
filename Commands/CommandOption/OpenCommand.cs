using DevCoffeeManagerApp.Views;
using DevCoffeeManagerApp.Store;

namespace DevCoffeeManagerApp.Commands.CommandOption
{
    public class OpenCommand : CommandBase
    {
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
