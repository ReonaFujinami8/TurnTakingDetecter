using Microsoft.Web.WebView2.Wpf;
using TurnTakingDetecter.Entities;
using System.IO;
using System.Text.Json;

namespace TurnTakingDetecter.Modules
{
    public class ASRManager
    {
        private WebView2? _webView;

        public event Action<ASRResult>? OnResult;

        public async Task Initialize () {
            _webView = new WebView2 ();
            await _webView.EnsureCoreWebView2Async();

            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Modules", "ASR.html"));

            _webView.Source = new Uri(path);

            _webView.CoreWebView2.WebMessageReceived += OnMessage;
        }

        public async Task Start (string lang) {
            if (_webView != null)
            {
                await _webView.ExecuteScriptAsync($"StartASR({lang})");
            }
        }

        public async Task Stop () {
            if (_webView != null)
            {
                await _webView.ExecuteScriptAsync("StopASR()");
            }
        }

        private void OnMessage (object? sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e) {
            var json = e.WebMessageAsJson;

            var result = JsonSerializer.Deserialize<ASRResult>(json);

            if(result != null)
            {
                OnResult?.Invoke(result);
            }
        }
    }
}
