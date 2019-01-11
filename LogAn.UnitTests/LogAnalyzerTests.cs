using System;
using NSubstitute;
using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        private readonly FakeExtensionManager _fakeExtensionManager;
        private readonly FakeWebService _mockWebService;
        private FakeEmailService _emailService;

        public LogAnalyzerTests()
        {
            _emailService = new FakeEmailService();
            _fakeExtensionManager = new FakeExtensionManager();
            _mockWebService = new FakeWebService();
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionUpperCase_ReturnTrue()
        {
            _fakeExtensionManager.WillBeValid = true;

            var log = MakerAnalyser(_fakeExtensionManager, _mockWebService, _emailService);
            var result = log.IsValidLogFileName("filewithgoodextension.SLF");

            Assert.True(result);
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionLowerCase_ReturnTrue()
        {
            _fakeExtensionManager.WillBeValid = true;

            var log = MakerAnalyser(_fakeExtensionManager, _mockWebService, _emailService);
            var result = log.IsValidLogFileName("filewithgoodextension.slf");

            Assert.True(result);
        }

        [Test]
        public void IsValidLogFileName_BadExtension_ReturnFalse()
        {
            _fakeExtensionManager.WillBeValid = false;

            var log = MakerAnalyser(_fakeExtensionManager, _mockWebService, _emailService);
            var result = log.IsValidLogFileName("filewithbadextension.foo");

            Assert.False(result);
        }

        [Test]
        public void IsValidFileName_EmptyFileName_ReturnException()
        {
            _fakeExtensionManager.WillBeValid = false;

            var log = MakerAnalyser(_fakeExtensionManager, _mockWebService, _emailService);
            var ex = Assert.Catch<Exception>(() => log.IsValidLogFileName(string.Empty));

            Assert.That(ex.Message, Does.Contain("file has to be provide"));
        }

        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnTrue()
        {
            _fakeExtensionManager.WillBeValid = true;

            var log = MakerAnalyser(_fakeExtensionManager, _mockWebService, _emailService);
            var result = log.IsValidLogFileName("short.ext");

            Assert.True(result);
        }

        [Test]
        public void Analyze_TooShortFileName_CallWebService()
        {
            _fakeExtensionManager.WillBeValid = true;

            var log = MakerAnalyser(_fakeExtensionManager, _mockWebService, _emailService);

            const string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);
            StringAssert.Contains("FileName too short: abc.ext", _mockWebService.LastError);
        }

        [Test]
        public void Analyze_WebServiceThrows_SendEmail()
        {
            _mockWebService.ToTrow = new Exception("fake exception");

            var log = MakerAnalyser(_fakeExtensionManager, _mockWebService, _emailService);
            const string tooShortFileName = "abc.ext";

            log.Analyze(tooShortFileName);

            var expectedEmail = new EmailInfo()
            {
                Body = "fake exception",
                Subject = "can't log",
                To = "someone@somewhere.com"
            };

            Assert.AreEqual(expectedEmail, _emailService.Email);
        }
    
        private LogAnalyzer MakerAnalyser(IExtensionManager extensionManager,
            IWebService fakeWebService, IEmailService emailService)
        {
            return new LogAnalyzer(extensionManager, fakeWebService, emailService);
        }
    }
}