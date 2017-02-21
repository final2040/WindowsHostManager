using Entities;

namespace Model
{
    public class FileManager
    {
        private FileHelper _fileHelper;

        public FileManager(FileHelper fileHelper)
        {
            this._fileHelper = fileHelper;
        }

        public void Add(EConfiguration configuration)
        {
            throw new System.NotImplementedException();
        }
    }
}