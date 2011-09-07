namespace Db4oExplorer.Domain
{
	public interface IConnectionProfile
	{
		string Name { get; set; }
		string Path { get; set; }
		IConnectionProfile Clone();
	}
}
