using System.Collections.Generic;

namespace Entities
{
    public interface IMainView: IView
    {
        List<EConfiguration> Configurations { get; set; }
        EConfiguration SelectedConfiguration { get; set; }
    }
}