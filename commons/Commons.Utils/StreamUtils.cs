using System.IO;

namespace Commons.Utils
{
	public static class StreamUtils
	{
		public static byte[] ReadToBuffer(Stream stream)
		{
			stream.Position = 0;
			int streamLength = (int)stream.Length;
			byte[] buffer = new byte[streamLength];
			stream.Read(buffer, 0, streamLength);
			return buffer;
		}
		
	}
}
