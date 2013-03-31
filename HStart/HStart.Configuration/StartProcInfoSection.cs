using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HStart.Configuration
{
    public class StartProcInfoSection : ConfigurationSection
    {
        public static string sConfigurationSectionConst = "StartProcInfoConfiguration";

        public void Save()
        {
            StartProcInfoSection spiSection = null;

            try
            {

                // Get the current configuration file.
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None);

                spiSection = GetConfigSection(config);
                foreach (ConfigurationProperty prop in spiSection.Properties)
                {
                    string name = prop.Name;
                    spiSection.SetPropertyValue(spiSection.Properties[name], this[name], false);
                }

                // Save the application configuration file.
                spiSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(sConfigurationSectionConst);
            }
            catch (ConfigurationErrorsException)
            {
            }
        }

        public static StartProcInfoSection CreateSection()
        {
            StartProcInfoSection spiSection = null;

            try
            {
                // Get the current configuration file.
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None);

                spiSection = CreateSection(config);
            }
            catch (ConfigurationErrorsException)
            {
            }

            return spiSection;
        }

        public static StartProcInfoSection CreateSection(System.Configuration.Configuration config)
        {
            StartProcInfoSection spiSection = null;

            try
            {
                // Add the custom section to the application 
                // configuration file. 
                if (config.Sections[StartProcInfoSection.sConfigurationSectionConst] == null)
                {
                    // Create a custom configuration section.
                    spiSection = new StartProcInfoSection();
                    config.Sections.Add(StartProcInfoSection.sConfigurationSectionConst, spiSection);
                }


                spiSection = config.Sections[StartProcInfoSection.sConfigurationSectionConst] as StartProcInfoSection;

                // Save the application configuration file.
                spiSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (ConfigurationErrorsException)
            {
            }

            return spiSection;
        }

        public static StartProcInfoSection GetConfigSection()
        {
            StartProcInfoSection spiSection = null;

            try
            {
                // Get the application configuration file.
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as System.Configuration.Configuration;

                spiSection = GetConfigSection(config);
            }
            catch (ConfigurationErrorsException)
            {
            }

            return spiSection;
        }

        public static StartProcInfoSection GetConfigSection(System.Configuration.Configuration config)
        {
            StartProcInfoSection spiSection = null;

            try
            {
                // Read and display the custom section.
                spiSection = config.Sections[StartProcInfoSection.sConfigurationSectionConst] as StartProcInfoSection;

                if (spiSection == null)
                {
                    spiSection = CreateSection(config);
                }
            }
            catch (ConfigurationErrorsException)
            {
            }

            return spiSection;
        }

        public StartProcInfoSection Copy()
        {
            StartProcInfoSection copy = new StartProcInfoSection();
            string xml = SerializeSection(this, sConfigurationSectionConst, ConfigurationSaveMode.Full);
            System.Xml.XmlReader rdr =
                new System.Xml.XmlTextReader(new System.IO.StringReader(xml));
            copy.DeserializeSection(rdr);
            return copy;
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        [ConfigurationProperty("StartProcInfos", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(StartProcInfoElementCollection), 
            CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap, 
            AddItemName = "addProcInfo", ClearItemsName = "clearProcInfo", RemoveItemName = "removeProcInfo")]
        public StartProcInfoElementCollection StartProcInfos
        {
            get { return this["StartProcInfos"] as StartProcInfoElementCollection; }
            set { this["StartProcInfos"] = value; }
        }
    }
}
