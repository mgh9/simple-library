using FinLib.Models.Enums;
using ValueType = FinLib.Models.Enums.ValueType;

namespace FinLib.Models.Base.SearchFilters
{
    public class SearchFilterItem<TValue>
    {
        /// <summary>
        /// if empty, then use the SearchFilterItem property's name itself
        /// </summary>
        public string ColumnName { get; set; }

        public ConditionType ConditionType { get; set; }
        public ValueType ValueType { get; set; }
        public TValue Value { get; set; }

        /// <summary>
        /// for when we want to compare a 'value' between Value and Value2
        /// </summary>
        public TValue Value2 { get; set; }
        public bool? IsIgnore { get; set; }
    }
}
