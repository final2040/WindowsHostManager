namespace Entities
{
    public interface IViewFactory
    {
        IView Create(string viewName);
    }
}