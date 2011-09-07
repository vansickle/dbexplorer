using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Commons.UI.WPF.EventWiring;

namespace Db4oExplorer.Domain
{
	public abstract class LocalConnection : IConnection,INotifyPropertyChanged
	{
		public IConnectionProfile Profile { get; set; }
		public abstract bool IsConnected { get; }

		public void UpdateProfileBy(IConnectionProfile profile)
		{
			Profile = profile;
			PropertyChanged.Fire(this,"Name");
		}

		public abstract void Disconnect();
		public abstract void TryConnect();
		public abstract void Defragment();

		public abstract void CreateNewClass();
		
		public string Name
		{
			get { return Profile.Name; }
		}

		public string Path
		{
			get { return Profile.Path; }
		}

		public abstract IList<IStoredClass> Objects { get; }
		private ConnectionStatus status = ConnectionStatus.DISCONNECTED;
		public ConnectionStatus Status
		{
			get { return status; }
			set { status = value; }
		}

		public abstract IList<NameValue> Statistics { get; }

		public override string ToString()
		{
			return string.Format("{0}", Name);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public abstract IList GetData(IStoredClass storedClass);
		public abstract IList GetData(string className);
	}
}
