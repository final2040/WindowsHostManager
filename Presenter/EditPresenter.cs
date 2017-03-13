using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AppResources;
using Entities;
using ObjectValidator;
using ObjectValidator.rules;
using ObjectValidator.Rules;

namespace Presenter
{
    public class EditPresenter : PresenterBase<IEditView, IHostManager>
    {
        private readonly Dictionary<string, string> _messageTable = new Dictionary<string, string>();
        private readonly Validator<EConfiguration> _validator = new Validator<EConfiguration>();

        public EditPresenter(IEditView view, IHostManager model)
        {
            _view = view;
            _model = model;
            InitializeLanguage();
            InitializeValidator();
        }

        private void InitializeLanguage()
        {
            _messageTable.Add("ErrorCaption", LocalizableStringHelper.GetLocalizableString("Error_Data_Tittle"));
            _messageTable.Add("EmptyText", LocalizableStringHelper.GetLocalizableString("Error_EmptyName_Text"));
            _messageTable.Add("InvalidFormat", LocalizableStringHelper.GetLocalizableString("Error_InvalidNameFormat_Text"));
            _messageTable.Add("EmptyContent", LocalizableStringHelper.GetLocalizableString("Error_EmptyContent_Text"));
            _messageTable.Add("NameTooLong", LocalizableStringHelper.GetLocalizableString("Error_NameTooLong_Text"));
            _messageTable.Add("Warning", LocalizableStringHelper.GetLocalizableString("Warning_Tittle"));
            _messageTable.Add("CancelConfirm", LocalizableStringHelper.GetLocalizableString("EditCancel_Confirmation"));
            _messageTable.Add("RewriteCaption", LocalizableStringHelper.GetLocalizableString("OverwriteMessage_Tittle"));
            _messageTable.Add("RewriteMessage", LocalizableStringHelper.GetLocalizableString("OverWriteMessage_Text"));
            _messageTable.Add("Success", LocalizableStringHelper.GetLocalizableString("Success_Tittle"));
            _messageTable.Add("SuccessSave", LocalizableStringHelper.GetLocalizableString("SuccessEdit_Text"));

        }

        private void InitializeValidator()
        {
            _validator.AddRule(new RequiredRule("Name", _messageTable["EmptyText"], true));
            _validator.AddRule(new RequiredRule("Content", _messageTable["EmptyContent"], true));
            _validator.AddRule(new RegexRule("Name", _messageTable["InvalidFormat"], "^[a-zA-Z0-9-_áéíóúÁÉÍÓÚ ]+$", true));
            _validator.AddRule(new MaxLengthRule("Name", 25, _messageTable["NameTooLong"]));
        }
        public void Save()
        {
            try
            {
                List<ValidationError> errors = new List<ValidationError>();
                if (_validator.TryValidate(_view.Configuration, errors))
                {
                    if (_view.IsDirty)
                    {
                        if (_view.EditMode == EditMode.New)
                            SaveNewConfig();
                        else
                            SaveConfig();
                    }
                    else
                    {
                        _view.DialogResult = DialogResult.OK;
                        _view.Close();
                    }
                }
                else
                    _view.ShowMessage(MessageType.Error, _messageTable["ErrorCaption"],
                        string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)));
            }
            catch (Exception exception)
            {
                ShowExceptionErrorMessage(exception);
            }
        }

        private void SaveNewConfig()
        {
            if (_model.Exists(_view.Configuration))
            {
                if (_view.ShowMessage(MessageType.YesNo, _messageTable["RewriteCaption"],
                    _messageTable["RewriteMessage"]) == DialogResult.Yes)
                    SaveConfig();
            }
            else SaveConfig();

        }

        private void SaveConfig()
        {
            _model.AddConfig(_view.Configuration);
            _view.ShowMessage(MessageType.Info, _messageTable["Success"],
                _messageTable["SuccessSave"]);
            _view.DialogResult = DialogResult.OK;
            _view.Close();
        }


        public void Cancel()
        {
            if (_view.IsDirty)
            {
                if (_view.ShowMessage(MessageType.YesNo, _messageTable["Warning"],
                    _messageTable["CancelConfirm"]) == DialogResult.Yes)
                {
                    CancelView();
                }
            }
            else
                CancelView();
        }

        private void CancelView()
        {
            _view.DialogResult = DialogResult.Cancel;
            _view.Close();
        }
    }
}