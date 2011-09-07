using System.IO;
using Nini.Config;

namespace Commons.Config
{
	public class BaseConfig
	{
		protected IniConfigSource source;

		public BaseConfig(string iniFileName)
		{
			source = new IniConfigSource(iniFileName);

			string mergedPath = string.Format("{0}.local", iniFileName);

			if (File.Exists(mergedPath))
			{
				var forMerge = new IniConfigSource(mergedPath);
				source.Merge(forMerge);
			}
		}
	}
}
