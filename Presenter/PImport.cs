using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AppResources;
using Entities;
using ObjectValidator;
using ObjectValidator.rules;
using ObjectValidator.Rules;

namespace Presenter
{
    public class PImport
    {

        private readonly Dictionary<string, string> _messageTable = new Dictionary<string, string>();
        private readonly IImportFileView _view;
        private readonly IHostManager _model;
        private readonly Validator<IImportFileView> _validator = new Validator<IImportFileView>();

        public PImport(IImportFileView view, IHostManager model)
        {
            this._view = view;
            this._model = model;
            this._view = view;
            InitializeMessageTable();
            InitializeValidator();
        }

        private void InitializeMessageTable()
        {
            _messageTable.Add("ErrorCaption", LocalizableStringHelper.GetLocalizableString("Error_Data_Tittle"));
            _messageTable.Add("EmptyName", LocalizableStringHelper.GetLocalizableString("Error_EmptyName_Text"));
            _messageTable.Add("InvalidNameFormat", LocalizableStringHelper.GetLocalizableString("Error_InvalidNameFormat_Text"));
            _messageTable.Add("EmptyPath", LocalizableStringHelper.GetLocalizableString("Error_EmptyPath_Text"));
            _messageTable.Add("UnexpectedErrorCaption", LocalizableStringHelper.GetLocalizableString("UnexpectedError_Tittle"));
            _messageTable.Add("UnexpectedErrorMessage", LocalizableStringHelper.GetLocalizableString("UnexpectedError_Text"));
            _messageTable.Add("SuccessImportCaption", LocalizableStringHelper.GetLocalizableString("Success_Tittle"));
            _messageTable.Add("SuccessImportMessage", LocalizableStringHelper.GetLocalizableString("SuccessImport_Text"));
            _messageTable.Add("RewriteCaption", LocalizableStringHelper.GetLocalizableString("OverwriteMessage_Tittle"));
            _messageTable.Add("RewriteMessage", LocalizableStringHelper.GetLocalizableString("OverWriteMessage_Text"));
            _messageTable.Add("NameTooLong", LocalizableStringHelper.GetLocalizableString("Error_NameTooLong_Text"));
        }

        public void Import()
        {
            List<ValidationError> errors = new List<ValidationError>();
            try
            {
                if (!_validator.TryValidate(_view, errors))
                {
                    _view.ShowMessage(MessageType.Error, _messageTable["ErrorCaption"],
                        string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)));
                }
                else
                {

                    EConfiguration config = GetConfig();
                    if (_model.Exists(config))
                    {
                        if (_view.ShowMessage(MessageType.YesNo, _messageTable["RewriteCaption"], _messageTable["RewriteMessage"]) == DialogResult.Yes)
                        {
                            SaveConfig(config);
                        }
                    }
                    else
                        SaveConfig(config);
                }
            }
            catch (Exception exception)
            {
                _view.ShowMessage(MessageType.Error,
                    _messageTable["UnexpectedErrorCaption"],
                    _messageTable["UnexpectedErrorMessage"] + exception.Message);
            }

        }

        private void InitializeValidator()
        {
            _validator.AddRule(new RequiredRule("ConfigName", _messageTable["EmptyName"], true));
            _validator.AddRule(new RegexRule("ConfigName", _messageTable["InvalidNameFormat"], "^[a-zA-Z0-9-_áéíóúÁÉÍÓÚ ]+$",
                true));
            _validator.AddRule(new MaxLengthRule("ConfigName", 25, _messageTable["NameTooLong"]));
            _validator.AddRule(new RequiredRule("Path", _messageTable["EmptyPath"], true));
        }

        private void SaveConfig(EConfiguration config)
        {
            _model.AddConfig(config);
            _view.ShowMessage(MessageType.Info,
                _messageTable["SuccessImportCaption"],
                _messageTable["SuccessImportMessage"]);
            _view.DialogResult = DialogResult.OK;
            _view.Close();
        }

        private EConfiguration GetConfig()
        {
            EConfiguration config = _model.ReadExternalConfig(_view.Path);
            config.Name = _view.ConfigName;
            return config;
        }
    }
}