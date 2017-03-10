using System.Collections.Generic;
using System.Windows.Forms;

namespace Entities
{
    public interface IMainView: IView
    {
        List<EConfiguration> Configurations { get; set; }
        EConfiguration SelectedConfiguration { get; set; }
    }
}