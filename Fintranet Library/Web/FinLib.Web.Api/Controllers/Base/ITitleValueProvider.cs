using FinLib.Models.Base;

namespace FinLib.Web.Api.Base
{
    public interface ITitleValueProvider
    {
        Task<JsonResult<List<TitleValue<int>>>> GetTitleValueListAsync(bool includeEmptySelector = false, bool includeSelectAllSelector = false, string text = "");
    }
}
