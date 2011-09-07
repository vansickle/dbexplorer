using System;
using System.Collections;
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
using Commons.UI.WPF.EventWiring;

namespace Db4oExplorer.StoredClass
{
	/// <summary>
	/// Interaction logic for StoredClassFieldListView.xaml
	/// </summary>
	public partial class StoredClassFieldListView : UserControl
	{
		public StoredClassFieldListView()
		{
			InitializeComponent();
		}

		public IList<Field> DataSource
		{
			set
			{
				grid.ItemsSource = value;
			}
		}

		private Field GetSelected()
		{
			return grid.SelectedValue as Field;
		}

		public event Action<Field> AliasFieldFired;

		private void aliasFieldMenuItem_Click(object sender, RoutedEventArgs e)
		{
			AliasFieldFired.Fire(GetSelected());
		}

		public event Action<Field> IndexFieldFired;

		private void indexFieldMenuItem_Click(object sender, RoutedEventArgs e)
		{
			IndexFieldFired.Fire(GetSelected());
		}

		public event Action<Field> RenameFieldFired;
		
		private void renameFieldMenuItem_Click(object sender, RoutedEventArgs e)
		{
			RenameFieldFired.Fire(GetSelected());
		}

		public event Action<Field,Field> CopyValuesBetweenFieldFired;

		private void copyValuesBetweenField_Click(object sender, RoutedEventArgs e)
		{
			IList selectedItems = grid.SelectedItems;
			CopyValuesBetweenFieldFired.Fire((Field)selectedItems[0],(Field)selectedItems[1]);
		}

		public event Action<Field> DeleteFieldFired;

		private void deleteField_Click(object sender, RoutedEventArgs e)
		{
			DeleteFieldFired.Fire(GetSelected());
		}
	}
}
