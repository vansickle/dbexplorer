using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Commons.UI.WPF.Controls.Markup
{
	/// <summary>
	/// Markup extension, that returns preconfigured tooltip with quick information (<see cref="Title"/> and <see cref="Body"/>)
	/// <example>
	///		<StackPanel ToolTip="{Markup:QuickTip Title=Attention!, BodyPath='This is a very dangerous operation!'}">
	/// </example>
	/// </summary>
	[MarkupExtensionReturnType(typeof(ToolTip))]
	public class QuickTipExtension:MarkupExtension
	{
		public string Title { get; set; }

		/// <summary>
		/// For title via Binding
		/// <example>
		///		<StackPanel ToolTip="{Markup:QuickTip TitlePath=Name, TitleFormat='Name: {0}'}">
		/// </example>
		/// </summary>
		public string TitlePath { get; set; }

		/// <summary>
		/// Binding format when use with <see cref="TitlePath"/>
		/// </summary>
		public string TitleFormat { get; set; }
		public string Body { get; set; }

		/// <summary>
		/// Like a <see cref="TitlePath"/>
		/// </summary>
		public string BodyPath { get; set; }

		/// <summary>
		/// Like a <see cref="TitleFormat"/>
		/// </summary>
		public string BodyFormat { get; set; }

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			var tip = new QuickTip();

			if(!String.IsNullOrEmpty(Title))
			{
				tip.Title = Title;
			}
			else if(!String.IsNullOrEmpty(TitlePath))
			{
				var binding = new Binding(TitlePath);
				if (!String.IsNullOrEmpty(TitleFormat))
					binding.StringFormat = TitleFormat;
				tip.SetBinding(QuickTip.TitleProperty, binding);
			}
			
			if(!String.IsNullOrEmpty(Body))
			{
				tip.Body = Body;
			}
			else if(!String.IsNullOrEmpty(BodyPath))
			{
				var binding = new Binding(BodyPath);
				if (!String.IsNullOrEmpty(BodyFormat))
					binding.StringFormat = BodyFormat;
				tip.SetBinding(QuickTip.BodyProperty, binding);
			}

			var toolTip = new ToolTip();
			toolTip.Background = Brushes.Transparent;
			toolTip.BorderThickness = new Thickness(0);
			toolTip.Content = tip;

			return toolTip;
		}
	}
}
