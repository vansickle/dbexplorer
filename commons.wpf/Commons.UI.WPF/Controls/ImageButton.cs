using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Commons.UI.WPF.Controls
{
	//TODO complete imagebutton (not used source) and check theme applying (both custom, aero n toolbar)
	public class ImageButton:Button
	{
		public ImageSource Image { get; set; }

		static ImageButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
		}
	}
}
