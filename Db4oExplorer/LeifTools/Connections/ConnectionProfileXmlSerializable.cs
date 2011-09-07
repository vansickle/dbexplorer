using System;
using System.Collections.Generic;
using System.Xml;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Connections
{
	/// <summary>
	/// XmlSerializer not friendly for interfaces in generic collections - so we need custom serialization
	/// </summary>
	public class ConnectionProfileXmlSerializable : GenericXmlSerializable<IConnectionProfile>
	{
		protected override void ParseValues(IConnectionProfile element, Dictionary<string, string> values)
		{
			//TODO make via reflection
			if(element is LocalConnectionProfile)
			{
				LocalConnectionProfile localProfile = (LocalConnectionProfile) element;
				localProfile.Name = values["Name"];		
				localProfile.Path = values["Path"];
			}
			else
			{
				RemoteConnectionProfile remoteProfile = (RemoteConnectionProfile) element;
				remoteProfile.Name = values["Name"];
				remoteProfile.Hostname = values["Hostname"];
				remoteProfile.Port = Convert.ToInt32(values["Port"]);
				remoteProfile.Login = values["Login"];
				remoteProfile.Password = values["Password"];
			}
		}

		protected override void AddNodes(XmlNode node, XmlDocument xdoc, IConnectionProfile element)
		{
			if (element is LocalConnectionProfile)
			{
				LocalConnectionProfile localProfile = (LocalConnectionProfile)element;
				node.AppendChild(CreateNode(xdoc, "Name", localProfile.Name));
				node.AppendChild(CreateNode(xdoc, "Path", localProfile.Path));
			}
			else
			{
				RemoteConnectionProfile remoteProfile = (RemoteConnectionProfile)element;
				node.AppendChild(CreateNode(xdoc, "Name", remoteProfile.Name));
				node.AppendChild(CreateNode(xdoc, "Hostname", remoteProfile.Hostname));
				node.AppendChild(CreateNode(xdoc, "Port", remoteProfile.Port.ToString()));
				node.AppendChild(CreateNode(xdoc, "Login", remoteProfile.Login));
				node.AppendChild(CreateNode(xdoc, "Password", remoteProfile.Password));
			}
		}
	}
}
