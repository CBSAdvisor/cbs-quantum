using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFG.Configuration
{
    public class FfgConfigSection : ConfigurationSection
    {
        public static string sConfigurationSectionConst = "FfgConfig";

        public void Save()
        {
            FfgConfigSection ffgSection = null;

            try
            {

                // Get the current configuration file.
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None);

                ffgSection = GetConfigSection(config);
                foreach (ConfigurationProperty prop in ffgSection.Properties)
                {
                    string name = prop.Name;
                    ffgSection.SetPropertyValue(ffgSection.Properties[name], this[name], false);
                }

                // Save the application configuration file.
                ffgSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(sConfigurationSectionConst);
            }
            catch (ConfigurationErrorsException)
            {
            }
        }

        public static FfgConfigSection CreateSection()
        {
            FfgConfigSection spiSection = null;

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

        public static FfgConfigSection CreateSection(System.Configuration.Configuration config)
        {
            FfgConfigSection spiSection = null;

            try
            {
                // Add the custom section to the application 
                // configuration file. 
                if (config.Sections[FfgConfigSection.sConfigurationSectionConst] == null)
                {
                    // Create a custom configuration section.
                    spiSection = new FfgConfigSection();
                    config.Sections.Add(FfgConfigSection.sConfigurationSectionConst, spiSection);
                }


                spiSection = config.Sections[FfgConfigSection.sConfigurationSectionConst] as FfgConfigSection;

                // Save the application configuration file.
                spiSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (ConfigurationErrorsException)
            {
            }

            return spiSection;
        }

        public static FfgConfigSection GetConfigSection()
        {
            FfgConfigSection spiSection = null;

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

        public static FfgConfigSection GetConfigSection(System.Configuration.Configuration config)
        {
            FfgConfigSection spiSection = null;

            try
            {
                // Read and display the custom section.
                spiSection = config.Sections[FfgConfigSection.sConfigurationSectionConst] as FfgConfigSection;

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

        public FfgConfigSection Copy()
        {
            FfgConfigSection copy = new FfgConfigSection();
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

        [ConfigurationProperty("UseAutoNumbers", DefaultValue = true, IsRequired = false)]
        public bool UseAutoNumbers
        {
            get { return (bool)this["UseAutoNumbers"]; }
            set { this["UseAutoNumbers"] = value; }
        }

        [ConfigurationProperty("Balance", DefaultValue = 20.0, IsRequired = false)]
        public double Balance
        {
            get { return (double)this["Balance"]; }
            set { this["Balance"] = value; }
        }

        [ConfigurationProperty("UnitCost", DefaultValue = 0.01, IsRequired = false)]
        public double UnitCost
        {
            get { return (double)this["UnitCost"]; }
            set { this["UnitCost"] = value; }
        }

        [ConfigurationProperty("NineGroups")]
        [ConfigurationCollection(typeof(NumbersGroupElementCollection), 
            CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap, AddItemName = "NineGroup")]
        public NumbersGroupElementCollection NineGroups
        {
            get { return this["NineGroups"] as NumbersGroupElementCollection; }
            set { this["NineGroups"] = value; }
        }
    }
}
