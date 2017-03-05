using System;
using AppResources;
using Entities;

namespace Presenter
{
    public class PresenterBase
    {
        protected IHostManager _model;
        protected IView _view;
        protected void ShowExceptionErrorMessage(Exception exception)
        {
            _view.ShowMessage(MessageType.Error,
                LocalizableStringHelper.GetLocalizableString("UnexpectedError_Tittle"),
                string.Format(LocalizableStringHelper.GetLocalizableString("UnexpectedError_Text")
                    , exception.Message));
        }
    }
}