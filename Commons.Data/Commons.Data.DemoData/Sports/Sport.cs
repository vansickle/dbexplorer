using System;

namespace Commons.Data.DemoData.Sports
{
	public class Sport
	{
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public DateTime Olympic { get; set; }

		public Sport(string name)
		{
			Name = name;
		}

		public Sport()
		{
		}
	}
}
