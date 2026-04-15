using Microsoft.Web.WebView2.Wpf;
using System.IO;

namespace TurnTakingDetecter.Modules
{
    public class AudioManager
    {
        private WebView2? _webView;

        public event Action<string?>? OnResult;

        public async Task Initialize () {
            _webView = new WebView2 ();
            await _webView.EnsureCoreWebView2Async();

            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Modules", "audio_manager.html"));

            _webView.Source = new Uri(path);

            _webView.CoreWebView2.WebMessageReceived += OnMessage;
        }

        public async Task Start (string lang) {
            if (_webView != null)
            {
                await _webView.ExecuteScriptAsync($"startAudioSystem({lang})");
            }
        }

        public async Task Stop () {
            if (_webView != null)
            {
                await _webView.ExecuteScriptAsync("stopAudioSystem()");
            }
        }

        private void OnMessage (object? sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e) {
            var json = e.WebMessageAsJson;

            if(json != null)
            {
                OnResult?.Invoke(json);
            }
        }
    }
}
