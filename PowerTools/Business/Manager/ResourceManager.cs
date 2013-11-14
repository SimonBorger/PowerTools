using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace PowerTools.Business.Manager
{
    public class ResourceManager
    {
        private static ResourceManager instance;

        private FileInfo endpointController;

        public static ResourceManager GetInstance()
        {
            if (instance == null)
                instance = new ResourceManager();

            return instance;
        }

        public FileInfo GetEndpointController()
        {
            if (this.endpointController == null || !File.Exists(this.endpointController.FullName))
            {
                this.endpointController = new FileInfo(string.Format("{0}.exe", Path.GetTempFileName()));
                File.WriteAllBytes(this.endpointController.FullName, Properties.Resources.EndpointController);
            }

            return this.endpointController;
        }
    }
}
