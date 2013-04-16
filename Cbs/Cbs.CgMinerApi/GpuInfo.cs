using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cbs.CgMinerApi
{
    public class GpuInfo
    {
        public GpuInfo()
        {
        }

        public static GpuInfo FromDictionary(Dictionary<string, string> dictionary)
        {
            GpuInfo gpuInfo = new GpuInfo();

            return gpuInfo;
        }
    }
}
