using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace FFG.Configuration
{
    public class NumbersGroupPartElement : FfgConfigElementWithKey<int>
    {
        [ConfigurationProperty("value", IsRequired = true, DefaultValue="")]
        public string Value { get { return this["value"] as string; } set { this["value"] = value; } }
    }
}
