using System;
using System.Collections.Generic;
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

namespace Commons.UI.WPF
{
	/// <summary>
	/// Interaction logic for QuickTip.xaml
	/// </summary>
	public partial class QuickTip
	{
		public static DependencyProperty TitleProperty;
		public static DependencyProperty BodyProperty;

		static QuickTip()
		{
			TitleProperty = DependencyProperty.Register("Title", typeof(string),
										typeof(QuickTip), new PropertyMetadata(null, new PropertyChangedCallback(OnTitleChanged)));
			BodyProperty = DependencyProperty.Register("Body", typeof(string),
																typeof(QuickTip), new PropertyMetadata(null, OnBodyChanged));
		}

		public QuickTip()
		{
			this.InitializeComponent();
		}

		private static void OnBodyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			QuickTip tip = (QuickTip) d;

			tip.body.Text = e.NewValue as string;
		}

		private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			QuickTip tip = (QuickTip)d;

			tip.title.Text = e.NewValue as string;
		}

		public string Title
		{
			get { return GetValue(TitleProperty) as string; }
			set { SetValue(TitleProperty,value); }
		}

		public string Body
		{
			get { return GetValue(BodyProperty) as string; }
			set { SetValue(BodyProperty,value); }
		}
	}
}
