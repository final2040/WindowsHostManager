namespace Entities
{
    public interface IEditView: IView
    {
        EConfiguration Configuration { get; set; }
        EditMode EditMode { get; set; }
    }
}