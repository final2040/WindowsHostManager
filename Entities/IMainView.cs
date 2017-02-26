using System.Collections.Generic;
using System.Windows.Forms;

namespace Entities
{
    public interface IMainView: IView
    {
        List<EConfiguration> Configurations { get; set; }
        DialogResult ShowMessage(MessageType type, string title, string message);
    }
}