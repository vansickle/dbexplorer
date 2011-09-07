using System;
using System.IO;

namespace Commons.Utils
{
    public static class FileUtils
    {
        private const string DEFAULT_REPLACE_WITH = "_";

        /// <summary>
        /// replace all illegal path characters with default "_"
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MakeValidFilePath(string path)
        {
            return MakeValidFilePath(path, DEFAULT_REPLACE_WITH);
        }
        
        /// <summary>
        /// replace all illegal path characters with
        /// </summary>
        /// <param name="path"></param>
        /// <param name="replaceIllegalWith"></param>
        /// <returns></returns>
        public static string MakeValidFilePath(string path, string replaceIllegalWith)
        {
            foreach (char c in Path.GetInvalidPathChars())
            {
                path = ReplaceIllegal(path, c, replaceIllegalWith);
            }
            path = ReplaceIllegal(path, '*', replaceIllegalWith);
            path = ReplaceIllegal(path, '?', replaceIllegalWith);
            path = ReplaceIllegal(path, '"', replaceIllegalWith);

            return path;
        }

        private static string ReplaceIllegal(string path, char c, string replaceIllegalWith)
        {
			if (string.IsNullOrEmpty(path)) return string.Empty;
            path = path.Replace(c.ToString(), replaceIllegalWith);
            return path;
        }

		/// <summary>
		/// Стандартный File.ReadAllBytes не устраивает из-за FileShare.Read
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static byte[] ReadAllBytes(string path)
		{
			byte[] buffer;
			using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				int offset = 0;
				long length = stream.Length;
				int count = (int)length;
				buffer = new byte[count];
				while (count > 0)
				{
					int num4 = stream.Read(buffer, offset, count);
					if (num4 == 0)
					{
						throw new EndOfStreamException();

					}
					offset += num4;
					count -= num4;
				}
			}
			return buffer;
		}

    	public static void DeleteIfExists(string path)
    	{
			if (File.Exists(path))
				File.Delete(path);
    	}
    }
}
