/*********************************************************************
 * Description   : This class maps the attributes elements in the 
 *                 configuration file to this class. Represents the
 *                 <add /> element.
**********************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HStart.Configuration
{
    public class StartProcInfoElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key { get { return this["key"] as string; } }

        [ConfigurationProperty("UseShellExecute", DefaultValue = "false", IsRequired = true)]
        public bool UseShellExecute { get { return (bool)this["UseShellExecute"]; } set { this["UseShellExecute"] = value; } }

        [ConfigurationProperty("RedirectStandardOutput", DefaultValue = "true", IsRequired = true)]
        public bool RedirectStandardOutput { get { return (bool)this["RedirectStandardOutput"]; } set { this["RedirectStandardOutput"] = value; } }

        [ConfigurationProperty("RedirectStandardError", DefaultValue = "true", IsRequired = true)]
        public bool RedirectStandardError { get { return (bool)this["RedirectStandardError"]; } set { this["RedirectStandardError"] = value; } }
    }
}
