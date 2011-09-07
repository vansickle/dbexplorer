using System;
using System.ComponentModel;

namespace Db4oExplorer.Domain
{
	[Serializable]
	public class LocalConnectionProfile : IConnectionProfile,INotifyPropertyChanged
	{
		private string name;
		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				NotifyPropertyChanged("Name");
			}
		}

		private string path;
		public string Path
		{
			get { return path; }
			set
			{
				path = value;

				if (String.IsNullOrEmpty(Name))
					Name = System.IO.Path.GetFileNameWithoutExtension(path);

				NotifyPropertyChanged("Path");
			}
		}

		public IConnectionProfile Clone()
		{
			return (IConnectionProfile) MemberwiseClone();
		}

		private void NotifyPropertyChanged(string propertyName)
		{
			if(PropertyChanged!=null)
			{
				PropertyChanged.Invoke(this,new PropertyChangedEventArgs(propertyName));
			}
		}

		public override string ToString()
		{
			return string.Format("Name: {0}, Path: {1}", Name, Path);
		}

		public bool Equals(LocalConnectionProfile other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.name, name) && Equals(other.path, path);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (LocalConnectionProfile)) return false;
			return Equals((LocalConnectionProfile) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((name != null ? name.GetHashCode() : 0)*397) ^ (path != null ? path.GetHashCode() : 0);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
