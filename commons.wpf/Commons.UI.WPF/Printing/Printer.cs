using System.Windows.Controls;
using System.Windows.Media;

namespace Commons.UI.WPF.Printing
{
	public class Printer
	{
		public void Print(Visual visual, string description)
		{
			PrintDialog dialog = new PrintDialog();

			if (dialog.ShowDialog().GetValueOrDefault())
			{
				dialog.PrintVisual(visual, description);
			}
		}
	}
}
