using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entities;

namespace Model
{
    public class HostManagerFileDal : IHostManager
    {
        protected IFileHelper FileHelper;
        private readonly string _hostsFilePath = Path.Combine(Environment.SystemDirectory, "drivers\\etc\\hosts");
        private readonly string _programBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string _extension = ".host";

        public HostManagerFileDal()
        {
            FileHelper = new FileHelper();
        }


        public void LoadConfig(EConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration", "La configuración no puede ser nula");
            if (string.IsNullOrWhiteSpace(configuration.Name))
                throw new ArgumentException("Se debe especificar" +
                                                " un nombre para la nueva " +
                                                "configuración", "configuration");
            if (string.IsNullOrWhiteSpace(configuration.Content))
                throw new ArgumentException("El contenido de la configuración no puede ser nulo", "configuration");

            FileHelper.WriteAllText(_hostsFilePath,
                configuration.Content);
        }

        public List<EConfiguration> GetAll()
        {
            return FileHelper.GetFiles(_programBaseDirectory).Where(f => Path.GetExtension(f) == _extension).Select(file => new EConfiguration()
            {
                Name = Path.GetFileNameWithoutExtension(file),
                Content = FileHelper.ReadAllText(file)
            }).ToList();
        }

        public EConfiguration ReadExternalConfig(string path)
        {
            if (!FileHelper.Exists(path))
                throw new FileNotFoundException("El archivo especificado no existe", path);

            return new EConfiguration()
            {
                Name = Path.GetFileNameWithoutExtension(path),
                Content = FileHelper.ReadAllText(path)
            };
        }

        public void AddConfig(EConfiguration configuration)
        {
            FileHelper.WriteAllText(GetFileName(configuration), configuration.Content);
        }

        public void DeleteConfig(EConfiguration configuration)
        {
            FileHelper.Delete(GetFileName(configuration));
        }

        private string GetFileName(EConfiguration configuration)
        {
            return Path.Combine(_programBaseDirectory,
                Path.ChangeExtension(configuration.Name, _extension));
        }

        public bool Exists(EConfiguration configuration)
        {
            return FileHelper.Exists(GetFileName(configuration));
        }
    }
}