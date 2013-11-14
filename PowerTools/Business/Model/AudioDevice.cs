using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace PowerTools.Business.Model
{
    public class AudioDevice
    {
        public AudioDevice(string id, string caption)
        {
            this.Id = id;
            this.Caption = caption;
        }

        public string Caption { get; private set; }

        public string Id { get; private set; }
    }
}
