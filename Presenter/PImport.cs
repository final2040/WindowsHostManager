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

        public PImport(IImportFileView view)
        {
            this._view = view;
        }

        public void Submit()
        {
            string error = ValidateView();
            if (error.Length > 0)
            {
                _view.ShowMessage(MessageType.Error,
                    LocalizableStringHelper.GetLocalizableString("ImportDialog_Error_Tittle"), error);
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
                errorMessage.AppendLine(LocalizableStringHelper.GetLocalizableString("ImportDialog_EmptyName_Text"));
            }
            if (!string.IsNullOrWhiteSpace(_view.ConfigName) 
                && !Regex.IsMatch(_view.ConfigName, "^[a-zA-Z0-9-_áéíóúÁÉÍÓÚ ]+$"))
            {
                errorMessage.AppendLine(
                    LocalizableStringHelper.GetLocalizableString("ImportDialog_InvalidNameFormat_Text"));
            }
            if (string.IsNullOrWhiteSpace(_view.Path))
            {
                errorMessage.AppendLine(LocalizableStringHelper.GetLocalizableString("ImportDialog_EmptyPath_Text"));
            }
            
            return errorMessage.ToString();
        }
    }
}