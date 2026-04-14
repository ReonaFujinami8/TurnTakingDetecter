using NAudio.CoreAudioApi;
using NAudio.Wave;
namespace TurnTakingDetecter.Modules
{
    public class VoiceManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static MMDeviceCollection DetectAudioDevice () {
            var enumerator = new MMDeviceEnumerator ();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);

            return devices;
        }
    }
}
