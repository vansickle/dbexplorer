using System;
using System.IO;
using System.Reflection;

namespace Commons.UI.LayoutDataStore
{
    public class LayoutDataStorePathFactory : ILayoutDataStorePathFactory
    {
        private LayoutDataTypePath typePath;

        /// <summary>
        ///                     »нициализирует новый экземпл€р класса <see cref="T:System.Object" />.
        /// </summary>
        public LayoutDataStorePathFactory(LayoutDataTypePath typePath)
        {
            this.typePath = typePath;
        }

        /// <summary>
        ///                     »нициализирует новый экземпл€р класса <see cref="T:System.Object" />.
        /// </summary>
        public LayoutDataStorePathFactory() : this(LayoutDataTypePath.ApplicationData)
        {
        }

        public LayoutDataTypePath TypePath
        {
            get { return typePath; }
            set { typePath = value; }
        }
        
		#region ILayoutDataStorePathFactory Members
        public string GetConfigFileName(string fileName, string fileExt)
        {
            string path;
            switch (typePath)
            {
                case LayoutDataTypePath.ExecutablePath:
                    path = string.Format("{0}{1}Settings"
                                         , Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                                         , Path.DirectorySeparatorChar);

                    break;
                case LayoutDataTypePath.ApplicationData:
                default:
                    path = string.Format("{0}{1}{2}"
                                         , Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                         , Path.DirectorySeparatorChar
                                         , Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location));
                    break;
            }
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return string.Format("{0}{1}{2}.{3}"
                                 , path, Path.DirectorySeparatorChar, fileName, fileExt);
        }

        #endregion
    }
}
