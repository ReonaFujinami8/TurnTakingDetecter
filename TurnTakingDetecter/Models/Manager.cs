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
            using JsonDocument document = JsonDocument.Parse(result);
            JsonElement root = document.RootElement;
            string? tmp = root.GetProperty("type").GetString();

            if (tmp == "asr")
            {
                bool isFinal;
                string content;
                tmp = root.GetProperty("isFinal").GetString();
                if(tmp == "true")
                {
                    isFinal = true;
                }
                else if(tmp == "false") 
                {
                    isFinal = false;
                }
                else
                {
                    return;
                }

                tmp = root.GetProperty("content").GetString();
                if(tmp != null)
                {
                    content = tmp;
                }
                else
                {
                    return;
                }

                var asrResult = new ASRResult(isFinal, content);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(asrResult)));
            }
            else if (tmp == "vad")
            {
                float volume;
                tmp = root.GetProperty("Volume").GetString();

                if(tmp != null)
                {
                    volume = float.Parse(tmp);
                }
                else
                {
                    return;
                }

                var vadResult = new VADResult(volume);

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(vadResult)));
            }
        }
    }
}
