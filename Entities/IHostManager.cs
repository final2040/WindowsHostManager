using System.Collections.Generic;

namespace Entities
{
    public interface IHostManager
    {
        void LoadConfig(EConfiguration configuration);
        List<EConfiguration> GetAll();
        EConfiguration GetConfig(string path);
        void AddConfig(EConfiguration configuration);
        void DeleteConfig(EConfiguration configuration);
    }
}