using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace FFG.Configuration
{
    public class FfgConfigElementWithKey<K> : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true, IsKey = true)]
        public K Key { get { return (K)this["key"]; } set { this["key"] = value; } }
    }
}
