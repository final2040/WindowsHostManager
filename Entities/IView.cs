using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Entities
{
    public interface IView
    {
        DialogResult ShowDialog();
        void Show();
        DialogResult DialogResult { get; }
    }
}