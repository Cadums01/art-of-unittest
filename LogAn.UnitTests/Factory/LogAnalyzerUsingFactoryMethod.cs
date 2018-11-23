using System;
using NUnit.Framework;

namespace LogAn.UnitTests.Factory
{
    public class LogAnalyzerUsingFactoryMethod
    {
        public bool IsValidLogFileName(string filename)
        {
            return GetManager().IsValid(filename);
        }

        protected virtual IExtensionManager GetManager(){
            return new FileExtensionManager();
        }

        [TestFixture]
        public class LogAnalyzerTests{

            [Test]
            public void OverrideTest()
            {
                FakeExtensionManager stub = new FakeExtensionManager
                {
                    WillBeValid = true
                };

                TestableLogAnalizer logan = new TestableLogAnalizer(stub);
                bool result = logan.IsValidLogFileName("file.ext");

                Assert.True(result);
            }
        }

        class TestableLogAnalizer:LogAnalyzerUsingFactoryMethod{
            public IExtensionManager manager;
            public TestableLogAnalizer(IExtensionManager mgr)
            {
                manager = mgr;
            }

            protected override IExtensionManager GetManager()
            {
                return manager;
            }
        }
    }
}
