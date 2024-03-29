#region License

/*
 * Copyright � 2002-2006 the original author or authors.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

using System;
using Common.Logging;
using Common.Logging.Factory;
using log4net.Core;

namespace Commons.Logging
{
    /// <remarks>
    /// Extended currently only because of internal constructor in Log4NetLogger
    /// Log4net is capable of outputting extended debug information about where the current 
    /// message was generated: class name, method name, file, line, etc. Log4net assumes that the location
    /// information should be gathered relative to where Debug() was called. 
    /// When using Common.Logging, Debug() is called in Common.Logging.Log4Net.ExtendedLog4NetLogger. This means that
    /// the location information will indicate that Common.Logging.Log4Net.ExtendedLog4NetLogger always made
    /// the call to Debug(). We need to know where Common.Logging.ILog.Debug()
    /// was called. To do this we need to use the log4net.ILog.Logger.Log method and pass in a Type telling
    /// log4net where in the stack to begin looking for location information.
    /// </remarks>
    /// <author>Gilles Bayon</author>
    /// <version>$Id: ExtendedLog4NetLogger.cs,v 1.1 2006/11/13 07:25:46 markpollack Exp $</version>
    public class ExtendedLog4NetLogger : AbstractLogger
    {
         #region Fields

        private readonly ILogger _logger = null;
        private readonly static Type declaringType = typeof(ExtendedLog4NetLogger);

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log"></param>
        internal protected ExtendedLog4NetLogger(ILoggerWrapper log)
        {
            _logger = log.Logger;
        }

        #region ILog Members

        /// <summary>
        /// Checks if this logger is enabled for the <see cref="F:Common.Logging.LogLevel.Trace" /> level.
        /// </summary>
        public override bool IsTraceEnabled
        {
            get { return _logger.IsEnabledFor(Level.Trace); }
        }

        /// <summary>
        /// Checks if this logger is enabled for the <see cref="F:Common.Logging.LogLevel.Debug" /> level.
        /// </summary>
        public override bool IsDebugEnabled
        {
            get { return _logger.IsEnabledFor(Level.Debug); }
        }

        /// <summary>
        /// Checks if this logger is enabled for the <see cref="F:Common.Logging.LogLevel.Info" /> level.
        /// </summary>
        public override bool IsInfoEnabled
        {
            get { return _logger.IsEnabledFor(Level.Info); }
        }

        /// <summary>
        /// Checks if this logger is enabled for the <see cref="F:Common.Logging.LogLevel.Warn" /> level.
        /// </summary>
        public override bool IsWarnEnabled
        {
            get { return _logger.IsEnabledFor(Level.Warn); }
        }

        /// <summary>
        /// Checks if this logger is enabled for the <see cref="F:Common.Logging.LogLevel.Error" /> level.
        /// </summary>
        public override bool IsErrorEnabled
        {
            get { return _logger.IsEnabledFor(Level.Error); }
        }

        /// <summary>
        /// Checks if this logger is enabled for the <see cref="F:Common.Logging.LogLevel.Fatal" /> level.
        /// </summary>
        public override bool IsFatalEnabled
        {
            get { return _logger.IsEnabledFor(Level.Fatal); }
        }

        /// <summary>
        /// Sends the message to the underlying log4net system.
        /// </summary>
        /// <param name="logLevel">the level of this log event.</param>
        /// <param name="message">the message to log</param>
        /// <param name="exception">the exception to log (may be null)</param>
        protected override void WriteInternal(LogLevel logLevel, object message, Exception exception)
        {
            Level level = GetLevel(logLevel);
            _logger.Log(declaringType, level, message, exception);
        }

        private static Level GetLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.All:
                    return Level.All;
                case LogLevel.Trace:
                    return Level.Trace;
                case LogLevel.Debug:
                    return Level.Debug;
                case LogLevel.Info:
                    return Level.Info;
                case LogLevel.Warn:
                    return Level.Warn;
                case LogLevel.Error:
                    return Level.Error;
                case LogLevel.Fatal:
                    return Level.Fatal;
                default:
                    throw new ArgumentOutOfRangeException("logLevel", logLevel, "unknown log level");
            }
        }

        #endregion
    }
}
