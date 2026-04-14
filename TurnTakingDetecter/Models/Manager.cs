using TurnTakingDetecter.Entities;
using TurnTakingDetecter.Modules;
using System.ComponentModel;
using System.Text.Json;

namespace TurnTakingDetecter.Models
{
    public class Manager
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ASRManager? _asrManager;
        private VoiceManager? _voiceManager;

        public async Task ExecuteASR (string lang) {
            _asrManager = new ASRManager();

            _asrManager.OnResult += r =>
            {
                SetASRResult(r.Content);
            };

            await _asrManager.Initialize();
            await _asrManager.Start(lang);
        }

        public async Task StopASR () {
            if(_asrManager != null)
            {
                await _asrManager.Stop();
            }
        }

        private void SetASRResult (string result) {
            var asrResult = JsonSerializer.Deserialize<ASRResult>(result);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(asrResult)));
        }
    }
}
