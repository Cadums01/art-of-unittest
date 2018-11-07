namespace LogAn
{
    public class FakeExtensionManager : IExtensionManager
    {
        public bool WillBeValid { get; set; } = false;

        public bool IsValid(string fileName)
        {
            return true;
        }
    }
}