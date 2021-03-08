namespace SenchaExtensions
{
    public interface ISortOperation
    {
        string Property { get; set; }
        SortDirection Direction { get; set; }
    }
}
