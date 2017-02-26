using System.Collections.Generic;

namespace Entities
{
    public interface IHostManager
    {
        void LoadConfig(EConfiguration configuration);
        List<EConfiguration> GetAll();
        EConfiguration ReadExternalConfig(string path);
        void AddConfig(EConfiguration configuration);
        void DeleteConfig(EConfiguration configuration);
        bool Exists(EConfiguration configuration);
    }
}