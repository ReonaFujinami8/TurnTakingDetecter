let micStream = null;

async function startAudioSystem(lang) {
    micStream = await navigator.mediaDevices.getUserMedia({
        audio: {
            echoCancellation: true,
            noiseSuppression: true,
            autoGainControl: true
        }
    });

    startASRRecognition(lang); // 音声認識処理を開始
    startVAD(micStream);            // VADを開始
}

function stopAudioSystem() {
    stopASRRecognition(); // 音声認識処理を終了
    stopVAD();

    if (micSteam) {
        micSteam.getTracks().forEach(track => track.stop());
        micSteam = null;
    }
}
