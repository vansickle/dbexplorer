#region License

/*
 * Copyright © 2002-2006 the original author or authors.
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

#region Imports

using System;
using System.Collections.Specialized;
using System.IO;
using Common.Logging;
using log4net.Config;

#endregion

namespace Commons.Logging
{
    /// <summary>
    /// Concrete subclass of ILoggerFactoryAdapter specific to log4net.
    /// </summary>
    /// <author>Gilles Bayon</author>
    /// <version>$Id: ExtendedLog4NetLoggerFactoryAdapter.cs,v 1.2 2006/11/29 23:42:01 oakinger Exp $</version>
    public class ExtendedLog4NetLoggerFactoryAdapter : ILoggerFactoryAdapter
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="properties"></param>
        public ExtendedLog4NetLoggerFactoryAdapter(NameValueCollection properties)
        {
            string configType = string.Empty;

            if (properties["configType"] != null)
            {
                configType = properties["configType"].ToUpper();
            }

            string configFile = string.Empty;
            if (properties["configFile"] != null)
            {
                configFile = properties["configFile"];
                if (configFile.StartsWith("~/") || configFile.StartsWith("~\\"))
                {
                    configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\') + "/", configFile.Substring(2));
                }
            }

            if (configType == "FILE" || configType == "FILE-WATCH")
            {
                if (configFile == string.Empty)
                {

                    throw new ConfigurationException("Configration property 'configFile' must be set for log4Net configuration of type 'FILE'.");

                }

                if (!File.Exists(configFile))
                {

                    throw new ConfigurationException("log4net configuration file '" + configFile + "' does not exists");

                }
            }

            FileInfo optionalConfigFile = null;
            if (properties["optionalConfigFile"] != null)
            {
                string optionalConfigFileName = properties["optionalConfigFile"];
                if (optionalConfigFileName.StartsWith("~/") || optionalConfigFileName.StartsWith("~\\"))
                {
                    optionalConfigFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('/', '\\') + "/", optionalConfigFileName.Substring(2));
                }
                if (optionalConfigFileName == String.Empty)
                    throw new ConfigurationException("Configration property 'optionalConfigFile' must be set to not empty string for log4Net configuration of type 'FILE'.");
                FileInfo fileInfo = new FileInfo(optionalConfigFileName);
                if (fileInfo.Exists) optionalConfigFile = fileInfo;
            }

            switch (configType)
            {
                case "INLINE":
                    XmlConfigurator.Configure();
                    break;
                case "FILE":
                    XmlConfigurator.Configure(new FileInfo(configFile));
                    if (optionalConfigFile != null)
                        XmlConfigurator.Configure(optionalConfigFile);
                    break;
                case "FILE-WATCH":
                    XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
                    if (optionalConfigFile != null)
                        XmlConfigurator.ConfigureAndWatch(optionalConfigFile);
                    break;
                case "EXTERNAL":
                    // Log4net will be configured outside of Common.Logging
                    break;
                default:
                    BasicConfigurator.Configure();
                    break;
            }
        }

        #region ILoggerFactoryAdapter Members

        /// <summary>
        /// Get a ILog instance by type name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILog GetLogger(string name)
        {
            return new ExtendedLog4NetLogger(log4net.LogManager.GetLogger(name));
        }

        /// <summary>
        /// Get a ILog instance by type 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ILog GetLogger(Type type)
        {
            return new ExtendedLog4NetLogger(log4net.LogManager.GetLogger(type));
        }

        #endregion
    }
}
