using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFG.Configuration
{
    public class NumbersGroupElement : FfgConfigElementWithKey<string>
    {
        [ConfigurationProperty("parts")]
        [ConfigurationCollection(typeof(NumbersGroupPartElement), AddItemName = "part")]
        public NumbersGroupPartElementCollection Parts { get { return this["parts"] as NumbersGroupPartElementCollection; } set { this["parts"] = value; } }
    }
}
