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
using Commons.UI.WPF.EventWiring;

namespace Commons.UI.WPF.DataGrid
{
	/// <summary>
	/// Interaction logic for BinaryViewer.xaml
	/// </summary>
	public partial class BinaryViewer : UserControl
	{
		public BinaryViewer()
		{
			InitializeComponent();
		}

		public byte[] DataSource
		{
			get { throw new NotImplementedException(); }
			set
			{
				SetImage(value);
				SetHtml(value);
				SetText(value);
				SetRtf(value);
			}
		}

		public string Path
		{
			get { return filePathTextBox.Text; }
			set { filePathTextBox.Text = value; }
		}

		private void SetRtf(byte[] bytes)
		{
			try
			{
				var range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
				range.Load(new MemoryStream(bytes),DataFormats.Rtf);
			}
			catch (Exception e)
			{
				
			}
		}

		private void SetText(byte[] value)
		{
			if (value == null) value = new byte[0];
			textBox.Text = UTF8Encoding.UTF8.GetString(value);
		}

		private void SetHtml(byte[] value)
		{
			if(value==null) value = new byte[0];
			webBrowser.NavigateToStream(new MemoryStream(value));
		}

		private void SetImage(byte[] value)
		{
			try
			{
				var source = new BitmapImage();
				
				if(value!=null)
				{
					source.BeginInit();
					source.StreamSource = new MemoryStream(value);
					source.EndInit();
				}

				image.Source = source;
			}
			catch (NotSupportedException exception)
			{
				
			}
		}

		public event Action BrowseFired;

		private void browse_Click(object sender, RoutedEventArgs e)
		{
			BrowseFired.Fire();
		}

		public event Action SaveFired;

		private void save_Click(object sender, RoutedEventArgs e)
		{
			SaveFired.Fire();
		}
	}
}
