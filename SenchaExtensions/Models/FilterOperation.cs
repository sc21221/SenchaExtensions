namespace SenchaExtensions
{
    public class FilterOperation : IFilterOperation
    {
        public string Property { get; set; }
        public object Value { get; set; }
        public string Operator { get; set; }
        public bool ExactMatch { get; set; }
        public bool AnyMatch { get; set; }
        public bool CaseSensitive { get; set; }
    }
}
