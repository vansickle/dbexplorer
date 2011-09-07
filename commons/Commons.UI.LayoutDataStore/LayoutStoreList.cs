using System;
using System.Collections.Generic;

namespace Commons.UI.LayoutDataStore
{
    /// <summary>
    /// just list with fluent inetrface support
    /// </summary>
    public class LayoutStoreList : List<ILayoutDataStore>
    {
        public LayoutStoreList(string parentName, params IList<ILayoutDataStore>[] storeLists)
        {
            foreach (IList<ILayoutDataStore> list in storeLists)
            {
                foreach (ILayoutDataStore store in list)
                {
                    store.Name = String.Format("{0}.{1}", parentName, store.Name);
                }
                AddRange(list);
            }
        }

        public LayoutStoreList()
        {
        }

        public LayoutStoreList Add(ILayoutDataStore store)
        {
            base.Add(store);
            return this;
        }

        public LayoutStoreList AddRange(IList<ILayoutDataStore> stores)
        {
            base.AddRange(stores);
            return this;
        }
    }
    /// <summary>
    /// Control имеет свойство Name
    /// </summary>
    public interface INamableControl
    {
        string Name { get; }
    }
}
