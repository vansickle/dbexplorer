using System;
using System.Collections.Generic;
using System.IO;
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
using Common.Logging;
using Db4oExplorer.Domain;
using Microsoft.Win32;
using Path=System.IO.Path;
using Commons.UI.WPF.EventWiring;

namespace Db4oExplorer.Connections
{
	/// <summary>
	/// Interaction logic for LocalConnectionProfileView.xaml
	/// </summary>
	public partial class LocalConnectionProfileView : UserControl,IConnectionProfileView
	{
		#region logging

		protected static readonly ILog LOG =
			LogManager.GetLogger(typeof (LocalConnectionProfileView));

		#endregion

		public LocalConnectionProfileView()
		{
			InitializeComponent();
		}

		public IConnectionProfile DataSource
		{
			get { return DataContext as IConnectionProfile; }
			set { DataContext = value; }
		}

		public event Action BrowseFired;

		private void browse_Click(object sender, RoutedEventArgs e)
		{
			LOG.Debug(x=>x("Current directory:"+Environment.CurrentDirectory));

			BrowseFired.Fire();

			LOG.Debug(x => x("Current directory:" + Environment.CurrentDirectory));
		}
	}
}
