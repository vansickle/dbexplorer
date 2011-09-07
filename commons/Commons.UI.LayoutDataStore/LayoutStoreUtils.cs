using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Commons.UI.LayoutDataStore
{
    /// <summary>
    /// common ulilities
    /// <rus>общие утилиты</rus>
    /// </summary>
    public class LayoutStoreUtils
    {
        /// <summary>
        /// convert stream in string
        /// <rus>переводит данные типа Stream в данные типа String</rus>
        /// </summary>
        /// <param name="dataStream">
        ///             stream data
        ///        <rus>данные типа Stream </rus>
        /// </param>
        /// <returns>
        ///             string data
        ///        <rus>данные типа String </rus>
        /// </returns>
        public static string Stream2String(Stream dataStream)
        {
            if (dataStream == null)
                return "";
            dataStream.Seek(0, SeekOrigin.Begin);

            StreamReader reader = new StreamReader(dataStream);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// convert string in stream
        /// <rus>переводит данные типа String в данные типа Stream</rus>
        /// </summary>
        /// <param name="dataString">
        ///             string data
        ///        <rus>данные типа String </rus>
        /// </param>
        /// <returns>
        ///             stream data
        ///        <rus>данные типа Stream </rus>
        /// </returns>
        public static Stream String2Stream(string dataString)
        {

            if (string.IsNullOrEmpty(dataString)) return null;

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.AutoFlush = true;
            writer.Write(dataString);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        public static string GetMD5Encrypted(string word)
        {

            //generate md5 password
            Byte[] originalBytes = Encoding.Default.GetBytes(word);
            Byte[] encodedBytes = new MD5CryptoServiceProvider().ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes);
        }


    }
}
