using TrackPlatform.Other;

namespace TrackPlatform.Example.Logs
{
    public static class LogIntegrator
    {
        public static void Integrate()
        {
            Logger.LogMessage += Logger_LogMessage;
        }

        private static void Logger_LogMessage(string message)
        {
            InternalLogger.Log.Debug(message);
        }
    }
}