using System.IO;

namespace Model
{
    public interface IFileHelper
    {
        void Copy(string sourceFileName, string destFileName);
        void Copy(string sourceFileName, string destFileName, bool overwrite);
        bool Exists(string path);
        void WriteAllText(string path, string contents);
        string[] GetFiles(string path);
        string ReadAllText(string path);
        void Delete(string path);
    }

    public class FileHelper : IFileHelper
    {
        public void Copy(string sourceFileName, string destFileName)
        {
            File.Copy(sourceFileName, destFileName);
        }

        public void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            File.Copy(sourceFileName, destFileName, overwrite);
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path,contents);
        }

        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }
    }
}