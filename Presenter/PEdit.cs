using System.Collections.Generic;
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
        private readonly IHostManager _model;
        private readonly Dictionary<string, string> _errorTable = new Dictionary<string, string>();

        public PEdit(IEditView view, IHostManager model)
        {
            _view = view;
            _model = model;
            _errorTable.Add("ErrorCaption", LocalizableStringHelper.GetLocalizableString("Error_Data_Tittle"));
            _errorTable.Add("EmptyText", LocalizableStringHelper.GetLocalizableString("Error_EmptyName_Text"));
            _errorTable.Add("InvalidFormat", LocalizableStringHelper.GetLocalizableString("Error_InvalidNameFormat_Text"));
            _errorTable.Add("EmptyContent", LocalizableStringHelper.GetLocalizableString("Error_EmptyContent_Text"));
        }

        public void Save()
        {
            string error = ValidateView();
            if (error.Length == 0)
            {
                _view.DialogResult = DialogResult.OK;
                _view.Close();
            }
            else
                _view.ShowMessage(MessageType.Error, _errorTable["ErrorCaption"],
                        error);
        }

        private string ValidateView()
        {
            StringBuilder errorMessage = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_view.Configuration.Name))
            {
                errorMessage.AppendLine(_errorTable["EmptyText"]);
            }
            if (!string.IsNullOrWhiteSpace(_view.Configuration.Name)
               && !Regex.IsMatch(_view.Configuration.Name, "^[a-zA-Z0-9-_áéíóúÁÉÍÓÚ ]+$"))
            {
                errorMessage.AppendLine(_errorTable["InvalidFormat"]);
            }
            if (string.IsNullOrWhiteSpace(_view.Configuration.Content))
            {
                errorMessage.AppendLine(_errorTable["EmptyContent"]);
            }
            return errorMessage.ToString();
        }
    }
}