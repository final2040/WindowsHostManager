using System.Windows.Forms;

namespace Entities
{
    public interface IView
    {
        DialogResult ShowDialog();
        void Show();
        void Close();
        DialogResult DialogResult { get; set; }
        DialogResult ShowMessage(MessageType type, string title, string message);

    }
}