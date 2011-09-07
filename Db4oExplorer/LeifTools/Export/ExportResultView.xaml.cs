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
using Commons.UI.WPF.EventWiring;

namespace LeifTools.Export
{
	/// <summary>
	/// Interaction logic for ExportResultView.xaml
	/// </summary>
	public partial class ExportResultView : UserControl
	{
		public ExportResultView()
		{
			InitializeComponent();
			saveButton.Click += (s, e) => SaveFired.Fire();
		}

		public event Action SaveFired;

		public string Text
		{
			get { return textBox.Text; }
			set { textBox.Text = value; }
		}
	}
}
