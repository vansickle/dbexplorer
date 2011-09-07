using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Commons.UI.WPF.Common;

namespace Commons.UI.WPF.Controls
{
	/// <summary>
	/// list view with filter box
	/// </summary>
	public class FilteredListView : ListView
	{
		private WatermarkTextBox filterBox;

		static FilteredListView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FilteredListView), new FrameworkPropertyMetadata(typeof(FilteredListView)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			filterBox = GetTemplateChild("filterBox") as WatermarkTextBox;
			if (filterBox != null) filterBox.TextChanged += filterBox_TextChanged;
		}

		public Func<string,object,bool> Filter{ get; set; }

		private void filterBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			ICollectionView view = CollectionViewSource.GetDefaultView(ItemsSource);
			if(view==null) return;

			if(!filterBox.IsWatermarked)
				view.Filter = Filter.Apply(filterBox.Text);
			else
			{
				view.Filter = null;
			}
		}
	}
}
