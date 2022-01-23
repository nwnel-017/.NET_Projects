namespace Logger
{
    public class LogFactory
    {
        private static string _Path;

        public BaseLogger CreateLogger()
        {
            if (_Path == null)
                return null;
            BaseLogger log = new FileLogger(_Path, nameof(LogFactory));
            return log;
        }

        public static void ConfigureFileLogger(string path) //where is this method getting called?
        {
            _Path = path;
        }
    }
}
