using System.Windows.Controls;
using System.Windows.Input;

namespace Commons.UI.WPF.Controls
{
	public class ExtendedComboBox:ComboBox
	{
		public ExtendedComboBox()
		{
			
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if(e.Key==Key.Escape)
			{
				SelectedItem = null;
			}
		}
	}
}
