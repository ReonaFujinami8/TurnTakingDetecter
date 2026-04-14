let recog = null;
let running = false;

function startASRRecognition(lang){
    recog = new webkitSpeechRecognition();

    recog.lang = lang; // ex."ja-JP";
    recog.interimResults = true; // 途中の音声認識結果も取得
    recog.continuous = true;

    running = true;

    recog.onresult = function(event){
        for(let i = event.resultIndex; i < event.results.length; i++){
            let transcript = event.results[i][0].transcript;
            let isFinal = event.results[i].isFinal;
            
            let result = {
                type : "asr",
                isFinal : isFinal,
                content : transcript
            };

            window.chrome.webview.postMessage(JSON.stringify(result));
        }

    }

    recog.onerror = function (e) {
        if (e.error === "no-speech") { 
            window.chrome.webview.postMessage({
                type: "asr_error",
                error: e.error
            });

            return;
        }

        if (running) {
            recog.start();
        }
    };

    recog.onend = function (e) {
        if (running) {
            recog.start();
        }
    }

    recog.start(); // 音声認識処理を開始
}

function stopASRRecognition() {
    running = false;

    if(recog){
        recog.stop();
    }
}
