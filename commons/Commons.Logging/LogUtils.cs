using System;
using Common.Logging;

namespace Commons.Logging
{
    
    public static class LogUtils
    {
        public static void Debug(ILog log, LogDelegate logDelegate)
        {
            if(log.IsDebugEnabled)
                log.Debug(logDelegate.Invoke());
        }

        public static void FormatDebug(ILog log, string message, params object[] parameters)
        {
            FormatDebug(log,message,null,parameters);
        }
            
        public static void FormatDebug(ILog log, string message, Exception exception, params object[] parameters)
        {
            log.Debug(String.Format(message,parameters),exception);
        }
        
        public static void FormatInfo(ILog log, string message, params object[] parameters)
        {
            FormatInfo(log,message,null,parameters);
        }
            
        public static void FormatInfo(ILog log, string message, Exception exception, params object[] parameters)
        {
            log.Info(String.Format(message,parameters),exception);
        }

        public static void Error(ILog log, LogDelegate logDelegate, Exception e)
        {
            if(log.IsErrorEnabled)
                log.Error(logDelegate.Invoke(),e);
        }
    }

    public delegate string LogDelegate();

    //IMPROVE extension method version of LogUtils


}
