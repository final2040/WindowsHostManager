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
    public class PEdit
    {
        private IEditView _view;
        private readonly IHostManager _model;
        private readonly Dictionary<string, string> _messageTable = new Dictionary<string, string>();


        public PEdit(IEditView view, IHostManager model)
        {
            _view = view;
            _model = model;
            _messageTable.Add("ErrorCaption", LocalizableStringHelper.GetLocalizableString("Error_Data_Tittle"));
            _messageTable.Add("EmptyText", LocalizableStringHelper.GetLocalizableString("Error_EmptyName_Text"));
            _messageTable.Add("InvalidFormat", LocalizableStringHelper.GetLocalizableString("Error_InvalidNameFormat_Text"));
            _messageTable.Add("EmptyContent", LocalizableStringHelper.GetLocalizableString("Error_EmptyContent_Text"));
            _messageTable.Add("NameTooLong", LocalizableStringHelper.GetLocalizableString("Error_NameTooLong_Text"));
        }

        public void Save()
        {
            Validator<EConfiguration> validator = new Validator<EConfiguration>();
            validator.AddRule(new RequiredRule("Name", _messageTable["EmptyText"],true));
            validator.AddRule(new RequiredRule("Content", _messageTable["EmptyText"],true));
            validator.AddRule(new RegexRule("Name", _messageTable["InvalidFormat"], "^[a-zA-Z0-9-_áéíóúÁÉÍÓÚ ]+$", true));
            validator.AddRule(new MaxLengthRule("Name", 25, _messageTable["NameTooLong"]));

            List<ValidationError> erros = new List<ValidationError>();
            if (validator.TryValidate(_view.Configuration, erros))
            {
                _model.AddConfig(_view.Configuration);
                _view.DialogResult = DialogResult.OK;
                _view.Close();
            }
            else
                _view.ShowMessage(MessageType.Error, _messageTable["ErrorCaption"],
                        string.Join(Environment.NewLine, erros.Select(e => e.ErrorMessage)));
        }
    }
}