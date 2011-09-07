using System;
using System.IO;
using System.Windows;
using Commons.Utils;
using Commons.UI.LayoutDataStore;
using Commons.UI.WPF.LayoutDataStore.Properties;

namespace Commons.UI.WPF.LayoutDataStore
{
    public class WPFLayoutStoreWorker : AbstractLayoutStoreWorker
    {
        public WPFLayoutStoreWorker(ILayoutDataStorePathFactory factory, string name, params ILayoutDataStore[] stores)
            :
                base(factory, name, stores)
        {
        }

        /// <summary>
        /// load layout data for form stores
        /// <rus>
        /// Загрузить настройки формы и ее контролов</rus>
        /// </summary>
        public override void Load()
        {
            FileInfo fileInfo = new FileInfo(FileUtils.MakeValidFilePath(ConfigFileName));
            if (!fileInfo.Exists)
            {
                LoadDefault();
                return;
            }
            try
            {
                LayoutSettings.ReadXml(ConfigFileName, false, stores);
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    String.Format(Resources.LoadingIsInterruptedWillBeLoadedByDefault,
                                  e),
                    Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
