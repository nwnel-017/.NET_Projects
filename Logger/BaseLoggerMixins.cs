using System;

namespace Logger
{
    public static class BaseLoggerMixins
    {
        public static void Error(this BaseLogger log, string message, string[] args)
        {
            if (log == null || message == null || message == "" || args == null)
              throw new ArgumentNullException();
            log.Log(LogLevel.Error, message);
        }
        public static void Warning(this BaseLogger log, string message, string[] args)
        {
            if (log == null || message == null || message == "" || args == null)
                throw new ArgumentNullException();
            log.Log(LogLevel.Warning, message);
        }
        public static void Information(this BaseLogger log, string message, string[] args)
        {
            if (log == null || message == null || message == "" || args == null)
                throw new ArgumentNullException();
            log.Log(LogLevel.Information, message);
        }
        public static void Debug(this BaseLogger log, string message, string[] args)
        {
            if (log == null || message == null || message == "" || args == null)
                throw new ArgumentNullException();
            log.Log(LogLevel.Debug, message);
        }

    }
}
