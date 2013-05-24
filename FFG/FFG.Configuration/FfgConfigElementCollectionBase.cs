using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace FFG.Configuration
{
    public class FfgConfigElementCollectionBase<K, T> : ConfigurationElementCollection 
        where T : FfgConfigElementWithKey<K>, new()
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public T this[int index]
        {
            get
            {
                return (T)base.BaseGet(index);
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

        public T this[object key]
        {
            get
            {
                return (T)base.BaseGet(key);
            }
            set
            {
                if (base.BaseGet(key) != null)
                {
                    base.BaseRemove(key);
                }
                this.BaseAdd(value);
            }
        }

        public void Add(T element)
        {
            BaseAdd(element);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((T)element).Key;
        }

        public void Remove(T element)
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

        public override bool IsReadOnly()
        {
            return false;
        }
    }
}
