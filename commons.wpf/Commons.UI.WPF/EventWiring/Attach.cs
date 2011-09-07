using System;
using System.Windows;
using System.Windows.Controls;

namespace Commons.UI.WPF.EventWiring
{
	//NOTE attempt to make wiring through attached properties, it's NOT DONE!
	public class Attach
	{
		public static readonly DependencyProperty WireAction = DependencyProperty.RegisterAttached(
			"Wire",
			typeof (Action),
			typeof (Attach),
			new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None)
			);

		public static void SetWire(UIElement element, Action value)
		{
			element.SetValue(WireAction, value);
		}

		public static Action GetWire(UIElement element)
		{
			return (Action) element.GetValue(WireAction);
		}
	}
}
