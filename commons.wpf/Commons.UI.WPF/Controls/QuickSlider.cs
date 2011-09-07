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

namespace Commons.UI.WPF.Controls
{
	[TemplatePart(Type = typeof(Button), Name = "min")]
	[TemplatePart(Type = typeof(Button), Name = "max")]
	public class QuickSlider : Slider
	{
		private Button min;
		private Button max;

		static QuickSlider()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(QuickSlider), new FrameworkPropertyMetadata(typeof(QuickSlider)));
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			min = GetTemplateChild("min") as Button;
			if (min != null) min.Click += min_Click;
			max = GetTemplateChild("max") as Button;
			if (max != null) max.Click += max_Click;
		}

		private void max_Click(object sender, RoutedEventArgs e)
		{
			Value = Maximum;	
		}

		void min_Click(object sender, RoutedEventArgs e)
		{
			Value = Minimum;
		}
	}
}
