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
        private static string sConfigurationSectionConst = "StartProcInfoConfiguration";

        public static StartProcInfoSection CreateSection()
        {
            StartProcInfoSection spiSection = null;

            try
            {

                // Create a custom configuration section.
                spiSection = new StartProcInfoSection();

                // Get the current configuration file.
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None);

                // Add the custom section to the application 
                // configuration file. 
                if (config.Sections[StartProcInfoSection.sConfigurationSectionConst] == null)
                {
                    config.Sections.Add(StartProcInfoSection.sConfigurationSectionConst, spiSection);
                }


                // Save the application configuration file.
                spiSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (ConfigurationErrorsException)
            {
            }

            return spiSection;
        }

        public static StartProcInfoSection GetConfig()
        {
            StartProcInfoSection spiSection = null;

            try
            {
                // Get the application configuration file.
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as System.Configuration.Configuration;

                // Read and display the custom section.
                spiSection = ConfigurationManager.GetSection(StartProcInfoSection.sConfigurationSectionConst) as StartProcInfoSection;

                if (spiSection == null)
                {
                    spiSection = CreateSection();
                }
            }
            catch (ConfigurationErrorsException)
            {
            }

            return spiSection;
        }

        [ConfigurationProperty("StartProcInfos", IsDefaultCollection = false),
        ConfigurationCollection(typeof(StartProcInfoElementCollection), AddItemName = "addProcInfo", ClearItemsName = "clearProcInfo", RemoveItemName = "removeProcInfo")]
        public StartProcInfoElementCollection StartProcInfos
        {
            get { return this["StartProcInfos"] as StartProcInfoElementCollection; }
        }
    }
}
