using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using PowerTools.Business.Model;
using System.Diagnostics;
using System.Threading;

namespace PowerTools.Business.Manager
{
    public class AudioManager
    {
        private static AudioManager instance;

        public static AudioManager GetInstance()
        {
            if (instance == null)
                instance = new AudioManager();

            return instance;
        }

        public IEnumerable<AudioDevice> GetOutputDevices()
        {
           using(var collection = new ManagementObjectSearcher("SELECT * FROM Win32_SoundDevice").Get())
           {
               int i = 0;
                var audioDevices = from ManagementObject @object in collection
                                   select new AudioDevice(
                                       id: (i++).ToString(),
                                       caption: @object.GetPropertyValue("Caption") as string);

                return audioDevices.ToList();
           }
        }

        public void SetCurrentAudioDevice(string deviceId)
        {
            Process.Start(
                new ProcessStartInfo()
                {
                    FileName = ResourceManager.GetInstance().GetEndpointController().FullName,
                    Arguments = deviceId,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                });
        }
    }
}
