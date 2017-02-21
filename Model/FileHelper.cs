using System.IO;

namespace Model
{
    public class FileHelper
    {
        public virtual void Copy(string sourceFileName, string destFileName)
        {
            File.Copy(sourceFileName, destFileName);
        }

        public virtual void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            File.Copy(sourceFileName, destFileName, overwrite);
        }

        public virtual bool Exists(string path)
        {
            return File.Exists(path);
        }

        public virtual void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path,contents);
        }

        public virtual string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public virtual string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public virtual void Delete(string path)
        {
            File.Delete(path);
        }
    }
}