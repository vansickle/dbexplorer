namespace Commons.Data.DemoData
{
	public class PainterBuilder
	{
		public ConvertableList<Painter> BuildAll()
		{
			return new ConvertableList<Painter>
			       	{
			       		new Painter{Name = "Иван Айвазовский"}
			       	};
		}
	}
}
