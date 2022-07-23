using FinLib.Common.Exceptions.Infra;

namespace FinLib.Models.Dtos
{
    /// <summary>
    /// مدل خروجی برای متد Get بصورت جی سون
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public class GetResultDto<TView> : Base.Dto.BaseDto
        where TView : Base.View.BaseView, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">دیتای فیلتر شده</param>
        /// <param name="count">تعداد تمام آیتم ها (و نه فقط فیلتر شده ها)</param>
        public GetResultDto(List<TView> data, long count)
        {
            Data = data ?? throw new NullModelException(nameof(data));
            Count = count;
        }

        public long Count { get; }
        public List<TView> Data { get; }
    }
}
