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
        /// برای زمانی که بخاهیم بصورت
        /// Between value AND value2
        /// مقایسه انجام بدیم..
        /// البته میشه با تفکیک
        /// value و value2
        /// در 2 کنترل جداگانه، هم به جواب رسید
        /// اولی میشه Greather Than
        /// و دومی میشه Less Than
        /// </summary>
        public TValue Value2 { get; set; }

        /// <summary>
        /// آیا در پارسینگ 
        /// مثلا در خصوص تاریخ های میلادی و شمسی
        /// به این صورت که تاریخ شمسی هم در دل فیلتر میاد ولی ایگنور باید باشه..و سمت
        /// سرور تبدیل بشه به میلادی و اون تاریخ میلادی، پارس بشه
        /// </summary>
        public bool? IsIgnore { get; set; }
    }
}
