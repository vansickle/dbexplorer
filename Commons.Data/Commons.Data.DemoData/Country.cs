using System;

namespace Commons.Data.DemoData
{
	[Serializable]
	public class Country
	{
		public string Name { get; set; }

		/// <summary>
		/// in square km
		/// </summary>
		public int Area { get; set; }

		/// <summary>
		/// Internet top-level domain
		/// </summary>
		public string InternetTLD { get; set; }

		public bool Equals(Country other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.Name, Name) && other.Area == Area && Equals(other.InternetTLD, InternetTLD);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Country)) return false;
			return Equals((Country) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = (Name != null ? Name.GetHashCode() : 0);
				result = (result*397) ^ Area;
				result = (result*397) ^ (InternetTLD != null ? InternetTLD.GetHashCode() : 0);
				return result;
			}
		}

		public override string ToString()
		{
			return string.Format("Name: {0}, Area: {1}, InternetTLD: {2}", Name, Area, InternetTLD);
		}
	}
}
