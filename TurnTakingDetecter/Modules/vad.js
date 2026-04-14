let audioContext;
let analyser;
let microphone;
let dataArray;
let vadRunning = false;

async function startVAD(stream) {
    audioContext = new AudioContext();
    microphone = audioContext.createMediaStreamSource(stream);

    analyser = audioContext.createAnalyser();
    analyser.fftSize = 512;
    microphone.connect(analyser);

    dataArray = new Uint8Array(analyser.fftSize);

    vadRunning = true;

    detectVolume();
}

function detectVolume() {
    if (!vadRunning) {
        return;
    }

    analyser.getByteTimeDomainData(dataArray);

    let sum = 0;

    for (let i = 0; i < dataArray.length; i++) {
        let x = dataArray[i] - 128;
        sum += x * x;
    }

    let volume = Math.sqrt(sum / dataArray.length);

    window.chrome.webview.postMessage(JSON.stringify({
        type: "vad",
        volume: volume
    }));

    requestAnimationFrame(detectVolume);
}

function stopVAD() {
    vadRunning = false;

    if (audioContext) {
        audioContext.close();
        audioContext = null;
    }
}