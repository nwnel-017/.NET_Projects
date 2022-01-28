namespace Logger
{
    public class LogFactory
    {
        public string? Path { get; set; }

        public FileLogger? CreateLogger()
        {
            if (Path == null)
                return null;
            FileLogger log = new(Path, nameof(LogFactory));
            return log;
        }

        public void ConfigureFileLogger(string path) 
        {
            Path = path;
        }
    }
}
