using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Connections
{
	public class ConnectionProfileRepository : IConnectionProfileRepository
	{
		private string path;
		private XmlSerializer serializer = new XmlSerializer(typeof(ConnectionProfileXmlSerializable), new Type[] { typeof(RemoteConnectionProfile), typeof(LocalConnectionProfile) });

		public ConnectionProfileRepository()
		{
			var settingsPath = string.Format("{0}\\{1}", 
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
				Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location));

			if (!Directory.Exists(settingsPath))
				Directory.CreateDirectory(settingsPath);
			
			path = string.Format("{0}\\connections.xml", settingsPath);
		}

		public IList<IConnectionProfile> GetAll()
		{
			try
			{
				using (FileStream reader = new FileStream(path,FileMode.Open))
				{
					ConnectionProfileXmlSerializable deserialize = (ConnectionProfileXmlSerializable) serializer.Deserialize(reader);
					return deserialize.Items;
				}
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine(e);
				return new List<IConnectionProfile>();
			}
		}

		public void CreateNew(IConnectionProfile profile)
		{
			IList<IConnectionProfile> profiles = GetAll();
			profiles.Add(profile);

			Write(profiles);
		}

		private void Write(IList<IConnectionProfile> profiles)
		{
			using(FileStream fileStream = new FileStream(path,FileMode.Create))
			{
				ConnectionProfileXmlSerializable serializable = new ConnectionProfileXmlSerializable();
				serializable.Items = (List<IConnectionProfile>) profiles;
				serializer.Serialize(fileStream,serializable);
				fileStream.Close();
			}
		}

		public void Delete(IConnectionProfile profile)
		{
			IList<IConnectionProfile> profiles = GetAll();
			profiles.Remove(profile);

			Write(profiles);
		}

		public void Update(IConnectionProfile originalProfile, IConnectionProfile connectionProfile)
		{
			//NOTE if any problems - remake profile to have something like GUID
			IList<IConnectionProfile> profiles = GetAll();
			profiles.Remove(originalProfile);
			profiles.Add(connectionProfile);

			Write(profiles);
		}
	}
}
