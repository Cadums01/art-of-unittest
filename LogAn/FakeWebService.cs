using System;

namespace LogAn
{
    public class FakeWebService : IWebService
    {
        public Exception ToTrow;
        public  string LastError { get; set; }
        
        public void LogError(string message)
        {
            if (ToTrow == null) return;
            
            LastError = message;
            throw ToTrow;
        }
    }
}