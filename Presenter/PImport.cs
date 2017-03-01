using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AppResources;
using Entities;

namespace Presenter
{
    public class PImport
    {
        private IImportFileView _view;
        private readonly Dictionary<string, string> _errorTable = new Dictionary<string, string>();

        public PImport(IImportFileView view)
        {
            this._view = view;
            _errorTable.Add("ErrorCaption", LocalizableStringHelper.GetLocalizableString("Error_Data_Tittle"));
            _errorTable.Add("EmptyName", LocalizableStringHelper.GetLocalizableString("Error_EmptyName_Text"));
            _errorTable.Add("InvalidNameFormat", LocalizableStringHelper.GetLocalizableString("Error_InvalidNameFormat_Text"));
            _errorTable.Add("EmptyPath", LocalizableStringHelper.GetLocalizableString("Error_EmptyPath_Text"));
        }

        public void Submit()
        {
            string error = ValidateView();
            if (error.Length > 0)
            {
                _view.ShowMessage(MessageType.Error,_errorTable["ErrorCaption"], error);
            }
            else
            {
                _view.DialogResult = DialogResult.OK;
                _view.Close();
            }

        }

        private string ValidateView()
        {
            StringBuilder errorMessage = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_view.ConfigName))
            {
                errorMessage.AppendLine(_errorTable["EmptyName"]);
            }
            if (!string.IsNullOrWhiteSpace(_view.ConfigName) 
                && !Regex.IsMatch(_view.ConfigName, "^[a-zA-Z0-9-_áéíóúÁÉÍÓÚ ]+$"))
            {
                errorMessage.AppendLine(_errorTable["InvalidNameFormat"]);
            }
            if (string.IsNullOrWhiteSpace(_view.Path))
            {
                errorMessage.AppendLine(_errorTable["EmptyPath"]);
            }
            
            return errorMessage.ToString();
        }
    }
}