using System.Windows.Input;
using TurnTakingDetecter.Components;
using TurnTakingDetecter.Models;

namespace TurnTakingDetecter.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand RecvVoicePressureCommand { get; }
        public ICommand ASRProcessCommand { get; }
        public ICommand TurnTakingProcessCommand { get; }

        private Manager _manager = new();
        public MainWindowViewModel () {
            RecvVoicePressureCommand = new RelayCommand(RetrieveVoicePressure);
            ASRProcessCommand = new RelayCommand(ExecuteASRProcess);
            TurnTakingProcessCommand = new RelayCommand(ExecuteTurnTakingProcess);
        }

        private void RetrieveVoicePressure () {

        }

        private void ExecuteASRProcess () {

        }

        private void ExecuteTurnTakingProcess () {

        }
    }
}
