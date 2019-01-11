namespace LogAn.Nsub
{
    public class LogAnalyzerNsub
    {
        private ILogger _logger;

        public LogAnalyzerNsub(ILogger logger)
        {
            _logger = logger;
        }
        
        public int MinNameLength { get; set; }

        public void Analyze(string filename)
        {
            if (filename.Length<MinNameLength)
            {
                _logger.LogError(string.Format("Filename too short: {0}",filename));
            }
        }
    }
}