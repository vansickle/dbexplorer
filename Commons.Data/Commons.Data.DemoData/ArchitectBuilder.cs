namespace Commons.Data.DemoData
{
	public class ArchitectBuilder
	{
		public ConvertableList<Architect> BuildAll()
		{
			return new ConvertableList<Architect>()
			       	{
			       		new Architect {Name = "Василий Баженов"},
			       		new Architect {Name = "Константин Тон"},
			       		new Architect {Name = "Бартоломео Растрелли"},
			       	};
		}
	}
}
