using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace Commons.UI.WPF.Controls.TabControl
{
	[TemplatePart(Name = "PART_CloseButton", Type = typeof(ButtonBase))]
	[TemplatePart(Name = "PART_TabContextMenu", Type = typeof(ContextMenu))]
	public class ExtendedTabItem : System.Windows.Controls.TabItem
	{
		static ExtendedTabItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedTabItem), new FrameworkPropertyMetadata(typeof(ExtendedTabItem)));
		}

		/// <summary>
		/// Provides a place to display an Icon on the Header and on the DropDown Context Menu
		/// </summary>
		public object Icon
		{
			get { return (object)GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}
		public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(object), typeof(ExtendedTabItem), new UIPropertyMetadata(null));

		/// <summary>
		/// Allow the Header to be Deleted by the end user
		/// </summary>
		public bool AllowDelete
		{
			get { return (bool)GetValue(AllowDeleteProperty); }
			set { SetValue(AllowDeleteProperty, value); }
		}
		public static readonly DependencyProperty AllowDeleteProperty = DependencyProperty.Register("AllowDelete", typeof(bool), typeof(ExtendedTabItem), new UIPropertyMetadata(true));

		/// <summary>
		/// OnApplyTemplate override
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			// wire up the CloseButton's Click event if the button exists
			ButtonBase button = this.Template.FindName("PART_CloseButton", this) as ButtonBase;
			if (button != null)
			{
				button.Click += delegate
				                	{
				                		// get the parent tabcontrol 
				                		ExtendedTabControl tc = Helper.FindParentControl<ExtendedTabControl>(this);
				                		if (tc == null) return;

				                		// remove this tabitem from the parent tabcontrol
				                		tc.RemoveTabItem(this);
				                	};
			}

			ContextMenu menu = this.Template.FindName("PART_TabContextMenu", this) as ContextMenu;
			if (menu != null)
			{
				foreach (MenuItem item in menu.Items)
				{
//					<MenuItem Header="Close" Name="closeTabMenuItem"></MenuItem>
//								<MenuItem Header="Close all" Name="closeAllTabsMenuItem"></MenuItem>
//								<MenuItem Header="Close all but this" Name="closeAllTabsButThisMenuItem"></MenuItem>

					if (item.Name == "closeTabMenuItem")
						item.Click += delegate
						{
							// get the parent tabcontrol 
							ExtendedTabControl tc = Helper.FindParentControl<ExtendedTabControl>(this);
							if (tc == null) return;

							// remove this tabitem from the parent tabcontrol
							tc.RemoveTabItem(this);
						};
					else if (item.Name == "closeAllTabsMenuItem")
						item.Click += delegate
						              	{
											// get the parent tabcontrol 
											ExtendedTabControl tc = Helper.FindParentControl<ExtendedTabControl>(this);
											if (tc == null) return;

						              		tc.RemoveAllTabs();
						              	};
					else if (item.Name == "closeAllTabsButThisMenuItem")
						item.Click += delegate
						              	{
											// get the parent tabcontrol 
											ExtendedTabControl tc = Helper.FindParentControl<ExtendedTabControl>(this);
											if (tc == null) return;

						              		tc.RemoveAllTabsBut(this);
						              	};
				}
			}

		}

		/// <summary>
		///     Used by the TabPanel for sizing
		/// </summary>
		internal Dimension Dimension { get; set; }

		/// <summary>
		/// OnMouseEnter, Create and Display a Tooltip
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
		{
			base.OnMouseEnter(e);

			this.ToolTip = Helper.CloneElement(Header);
			e.Handled = true;
		}

		/// <summary>
		/// OnMouseLeave, remove the tooltip
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
		{
			base.OnMouseLeave(e);

			this.ToolTip = null;
			e.Handled = true;
		}
	}
}
