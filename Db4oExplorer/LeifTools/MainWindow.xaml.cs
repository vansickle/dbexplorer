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
using Db4oExplorer.Domain;
using Db4oExplorer.Explorer;
using Commons.UI.LayoutDataStore;
using Commons.UI.WPF;
using Commons.UI.WPF.Controls.TabControl;
using Commons.UI.WPF.EventWiring;
using Commons.UI.WPF.Common;
using Commons.UI.WPF.LayoutDataStore;

namespace Db4oExplorer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : AbstractMainWindow,IMainWindow,IContainsLayoutStores
	{
		public MainWindow()
		{
			InitializeComponent();
			PreviewKeyDown += new KeyEventHandler(MainWindow_PreviewKeyDown);
			explorer.AddNewConnectionFired += (s)=>AddNewConnectionFired.Fire(s);
		}

		void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key==Key.F&&e.KeyboardDevice.Modifiers==ModifierKeys.Control)
			{
				explorer.FocusSearch();
			}
		}

		public event Action<string> AddNewConnectionFired;

		private void addNewConnectionMenuItem_Click(object sender, RoutedEventArgs e)
		{
			AddNewConnectionFired.Fire(null);
		}

		public override void AddTabPane(Control control, object caption, object icon, Action closeAction)
		{
			ExtendedTabItem item = new ExtendedTabItem();
			item.Icon = icon;
			item.Header = caption;

			control.Height = Double.NaN;
			control.Width = Double.NaN;

			item.Content = control;
			
			tabPane.Items.Add(item);

			if (closeAction != null)
				tabPane.TabItemClosed += (sender, args) =>
				                         	{
				                         		if (item == args.TabItem)
				                         			closeAction.Invoke();
				                         	};
			item.Focus();
		}

		public event Action ShowAboutFired;

		private void aboutMenuItem_Click(object sender, RoutedEventArgs e)
		{
			ShowAboutFired.Fire();
		}

		public ExplorerViewModel DataSource
		{
			get { return explorer.DataSource; }
			set { explorer.DataSource = value; }
		}

		public IList<ILayoutDataStore> GetLayoutStores()
		{
			return new LayoutStoreList(Name)
				.Add(new WindowLayoutStore(this))
				.Add(new DockPanelLayoutStore(this.mainDockPanel));
		}

		public event Action QueryToolFired;

		private void queryToolMenuItem_Click(object sender, RoutedEventArgs e)
		{
			QueryToolFired.Fire();
		}

		public event Action ExportDataToSqlFired;

		private void exportDataToSqlMenuItem_Click(object sender, RoutedEventArgs e)
		{
			ExportDataToSqlFired.Fire();
		}

		public event Action ExportDataToCsvFired;

		private void exportDataToCsvMenuItem_Click(object sender, RoutedEventArgs e)
		{
			ExportDataToCsvFired.Fire();
		}
	}
}
