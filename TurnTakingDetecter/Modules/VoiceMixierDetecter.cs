using NAudio.Wave;
namespace TurnTakingDetecter.Modules
{
    public class VoiceMixierDetecter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<WaveInCapabilities> GetAudioDevice () {
            var list = new List<WaveInCapabilities> ();

            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                list.Add(WaveIn.GetCapabilities(i));
            }

            return list;
        }
    }
}
