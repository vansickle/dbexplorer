using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;


namespace Db4oExplorer.Connections
{
	public abstract class GenericXmlSerializable<ISerializedItem> : IXmlSerializable 
	{
		public List<ISerializedItem> Items { get; set; }
        
		public GenericXmlSerializable()
		{
			Items = new List<ISerializedItem>();
		}
        
		#region IXmlSerializable implementation
        
		public XmlSchema GetSchema()
		{
			return null;
		}
        
		public void ReadXml(XmlReader reader)
		{
			// Create a XPathDocument out of the Xml in order to navigate through
			XPathDocument xPath = new XPathDocument(reader);
			XPathNavigator navigator = xPath.CreateNavigator();
            
			// Iterate through all elements of Items
			XPathNodeIterator iterator = navigator.Select("//" + this.GetType().Name + "/Items/Element");
			while (iterator.MoveNext())
			{
				Dictionary<string, string> values = ReadNodes(iterator.Current);
                
				// Create element of the specified type
				ISerializedItem element = (ISerializedItem)Activator.CreateInstance(Type.GetType(values["Type"]));

				ParseValues(element,values);
                
				Items.Add(element);
			}
		}

		protected abstract void ParseValues(ISerializedItem element, Dictionary<string, string> values);

		public void WriteXml(XmlWriter writer)
		{
			// Create a new Xml Document
			XmlDocument xdoc = new XmlDocument();
            
			// Iterate trough all Items and append them
			XmlNode items = xdoc.AppendChild(CreateNode(xdoc, "Items", string.Empty));
			foreach (ISerializedItem item in Items)
			{
				XmlNode element = items.AppendChild(CreateNode(xdoc, "Element", string.Empty));
				element.AppendChild(CreateNode(xdoc, "Type", item.GetType().FullName));
				AddNodes(element,xdoc,item);
			}
            
			// Output XML
			xdoc.WriteTo(writer);
		}

		protected abstract void AddNodes(XmlNode node, XmlDocument xdoc, ISerializedItem element);

		protected static XmlNode CreateNode(XmlDocument doc, string name, string value)
		{
			XmlNode node = doc.CreateNode(XmlNodeType.Element, name, string.Empty);
			if (!string.IsNullOrEmpty(value))
				node.AppendChild(doc.CreateTextNode(value));
			return node;
		}
        
		private static Dictionary<string, string> ReadNodes(XPathNavigator navigator)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			if (!navigator.MoveToFirstChild())
				return result;
            
			do { result.Add(navigator.Name, navigator.Value); } while (navigator.MoveToNext());
            
			navigator.MoveToParent();
			return result;
		}
        
		#endregion
	}
}
