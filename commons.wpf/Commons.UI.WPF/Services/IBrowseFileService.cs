namespace Commons.UI.WPF.Services
{
	public interface IBrowseFileService
	{
		BrowseResult Browse(BrowseParams browseParams);
		BrowseResult Save(BrowseParams browseParams);
	}
}
