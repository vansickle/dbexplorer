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

namespace Db4oExplorer.Connections
{
	/// <summary>
	/// Interaction logic for LocalConnectionProfileView.xaml
	/// </summary>
	public partial class RemoteConnectionProfileView : UserControl,IConnectionProfileView
	{
		#region logging

		protected static readonly ILog LOG =
			LogManager.GetLogger(typeof (LocalConnectionProfileView));

		#endregion

		public RemoteConnectionProfileView()
		{
			InitializeComponent();
		}

		public IConnectionProfile DataSource
		{
			get { return DataContext as IConnectionProfile; }
			set { DataContext = value; }
		}
	}
}
