using DevCoffeeManagerApp.Views;

namespace DevCoffeeManagerApp.Commands.CommandSchedule
{
    internal class OpenCommand: CommandBase
    {
        public OpenCommand() { }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            AddSchedule view = new AddSchedule();
            view.Show();
            return;
        }
    }
}
