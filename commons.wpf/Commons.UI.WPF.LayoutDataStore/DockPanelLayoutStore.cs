using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using Commons.UI.LayoutDataStore;
using Commons.Utils.v35;

namespace Commons.UI.WPF.LayoutDataStore
{
	public class DockPanelLayoutStore : AbstractLayoutDataStore
	{
		private XmlSerializer serializer = new XmlSerializer(typeof(DockPanelSettings), new Type[] { typeof(DockPanelChildSettings) });

		public DockPanelLayoutStore(DockPanel panel) : base(panel,panel.Name)
		{
		}

		public override void Load(string xmlLayoutData)
		{
			DockPanel panel = (DockPanel) entity;

			var children = panel.Children;

			var reader = new StringReader(xmlLayoutData);
			DockPanelSettings settings = (DockPanelSettings) serializer.Deserialize(reader);

			List<DockPanelChildSettings> childSettingses = settings.Children;

			childSettingses.ForEach((setting,i)=>
			                        	{
			                        		FrameworkElement element = children[i] as FrameworkElement;
											if (element == null) return;

			                        		element.Width = setting.Width;
			                        		element.Height = setting.Height;
			                        	});
		}

		protected override void SaveEntity2Memory(Stream memory)
		{
			DockPanel panel = (DockPanel)entity;
			DockPanelSettings settings = new DockPanelSettings();

			foreach (UIElement element in panel.Children)
			{
				var dockChildSettings = new DockPanelChildSettings();
				
				element.IfInstanceOf<FrameworkElement>(e =>
				                                       	{
				                                       		dockChildSettings.Width = e.Width;
				                                       		dockChildSettings.Height = e.Height;
				                                       	});

				settings.AddChild(dockChildSettings);
			}

			serializer.Serialize(memory,settings);
		}
	}
}
