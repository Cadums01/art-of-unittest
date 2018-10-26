using System;
using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {

        [Test]
        public void IsValidLogFileName_GoodExtensionUpperCase_ReturnTrue()
        {
            var log = new LogAnalyzer();

            var result = log.IsValidLogFileName("filewithgoodextension.SLF");

            Assert.True(result);
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionLowerCase_ReturnTrue()
        {
            var log = MakerAnalyser();

            var result = log.IsValidLogFileName("filewithgoodextension.slf");

            Assert.True(result);
        }

        [Test]
        public void IsValidLogFileName_BadExtension_ReturnFalse()
        {
            var log = MakerAnalyser();

            var result = log.IsValidLogFileName("filewithbadextension.foo");

            Assert.False(result);
        }

        [Test]
        public void IsValidFileName_EmptyFileName_ReturnException()
        {
            var log = MakerAnalyser();

            var ex = Assert.Catch<Exception>(() => log.IsValidLogFileName(string.Empty));

            Assert.That(ex.Message, Does.Contain("file has to be provide"));
        }

        [TestCase("filenamewitbadextension.foo", false)]
        [TestCase("filenamewithgoodextension.slf", true)]
        public void IsValidLogFileName_WhenCalled_ChangeWasLastFileNameValid(string fileName, bool expected)
        {
            var log = MakerAnalyser();

            log.IsValidLogFileName(fileName);

            Assert.AreEqual(expected, log.WasLastFileNameValid);
        }

        public LogAnalyzer MakerAnalyser()
        {
            return new LogAnalyzer();
        }
    }
}