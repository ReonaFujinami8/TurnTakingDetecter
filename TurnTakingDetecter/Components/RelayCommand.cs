using System.Windows.Input;

namespace TurnTakingDetecter.Components
{
    public class RelayCommand (Action execute) : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Action _execute = execute;

        public bool CanExecute (object? parameter) => true;

        public void Execute(object? parameter) {
            _execute();
        }
    }
}
