using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Point=System.Windows.Point;

namespace Commons.UI.WPF.Controls
{
	/// <summary>
	/// DockPanelSplitter is a splitter control for DockPanels.
	/// Add the DockPanelSplitter after the element you want to resize.
	/// Set the DockPanel.Dock to define which edge the splitter should work on.
	/// based on http://www.codeproject.com/KB/WPF/DockPanelSplitter.aspx
	/// </summary>
	public class DockPanelSplitter : Border
	{

		private FrameworkElement _element;  // element to resize
		private Dock _dock;                 // DockStyle of the splitter element
		private double _width;              // current desired width of the _element, can be less than minwidth
		private double _height;             // current desired height of the _element, can be less than minheight

		public DockPanelSplitter()
		{
			//we need to set background, because otherwise user click "through" this control and it doesn't catch mouse events
			Background = Brushes.Transparent;
		}

		double AdjustWidth(double dx)
		{
			if (_dock == Dock.Right)
				dx = -dx;

			_width += dx;

			if (_width > _element.MinWidth)
				_element.Width = _width;
			else
				_element.Width = _element.MinWidth;

			return dx;
		}

		double AdjustHeight(double dy)
		{
			if (_dock == Dock.Bottom)
				dy = -dy;

			_height += dy;
			if (_height > _element.MinHeight)
				_element.Height = _height;
			else
				_element.Height = _element.MinHeight;

			return dy;
		}

		Point StartDragPoint;

		protected override void OnMouseEnter(MouseEventArgs e)
		{
			base.OnMouseEnter(e);
			if (!IsEnabled) return;
			_dock = DockPanel.GetDock(this);
			if (_dock == Dock.Left || _dock == Dock.Right)
			{
				Cursor = Cursors.SizeWE;
			}
			else
			{
				Cursor = Cursors.SizeNS;
			}
		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			if (!IsEnabled) return;

			if (!IsMouseCaptured)
			{
				StartDragPoint = e.GetPosition(Parent as IInputElement);

				Panel ParentPanel = Parent as Panel;
				int i = ParentPanel.Children.IndexOf(this);

				// The splitter cannot be the first child of the parent DockPanel
				// The splitter works on the 'older' sibling 
				if (i > 0 && ParentPanel.Children.Count > 0)
				{
					_element = ParentPanel.Children[i - 1] as FrameworkElement;
					_element = ParentPanel.Children[i - 1] as FrameworkElement;

					if (_element != null)
					{
						_width = _element.ActualWidth;
						_height = _element.ActualHeight;
						_dock = DockPanel.GetDock(this);
						CaptureMouse();
					}
				}
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (IsMouseCaptured)
			{
				Point ptCurrent = e.GetPosition(Parent as IInputElement);
				Point delta = new Point(ptCurrent.X - StartDragPoint.X, ptCurrent.Y - StartDragPoint.Y);
				bool IsVertical = (_dock == Dock.Left || _dock == Dock.Right);

				if (IsVertical)
					delta.X = AdjustWidth(delta.X);
				else
					delta.Y = AdjustHeight(delta.Y);

				bool IsBottomOrRight = (_dock == Dock.Right || _dock == Dock.Bottom);

				// When docked to the bottom or right, the position has changed after adjusting the size
				if (IsBottomOrRight)
					StartDragPoint = e.GetPosition(Parent as IInputElement);
				else
					StartDragPoint = new Point(StartDragPoint.X + delta.X, StartDragPoint.Y + delta.Y);
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			if (IsMouseCaptured)
			{
				ReleaseMouseCapture();
			}
			base.OnMouseUp(e);
		}

	}
}
