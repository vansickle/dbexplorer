using System;

namespace Commons.UI.WPF.Services
{
	/// <summary>
	/// openfiledialog changes application working directory, this container helps to preserve it
	/// </summary>
	public class PreserveWorkingDirectory : IDisposable
	{
		private readonly string workingDirectory;

		public PreserveWorkingDirectory()
		{
			workingDirectory = Environment.CurrentDirectory;
		}

		public void Dispose()
		{
			Environment.CurrentDirectory = workingDirectory;
		}
	}
}
