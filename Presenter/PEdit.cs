using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AppResources;
using Entities;

namespace Presenter
{
    public class PEdit
    {
        private IEditView _view;

        public PEdit(IEditView view)
        {
            this._view = view;
        }

        public void Submit()
        {
            string error = ValidateView();
            if (error.Length == 0)
            {
                _view.DialogResult = DialogResult.OK;
                _view.Close();
            }
            else
                _view.ShowMessage(MessageType.Error,
                        LocalizableStringHelper.GetLocalizableString("Error_Data_Tittle"),
                        error);
        }

        private string ValidateView()
        {
            StringBuilder errorMessage = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_view.Configuration.Name))
            {
                errorMessage.AppendLine(LocalizableStringHelper.GetLocalizableString("Error_EmptyName_Text"));
            }
            if (!string.IsNullOrWhiteSpace(_view.Configuration.Name)
               && !Regex.IsMatch(_view.Configuration.Name, "^[a-zA-Z0-9-_áéíóúÁÉÍÓÚ ]+$"))
            {
                errorMessage.AppendLine(
                    LocalizableStringHelper.GetLocalizableString("Error_InvalidNameFormat_Text"));
            }
            if (string.IsNullOrWhiteSpace(_view.Configuration.Content))
            {
                errorMessage.AppendLine(LocalizableStringHelper.GetLocalizableString("Error_EmptyContent_Text"));
            }
            return errorMessage.ToString();
        }
    }
}