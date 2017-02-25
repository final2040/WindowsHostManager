namespace Entities
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
}