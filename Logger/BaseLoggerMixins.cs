using System;

namespace Logger
{
    public static class BaseLoggerMixins
    {
        public static void Error(this BaseLogger log, string message, params object[] arguments)
        {
            if(log == null)
                throw new ArgumentNullException(nameof(log));
            string fullMessage = String.Format(message, arguments);
            log.Log(LogLevel.Error, fullMessage);
        }
        public static void Warning(this BaseLogger log, string message, params object[] arguments)
        {
            if (log == null)
                throw new ArgumentNullException(nameof(log));
            string fullMessage = String.Format(message, arguments);
            log.Log(LogLevel.Warning, fullMessage);
        }
        public static void Information(this BaseLogger log, string message, params object[] arguments)
        {
            if (log == null)
                throw new ArgumentNullException(nameof(log));
            string fullMessage = String.Format(message, arguments);
            log.Log(LogLevel.Information, fullMessage);
        }
        public static void Debug(this BaseLogger log, string message, params object[] arguments)
        {
            if (log == null)
                throw new ArgumentNullException(nameof(log));
            string fullMessage = String.Format(message, arguments);
            log.Log(LogLevel.Debug, fullMessage);
        }

    }
}
