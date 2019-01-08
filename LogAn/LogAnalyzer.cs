using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LogAn.UnitTests")]

namespace LogAn
{
    public class LogAnalyzer
    {
        private readonly IExtensionManager _extensionManager;
        private readonly IWebService _service;
        private IEmailService _emailService;

        public LogAnalyzer(IExtensionManager extensionManager,
            IWebService service, IEmailService emailService)
        {
            _extensionManager = extensionManager;
            _service = service;
            _emailService = emailService;
        }

        public bool IsValidLogFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("file has to be provide");
            }

            return _extensionManager.IsValid(fileName);
        }

        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                try
                {
                    _service.LogError($"FileName too short: {fileName}");
                }
                catch (Exception ex)
                {
                    var email = new EmailInfo()
                    {
                        Body = "fake exception",
                        Subject = "can't log",
                        To = "someone@somewhere.com"
                    };
                    
                    _emailService.SendEmail(email);
                }
            }
        }
    }
}