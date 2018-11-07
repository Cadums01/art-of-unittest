using System;
namespace LogAn
{
    public class FileExtensionManager:IExtensionManager
    {
        public bool WasLastFileNameValid { get; set; }

        public bool IsValid(string fileName){

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
