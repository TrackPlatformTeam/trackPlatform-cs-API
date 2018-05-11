﻿using log4net;

namespace TrackPlatform.Example.Logs
{
    /// <summary>
    /// log4net wrapper
    /// </summary>
    public static class InternalLogger
    {
        private static bool _isFirstRun = true;

        public static ILog Log
        {
            get
            {
                if (_isFirstRun)
                {
                    log4net.Config.XmlConfigurator.Configure();
                    _isFirstRun = false;
                }
                return LogManager.GetLogger(typeof(Program));
            }
        }
    }
}