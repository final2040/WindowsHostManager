using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AppResources;
using Entities;
using Model;

namespace Presenter
{
    public class PMain
    {
        private IMainView _view;
        private readonly IImportFileView _importFileView;
        private IHostManager _model;


        public PMain(IMainView view, IImportFileView importFileView, IHostManager model)
        {
            _view = view;
            _importFileView = importFileView;
            _model = model;

            LocalizableStringHelper.SetCulture("es");
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
                _view.ShowMessage(MessageType.Error, LocalizableStringHelper.GetLocalizableString("UnexpectedError_Tittle"), LocalizableStringHelper.GetLocalizableString("UnexpectedError_Text") + exception.Message);
            }

            if (configurationList == null || configurationList.Count == 0)
                _view.ShowMessage(MessageType.Error, LocalizableStringHelper.GetLocalizableString("NoConfigurationFoundError_Tittle"), LocalizableStringHelper.GetLocalizableString("NoConfigurationFoundError_Text"));

            _view.Configurations = configurationList;
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

        private void ImportConfig(EConfiguration configuration)
        {

            if (_model.Exists(configuration))
            {
                var result = _view.ShowMessage(MessageType.YesNo,
                    LocalizableStringHelper.GetLocalizableString("OverwriteMessage_Tittle"),
                    LocalizableStringHelper.GetLocalizableString("OverWriteMessage_Text"));
                if (result == DialogResult.Yes)
                {
                    _model.AddConfig(configuration);
                    UpdateView();
                }
            }
            else
            {
                _model.AddConfig(configuration);
                UpdateView();
            }
        }

        public void ImportConfig()
        {
            if (_importFileView.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportConfig(new EConfiguration(0, _importFileView.ConfigName,
                        _model.ReadExternalConfig(_importFileView.Path).Content));
                    _view.ShowMessage(MessageType.Info, LocalizableStringHelper.GetLocalizableString("Success_Tittle"),
                        LocalizableStringHelper.GetLocalizableString("SuccessImport_Text"));
                }
                catch (Exception exception)
                {
                    ShowExceptionErrorMessage(exception);
                }
            }
        }

        private void ShowExceptionErrorMessage(Exception exception)
        {
            _view.ShowMessage(MessageType.Error,
                LocalizableStringHelper.GetLocalizableString("UnexpectedError_Tittle"),
                LocalizableStringHelper.GetLocalizableString("UnexpectedError_Text")
                + exception.Message);
        }

        public void Delete(EConfiguration configuration)
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
    }
}