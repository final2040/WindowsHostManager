using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AppResources;
using Entities;

namespace Presenter
{
    public class PMain : PresenterBase
    {
        private readonly IImportFileView _importFileView;
        private readonly IEditView _editView;

        public PMain(IMainView view, IImportFileView importFileView, IEditView editView, IHostManager model)
        {
            _view = view;
            _importFileView = importFileView;
            _editView = editView;
            _model = model;
            //LocalizableStringHelper.SetCulture("es"); // comentado para usar cultura neutral en lo que se finaliza la applicación
        }

        public void UpdateView()
        {
            List<EConfiguration> configurationList = null;

            try
            {
                configurationList = _model.GetAll();
            }
            catch (Exception exception)
            {
                ShowExceptionErrorMessage(exception);
            }

            if (configurationList == null || configurationList.Count == 0)
                _view.ShowMessage(MessageType.Error, LocalizableStringHelper.GetLocalizableString("NoConfigurationFoundError_Tittle"), LocalizableStringHelper.GetLocalizableString("NoConfigurationFoundError_Text"));

           ((IMainView)_view).Configurations = configurationList;
        }

        public void SetConfig(EConfiguration configuration)
        {
            if (configuration == null || string.IsNullOrWhiteSpace(configuration.Name) || string.IsNullOrWhiteSpace(configuration.Content))
                _view.ShowMessage(MessageType.Error, LocalizableStringHelper.GetLocalizableString("InvalidConfigurationError_Tittle"), LocalizableStringHelper.GetLocalizableString("InvalidConfigurationError_Text"));
            else
                try
                {
                    _model.LoadConfig(configuration);
                    _view.ShowMessage(MessageType.Info,
                        LocalizableStringHelper.GetLocalizableString("Success_Tittle"),
                        string.Format(LocalizableStringHelper.GetLocalizableString("SuccessSetConfiguration_Text"),
                            configuration.Name));
                }
                catch (Exception exception)
                {
                    ShowExceptionErrorMessage(exception);
                }
        }

        public void ImportConfig()
        {
            if (_importFileView.ShowDialog() == DialogResult.OK)
            {
                UpdateView();
            }
        }

        public void Delete(EConfiguration configuration)
        {

            try
            {
                if (configuration == null)
                    _view.ShowMessage(MessageType.Error,
                        LocalizableStringHelper.GetLocalizableString("InvalidConfigurationError_Tittle"),
                        LocalizableStringHelper.GetLocalizableString("InvalidConfigurationError_Text"));
                else
                {
                    DialogResult result = _view.ShowMessage(MessageType.YesNo,
                        LocalizableStringHelper.GetLocalizableString("Warning_Tittle"),
                        LocalizableStringHelper.GetLocalizableString("DeleteConfirmation_Text"));
                    if (result == DialogResult.Yes)
                    {
                        _model.DeleteConfig(configuration);
                        UpdateView();
                    }
                }
            }
            catch (Exception exception)
            {
                ShowExceptionErrorMessage(exception);
            }
        }

        public void Edit(EConfiguration configuration)
        {
            if (configuration != null)
            {
                _editView.Configuration = configuration;
                _editView.EditMode = EditMode.Edit;
                _editView.ShowDialog();
            }
            else
                _view.ShowMessage(MessageType.Error,
                    LocalizableStringHelper.GetLocalizableString("InvalidConfigurationError_Tittle"),
                    LocalizableStringHelper.GetLocalizableString("InvalidConfigurationError_Text"));

        }

        public void New()
        {
            _editView.EditMode = EditMode.New;
            if (_editView.ShowDialog() == DialogResult.OK) UpdateView();
        }
    }
}