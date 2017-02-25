using Entities;

namespace Presenter
{
    public class PMain
    {
        private IMainView _view;
        private IHostManager _model;

        public PMain(IMainView view, IHostManager model)
        {
            _view = view;
            _model = model;
        }

        public void UpdateView()
        {
            _view.Configurations = _model.GetAll();
        }
    }
}