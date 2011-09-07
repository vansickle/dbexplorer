using System;

namespace Commons.UI.LayoutDataStore
{
    [Serializable]
    public class LayoutDataStoreException : ApplicationException
    {
        public LayoutDataStoreException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        public LayoutDataStoreException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public LayoutDataStoreException(string message)
            : base(message)
        {
        }

        public LayoutDataStoreException()
        {
        }
    }
}
