/*********************************************************************
 * Description   : This class maps the attributes elements in the 
 *                 configuration file to this class. Represents the
 *                 <add /> element.
**********************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HStart.Configuration
{
    public class StartProcInfoElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true, IsKey = true)]
        public string Key { get { return this["key"] as string; } set { this["key"] = value; } }

        [ConfigurationProperty("CreateNoWindow", DefaultValue = "true", IsRequired = false)]
        public bool CreateNoWindow { get { return (bool)this["CreateNoWindow"]; } set { this["CreateNoWindow"] = value; } }

        [ConfigurationProperty("UseShellExecute", DefaultValue = "false", IsRequired = false)]
        public bool UseShellExecute { get { return (bool)this["UseShellExecute"]; } set { this["UseShellExecute"] = value; } }

        [ConfigurationProperty("RedirectStandardOutput", DefaultValue = "true", IsRequired = false)]
        public bool RedirectStandardOutput { get { return (bool)this["RedirectStandardOutput"]; } set { this["RedirectStandardOutput"] = value; } }

        [ConfigurationProperty("RedirectStandardError", DefaultValue = "true", IsRequired = false)]
        public bool RedirectStandardError { get { return (bool)this["RedirectStandardError"]; } set { this["RedirectStandardError"] = value; } }

        [ConfigurationProperty("FileName", DefaultValue = "", IsRequired = true)]
        public string FileName { get { return (string)this["FileName"]; } set { this["FileName"] = value; } }

        [ConfigurationProperty("Arguments", DefaultValue = "", IsRequired = true)]
        public string Arguments { get { return (string)this["Arguments"]; } set { this["Arguments"] = value; } }

        [ConfigurationProperty("ProcessWindowStyle", DefaultValue = "Hidden", IsRequired = true)]
        public ProcessWindowStyle WindowStyle { get { return (ProcessWindowStyle)this["ProcessWindowStyle"]; } set { this["ProcessWindowStyle"] = value; } }

        public override bool IsReadOnly()
        {
            return false;
        }
    }
}
