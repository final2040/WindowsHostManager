using System;
using AppResources;
using Entities;

namespace Presenter
{
    public class PresenterBase<T, T2> 
        where T:IView
        where T2:IHostManager
    {
        protected T2 _model;
        protected T _view;
        protected void ShowExceptionErrorMessage(Exception exception)
        {
            _view.ShowMessage(MessageType.Error,
                LocalizableStringHelper.GetLocalizableString("UnexpectedError_Tittle"),
                string.Format(LocalizableStringHelper.GetLocalizableString("UnexpectedError_Text")
                    , exception.Message));
        }
    }
}