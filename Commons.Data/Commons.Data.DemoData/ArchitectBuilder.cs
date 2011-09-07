namespace Commons.Data.DemoData
{
	public class ArchitectBuilder
	{
		public ConvertableList<Architect> BuildAll()
		{
			return new ConvertableList<Architect>()
			       	{
			       		new Architect {Name = "������� �������"},
			       		new Architect {Name = "���������� ���"},
			       		new Architect {Name = "���������� ���������"},
			       	};
		}
	}
}
