/*********************************************************************
 * Description   : This class is the collection of settings loaded
 *                 from the WebConfig.Conf or App.Conf.
**********************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HStart.Configuration
{
    public class StartProcInfoElementCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public StartProcInfoElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as StartProcInfoElement;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public void Add(StartProcInfoElement element)
        {
            BaseAdd(element);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new StartProcInfoElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((StartProcInfoElement)element).Key;
        }

        public void Remove(StartProcInfoElement element)
        {
            BaseRemove(element.Key);
        }
        public void Remove(string name)
        {
            BaseRemove(name);
        }
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }
    }
}
