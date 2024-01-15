namespace TreeAndJournal.Application.Logging
{
    public class DbLoggerOptions
    {
        public string ConnectionString { get; set; }
        public string[] LogFields { get; set; }
        public string LogTable { get;set; }

        public DbLoggerOptions() 
        {
        }
    }
}
