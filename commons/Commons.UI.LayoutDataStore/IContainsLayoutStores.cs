using System.Collections.Generic;

namespace Commons.UI.LayoutDataStore
{
    /// <summary>
    /// usercontrol hast built-in layoutdatastore support
    /// </summary>
    public interface IContainsLayoutStores
    {
        /// <summary>
        /// Get List Of Inner Controls
        /// </summary>
        /// <returns></returns>
        IList<ILayoutDataStore> GetLayoutStores();

    }
}
