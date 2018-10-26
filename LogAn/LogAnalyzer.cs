using System;

namespace LogAn
{
    public class LogAnalyzer
    {
        public bool WasLastFileNameValid { get; set; }

        public bool IsValidLogFileName(string fileName)
        {
            WasLastFileNameValid = false;

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("file has to be provide");

            if (!fileName.EndsWith(".SLF", StringComparison.OrdinalIgnoreCase))
                return false;

            WasLastFileNameValid = true;
            return true;
        }
    }
}