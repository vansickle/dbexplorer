using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Commons.UI.WPF.Controls
{
	public class WatermarkTextBox:TextBox
	{
		private bool isWatermarkEnabled = true;
		public static DependencyProperty WatermarkTextProperty;

		static WatermarkTextBox()
		{
			WatermarkTextProperty = DependencyProperty.Register("WatermarkText", typeof(string),
			                                                    typeof(WatermarkTextBox), new PropertyMetadata("Watermark"));		
		}

		public string WatermarkText
		{
			get { return (string) GetValue(WatermarkTextProperty); }
			set { SetValue(WatermarkTextProperty,value); }
		}

		[DefaultValue(true)]
		public bool IsWatermarkEnabled
		{
			get { return isWatermarkEnabled; }
			set { isWatermarkEnabled = value; }
		}

		public bool IsWatermarked
		{
			get { return ReferenceEquals(Text,WatermarkText); }
		}

		public WatermarkTextBox()
		{
			Loaded += WatermarkTextBox_Loaded;
		}

		void WatermarkTextBox_Loaded(object sender, RoutedEventArgs e)
		{
			if (IsWatermarkEnabled)
			{
				SetWatermarkStyle();
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if(e.Key==Key.Escape)
			{
				SetWatermarkStyle();
				UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

				// Change keyboard focus.
				if (elementWithFocus != null)
				{
					elementWithFocus.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

				}
			}
		}

		private void SetWatermarkStyle()
		{
			Background = new SolidColorBrush(Colors.LightGray);
			Foreground = new SolidColorBrush(Colors.Gray);
			Text = WatermarkText;
		}

		protected override void OnLostFocus(RoutedEventArgs e)
		{
			if(string.IsNullOrEmpty(Text))
			{
				SetWatermarkStyle();
			}
			base.OnLostFocus(e);
		}

		protected override void OnGotFocus(RoutedEventArgs e)
		{
			if(IsWatermarked)
			{
				Text = string.Empty;
				SetDefaultStyle();
			}
			base.OnGotFocus(e);
		}

		private void SetDefaultStyle()
		{
			Background = new SolidColorBrush(Colors.White);
			Foreground = new SolidColorBrush(Colors.Black);
		}
	}
}
