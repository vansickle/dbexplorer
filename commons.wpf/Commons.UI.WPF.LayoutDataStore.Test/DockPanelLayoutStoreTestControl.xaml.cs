using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Commons.UI.LayoutDataStore;

namespace Commons.UI.WPF.LayoutDataStore.Test
{
	/// <summary>
	/// Interaction logic for DockPanelLayoutStoreTestControl.xaml
	/// </summary>
	public partial class DockPanelLayoutStoreTestControl : UserControl,IContainsLayoutStores
	{
		public DockPanelLayoutStoreTestControl()
		{
			InitializeComponent();
		}

		public IList<ILayoutDataStore> GetLayoutStores()
		{
			return new LayoutStoreList(this.Name).Add(new DockPanelLayoutStore(panel));
		}
	}
}
