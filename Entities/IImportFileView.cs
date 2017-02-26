namespace Entities
{
    public interface IImportFileView:IView
    {
        string ConfigName { get; }
        string Path { get; }
    }
}