using System;
using System.Windows;
using System.Windows.Controls;
using Commons.UI.LayoutDataStore;

namespace Commons.UI.WPF.LayoutDataStore
{
    /// <summary>
    /// common class for Control LayoutDataStore classes
    /// entity is Control
    /// </summary>
    public abstract class WPFLayoutDataStore : AbstractLayoutDataStore
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="entity">control, that data you store</param>
        /// <param name="parent">used to ensure originality of name </param>
        public WPFLayoutDataStore(Control entity,Control parent) : base(entity,FormatName(entity, parent))
        {
        }

        private static string FormatName(Control entity, Control parent)
        {
            if (parent != null)
                return String.Format("{0}.{1}", parent.Name, entity.Name);
            return entity.Name;
        }


        protected WPFLayoutDataStore(Control entity, Control parent, bool generateCheckCode)
            : base(entity,FormatName(entity,parent),generateCheckCode)
        {
        }
        protected WPFLayoutDataStore(Control entity, string name, bool checkCode)
            : base(entity, name, checkCode)
        {

        }        
        
        public Control Control
        {
            get { return (Control) entity; }
        }

    	/// <summary>
    	/// loading for defined entity
    	/// </summary>
    	/// <param name="xmlLayoutData">layout data</param>
    	protected abstract void LoadEntityFromString(string xmlLayoutData);

		public override void Load(string xmlLayoutData)
        {
            try
            {
                LoadEntityFromString(xmlLayoutData);
            }
            catch (Exception e)
            {
                if (e is LayoutDataStoreException) return;

                MessageBox.Show(
                    string.Format(Properties.Resources.LoadingIsInterrupted, e),
                    Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected object GetDefaultMenuItem()
        {
            MenuItem item =
                new MenuItem
                    {
                        Header=Properties.Resources.LoadDefaultPanelLayout,
                        Icon = null
                        
                    };
            item.Click += item_Click;

            item.Name = "LoadDefaultLayoutItem";
            item.Visibility = Visibility.Visible;
            return item;
        }

        void item_Click(object sender, RoutedEventArgs e)
        {
            LoadDefaultLayout(sender,e);        
        }
    }
}
