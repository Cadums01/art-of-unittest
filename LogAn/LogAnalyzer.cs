using System;

namespace LogAn
{
    public class LogAnalyzer
    {
        private IExtensionManager _extensionManager;

        public LogAnalyzer(IExtensionManager extensionManager)
        {
            _extensionManager = extensionManager;
        }

        public bool IsValidLogFileName(string fileName)
        {   
            return _extensionManager.IsValid(fileName);
        }
    }
}