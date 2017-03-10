using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AppResources;
using Entities;

namespace Presenter
{
    public class MainPresenter : PresenterBase
    {
        private readonly IViewFactory _viewFactory;
        

        public MainPresenter(IMainView view, IViewFactory viewFactory, IHostManager model)
        {
            _viewFactory = viewFactory;
            _view = view;
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

        public void SetConfig()
        {
            if (((IMainView)_view).SelectedConfiguration == null 
                || string.IsNullOrWhiteSpace(((IMainView)_view).SelectedConfiguration.Name) 
                || string.IsNullOrWhiteSpace(((IMainView)_view).SelectedConfiguration.Content))
                _view.ShowMessage(MessageType.Error, LocalizableStringHelper.GetLocalizableString("InvalidConfigurationError_Tittle"), LocalizableStringHelper.GetLocalizableString("InvalidConfigurationError_Text"));
            else
                try
                {
                    _model.LoadConfig(((IMainView)_view).SelectedConfiguration);
                    _view.ShowMessage(MessageType.Info,
                        LocalizableStringHelper.GetLocalizableString("Success_Tittle"),
                        string.Format(LocalizableStringHelper.GetLocalizableString("SuccessSetConfiguration_Text"),
                            ((IMainView)_view).SelectedConfiguration.Name));
                }
                catch (Exception exception)
                {
                    ShowExceptionErrorMessage(exception);
                }
        }

        public void ImportConfig()
        {
            IImportFileView importView = (IImportFileView)_viewFactory.Create("ImportView");
            if (importView.ShowDialog() == DialogResult.OK)
            {
                UpdateView();
            }
        }

        public void Delete()
        {

            try
            {
                if (((IMainView)_view).SelectedConfiguration == null)
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
                        _model.DeleteConfig(((IMainView)_view).SelectedConfiguration);
                        UpdateView();
                    }
                }
            }
            catch (Exception exception)
            {
                ShowExceptionErrorMessage(exception);
            }
        }

        public void Edit()
        {
            IEditView editView = (IEditView)_viewFactory.Create("EditView");
            if (((IMainView)_view).SelectedConfiguration != null)
            {
                editView.Configuration = ((IMainView)_view).SelectedConfiguration;
                editView.EditMode = EditMode.Edit;
                editView.ShowDialog();
            }
            else
                _view.ShowMessage(MessageType.Error,
                    LocalizableStringHelper.GetLocalizableString("InvalidConfigurationError_Tittle"),
                    LocalizableStringHelper.GetLocalizableString("InvalidConfigurationError_Text"));

        }

        public void New()
        {
            IEditView editView = (IEditView)_viewFactory.Create("EditView");
            editView.EditMode = EditMode.New;
            if (editView.ShowDialog() == DialogResult.OK) UpdateView();
        }

        public void BackupConfig(bool showSuccessMessage)
        {

            try
            {
                EConfiguration currentConfiguration = _model.ReadExternalConfig(_model.HostsFilePath);
                currentConfiguration.Name = "Current";
                _model.AddConfig(currentConfiguration);
                UpdateView();
                if (showSuccessMessage)
                    _view.ShowMessage(MessageType.Info, LocalizableStringHelper.GetLocalizableString("Success_Tittle"),
                        LocalizableStringHelper.GetLocalizableString("BackupSuccess_Text"));
            }
            catch (Exception exception)
            {
                _view.ShowMessage(
                    MessageType.Error,
                    LocalizableStringHelper.GetLocalizableString("UnexpectedError_Text"),
                    string.Format(LocalizableStringHelper.GetLocalizableString("BackupError_Text"), exception.Message));
            }
        }
    }
}