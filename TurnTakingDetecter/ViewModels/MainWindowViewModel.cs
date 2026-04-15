using OxyPlot;
using OxyPlot.Series;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TurnTakingDetecter.Components;
using TurnTakingDetecter.Models;

namespace TurnTakingDetecter.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly LineSeries _vadSeries = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand StartAudioManagerCommand { get; }
        public ICommand StopAudioManagerCommand { get; }
        public PlotModel VADPlot { get; } = new();

        private Manager _manager = new();
        public MainWindowViewModel () {
            StartAudioManagerCommand = new RelayCommand(StartAudioManager);
            StopAudioManagerCommand = new RelayCommand(StopAudioManager);
            _manager.PropertyChanged += ReferPropertyChanged; 
        }

        private void StartAudioManager () {
            MessageBox.Show("Start");
        }

        private void StopAudioManager () {

        }

        private void InitializePlot () {

        }

        private void ReferPropertyChanged (sender e, ) {

        }

        public string ASRIsFinal
        {
            get => _asrIsFinal;
            set
            {
                if(_asrIsFinal != value)
                {
                    _asrIsFinal = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ASRIsFinal)));
                }
            }
        }
        private string _asrIsFinal = string.Empty;

        public string ASRRecognitionContent
        {
            get => _asrRecognitionContent;
            set
            {
                if (_asrRecognitionContent != value)
                {
                    _asrRecognitionContent = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ASRRecognitionContent)));
                }
            }
        }
        private string _asrRecognitionContent = string.Empty;
    }
}
