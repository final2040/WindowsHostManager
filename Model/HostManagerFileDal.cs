using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entities;

namespace Model
{
    public class HostManagerFileDal
    {
        private readonly IFileHelper _fileHelper;
        private readonly string _hostsFilePath = Path.Combine(Environment.SystemDirectory, "drivers\\etc\\hosts");
        private readonly string _programBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string _extension = ".host";

        public HostManagerFileDal(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }

        public void LoadConfig(EConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(configuration.Content))
                throw new ArgumentNullException("El contenido de la configuración no puede ser nulo");

            _fileHelper.WriteAllText(_hostsFilePath,
                configuration.Content);
        }

        public void AddConfig(string name, string content)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "Se debe especificar" +
                                                " un nombre para la nueva " +
                                                "configuración");
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException("content","Debe de especificar " +
                                                "un contenido para la nueva " +
                                                "configuración");

            string fileName = Path.Combine(_programBaseDirectory,
                Path.ChangeExtension(name,_extension));

            _fileHelper.WriteAllText(fileName, content);
        }

        public void AddConfig(string path)
        {
            if (!_fileHelper.Exists(path))
                throw new FileNotFoundException("No se pudo encontrar el archivo origen", path);

            var fileName = Path.GetFileName(path);
            var destinationFile = Path.Combine(_programBaseDirectory, Path.ChangeExtension(fileName, _extension));
            _fileHelper.Copy(path, destinationFile, true);
        }

        public List<EConfiguration> GetAll()
        {
            return _fileHelper.GetFiles(Path.Combine(_programBaseDirectory, "*.host")).Select(file => new EConfiguration()
            {
                Name = Path.GetFileNameWithoutExtension(file), Content = _fileHelper.ReadAllText(file)
            }).ToList();
        }

        public EConfiguration GetConfig(string path)
        {
            if (!_fileHelper.Exists(path))
                throw new FileNotFoundException("El archivo especificado no existe", path);

            return new EConfiguration()
            {
                Name = Path.GetFileNameWithoutExtension(path),
                Content = _fileHelper.ReadAllText(path)
            };
        }

        public void AddConfig(EConfiguration configuration)
        {
            AddConfig(configuration.Name, configuration.Content);
        }

        public void DeleteConfig(EConfiguration configuration)
        {
            string fileName = Path.Combine(_programBaseDirectory,
                Path.ChangeExtension(configuration.Name, _extension));
            _fileHelper.Delete(configuration.Name);
        }
    }
}