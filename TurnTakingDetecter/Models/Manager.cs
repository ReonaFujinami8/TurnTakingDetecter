using TurnTakingDetecter.Entities;
using TurnTakingDetecter.Modules;
using System.ComponentModel;
using System.Text.Json;

namespace TurnTakingDetecter.Models
{
    public class Manager
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private AudioManager? _audioManager;

        public async Task StartAudio (string lang) {
            _audioManager = new AudioManager();

            _audioManager.OnResult += r =>
            {
                SetAudioResult(r.Content);
            };

            await _audioManager.Initialize();
            await _audioManager.Start(lang);
        }

        public async Task StopAudio () {
            if(_audioManager != null)
            {
                await _audioManager.Stop();
            }
        }

        private void SetAudioResult (string result) {
            var asrResult = JsonSerializer.Deserialize<ASRResult>(result);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(asrResult)));
        }
    }
}
